using System;
using ArcadianLab.DemoGame.Gameplay.Core;
using ArcadianLab.DemoGame.Player.Controller;
using ArcadianLab.DemoGame.Sound.Controller;
using TMPro;
using UnityEngine;

namespace ArcadianLab.DemoGame.Gameplay.Unit
{
    public class Interactable : MonoBehaviour
    {
        [SerializeField] private GameObject numParent;
        [SerializeField] private TextMeshProUGUI numText;

        private InteractOperation interactOperation;
        private int threshold = 0;

        private void OnEnable()
        {
            interactOperation = (InteractOperation)UnityEngine.Random.Range(0, Enum.GetValues(typeof(InteractOperation)).Length);
            switch (interactOperation)
            {
                case InteractOperation.Add:
                    SetAddThreshold();
                    break;
                case InteractOperation.Sub:
                    SetSubThreshold();
                    break;
                default:
                    break;
            }
            numText.text = threshold.ToString();
        }
        private void SetAddThreshold()
        {
            threshold = UnityEngine.Random.Range(1, 4);
        }
        private void SetSubThreshold()
        {
            threshold = UnityEngine.Random.Range(-4, 0);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                gameObject.SetActive(false);
                switch (interactOperation)
                {
                    case InteractOperation.Add:
                        SoundHandler.Instance.PlayAddSfx();
                        PlayerHandler.Instance.Player.AddTrail(threshold);
                        break;
                    case InteractOperation.Sub:
                        SoundHandler.Instance.PlaySubSfx();
                        PlayerHandler.Instance.Player.RemoveTrail(Mathf.Abs(threshold));
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
