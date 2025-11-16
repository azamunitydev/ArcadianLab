using System.Collections.Generic;
using ArcadianLab.DemoGame.Gameplay.Controller;
using ArcadianLab.DemoGame.Gameplay.Core;
using ArcadianLab.DemoGame.Gameplay.PlaneBoard.Controller;
using ArcadianLab.DemoGame.Gameplay.Unit;
using ArcadianLab.DemoGame.Player.Controller;
using ArcadianLab.DemoGame.Sound.Controller;
using ArcadianLab.DemoGame.Utilities.Generics;
using DG.Tweening;
using UnityEngine;

namespace ArcadianLab.DemoGame.Player.Unit
{
    public class PlayerControl : MonoBehaviour
    {
        [Header("Player References")]
        [SerializeField] private Trail trailComponent;
        [SerializeField] private ParticleSystem addParticles;
        [SerializeField] private ParticleSystem subParticles;

        [Header("Positions")]
        [SerializeField] private Vector3 centerPOS;
        [SerializeField, Range(0f, 10f)] private float horizontalMoveOffset = 3f;

        [Header("Swipe Settings")]
        [SerializeField] private float dragSensitivity = 0.02f;

        [Header("Snake Trail Settings")]
        [SerializeField] private float followDistance = 0.5f;
        [SerializeField] private float followSpeed = 10f;

        private bool isDragging = false;
        private float startMouseX;
        private float startPlayerX;
        private Tween moveTween = null;
        private List<Trail> trail = null;
        private List<Trail> removedTrail = null;
        private Sequence particleSeq = null;

        private void Awake()
        {
            if (trail == null) trail = new List<Trail>();
            if (removedTrail == null) removedTrail = new List<Trail>();
            trail.Add(trailComponent);
        }
        private void Update()
        {
            HandleInput();
            UpdateTrailFollow();
        }
        private void HandleInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                isDragging = true;
                startMouseX = Input.mousePosition.x;
                startPlayerX = transform.position.x;
                GenericMethods.KillTween(moveTween);
            }
            if (Input.GetMouseButton(0) && isDragging)
            {
                float delta = (Input.mousePosition.x - startMouseX) * dragSensitivity;
                float targetX = startPlayerX + delta;
                float leftLimit = centerPOS.x - horizontalMoveOffset;
                float rightLimit = centerPOS.x + horizontalMoveOffset;
                targetX = Mathf.Clamp(targetX, leftLimit, rightLimit);
                Vector3 targetPos = new Vector3(targetX, transform.position.y, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, targetPos, 0.2f);
            }
            if (Input.GetMouseButtonUp(0)) isDragging = false;
        }
        private void UpdateTrailFollow()
        {
            if (trail == null || trail.Count == 0) return;

            Vector3 previousPos = new Vector3(
                transform.position.x,
                trail[0].transform.position.y,
                trail[0].transform.position.z
            );

            for (int i = 0; i < trail.Count; i++)
            {
                Rigidbody segment = trail[i].Rigid;
                float fixedZ = -13.6f + (-i * followDistance);
                Vector3 targetPos = new Vector3(
                    previousPos.x,
                    segment.position.y,
                    fixedZ
                );
                segment.MovePosition(
                    Vector3.Lerp(segment.position, targetPos, followSpeed * Time.deltaTime)
                );
                previousPos = segment.position;
            }
            DestroryRemovedTrail();
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(centerPOS, 0.1f);

            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(centerPOS + Vector3.left * horizontalMoveOffset, 0.1f);
            Gizmos.DrawSphere(centerPOS + Vector3.right * horizontalMoveOffset, 0.1f);
        }
        public void AddTrail(int count)
        {
            for (int i = 0; i < count; i++) trail.Add(Instantiate(PlayerHandler.Instance.TrailPrefab));
            SetTrailText();
            ShowParticles(InteractOperation.Add);
        }
        public void RemoveTrail(int count)
        {
            if (trail.Count <= count)
            {
                GameManager.Instance?.ShowWinFailPanel(false);
                PlaneHandler.Instance.TogglePlanesMovement(false);
                SoundHandler.Instance.PlayFailSfx();
            }
            else
                for (int i = 0; i < count; i++)
                {
                    Trail trailElement = trail[trail.Count - 1];
                    removedTrail.Add(trailElement);
                    trail.Remove(trailElement);
                }
            SetTrailText();
            ShowParticles(InteractOperation.Sub);
        }
        public void SetTrailText()
        {
            if (trail == null || trail.Count == 0) return;
            for (int i = 0; i < trail.Count; i++)
                trail[i].TrailText.text = (trail.Count - i).ToString();
        }
        private void DestroryRemovedTrail()
        {
            if (removedTrail == null || removedTrail.Count == 0) return;
            for (int i = 0; i < removedTrail.Count; i++)
            {
                Trail trailElement = removedTrail[i];
                removedTrail.RemoveAt(i);
                Destroy(trailElement.gameObject);
            }
        }
        private void ShowParticles(InteractOperation operation)
        {
            GenericMethods.KillTween(particleSeq);

            addParticles.gameObject.SetActive(false);
            subParticles.gameObject.SetActive(false);

            switch (operation)
            {
                case InteractOperation.Add:
                    particleSeq = DOTween.Sequence();
                    particleSeq.AppendCallback(() => addParticles.gameObject.SetActive(true));
                    particleSeq.AppendInterval(1f);
                    particleSeq.AppendCallback(() => addParticles.gameObject.SetActive(false));
                    break;
                case InteractOperation.Sub:
                    particleSeq = DOTween.Sequence();
                    particleSeq.AppendCallback(() => subParticles.gameObject.SetActive(true));
                    particleSeq.AppendInterval(1f);
                    particleSeq.AppendCallback(() => subParticles.gameObject.SetActive(false));
                    break;
                default:
                    break;
            }
        }
    }
}
