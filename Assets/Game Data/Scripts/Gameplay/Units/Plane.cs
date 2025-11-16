using System.Collections.Generic;
using ArcadianLab.DemoGame.Gameplay.Controller;
using ArcadianLab.DemoGame.Gameplay.PlaneBoard.Controller;
using ArcadianLab.DemoGame.Gameplay.Unit;
using ArcadianLab.DemoGame.ObjectPooling.Controller;
using ArcadianLab.DemoGame.ObjectPooling.Unit;
using ArcadianLab.DemoGame.Sound.Controller;
using DG.Tweening;
using UnityEngine;

public class Plane : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private PoolItem poolItem;
    [SerializeField] private Interactable[] interactables;
    [SerializeField] private GameObject finishLine;
    [SerializeField] private Transform confettiPOS;

    public GameObject FinishLine => finishLine;
    public bool IsFinal { get; set; } = false;

    private int enableCount = 0;
    private bool toggleMove = false;

    private void OnEnable()
    {
        PlaneHandler.OnTogglePlaneMovement += ToggleMove;
        if (interactables == null || interactables.Length == 0) return;
        enableCount = Random.Range(3, 5);
        int count = Mathf.Clamp(enableCount, 0, interactables.Length);
        foreach (var item in interactables) item.gameObject.SetActive(false);
        List<int> availableIndices = new List<int>();
        for (int i = 0; i < interactables.Length; i++) availableIndices.Add(i);
        for (int i = 0; i < availableIndices.Count; i++)
        {
            int randomIndex = Random.Range(i, availableIndices.Count);
            (availableIndices[i], availableIndices[randomIndex]) = (availableIndices[randomIndex], availableIndices[i]);
        }
        for (int i = 0; i < count; i++)
        {
            int index = availableIndices[i];
            interactables[index].gameObject.SetActive(true);
        }
        toggleMove = true;
    }
    private void OnDisable()
    {
        PlaneHandler.OnTogglePlaneMovement -= ToggleMove;
    }
    private void Update()
    {
        if (toggleMove)
            transform.position = transform.position + Vector3.back * Time.deltaTime * speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlaneHandler.Instance?.GeneratePlaneOffset(2);
            DOVirtual.DelayedCall(3f, () =>
            {
                if (!IsFinal)
                    ObjectPoolHandler.Instance?.ReturnItemToPool(poolItem);
            });
        }
        if (IsFinal)
        {
            PlaneHandler.Instance.TogglePlanesMovement(false);
            ParticleSystem confetti = Instantiate(GameManager.Instance.ConfettiParticles, confettiPOS);
            confetti.transform.localPosition = Vector3.zero;
            confetti.transform.localRotation = Quaternion.identity;
            DOVirtual.DelayedCall(1f, () =>
            {
                GameManager.Instance?.ShowWinFailPanel(true);
            });
            SoundHandler.Instance?.PlayWinSfx();
            SoundHandler.Instance?.PlayConfettiSfx();
        }
    }
    public void ToggleMove(bool toggle) => toggleMove = toggle;
}
