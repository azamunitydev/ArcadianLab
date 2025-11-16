using System;
using ArcadianLab.DemoGame.ObjectPooling.Controller;
using ArcadianLab.DemoGame.ObjectPooling.Core;
using ArcadianLab.DemoGame.ObjectPooling.Unit;
using DG.Tweening;
using UnityEngine;

namespace ArcadianLab.DemoGame.Gameplay.PlaneBoard.Controller
{
    public class PlaneHandler : MonoBehaviour
    {
        public static PlaneHandler Instance = null;

        private Plane currentPlane = null;

        private int planeIndex = 0;
        private int winIndex = 0;
        private bool hasWinPlaneCreated = false;

        public static Action<bool> OnTogglePlaneMovement = null;

        private void Awake()
        {
            if (Instance == null) Instance = this;
        }
        private void Start()
        {
            winIndex = UnityEngine.Random.Range(3, 10);

            DOVirtual.DelayedCall(0.5f, () =>
            {
                GeneratePlane(2);
            });
        }
        public void GeneratePlane(int count)
        {
            for (int i = 1; i <= count; i++)
            {
                PoolItem plane = ObjectPoolHandler.Instance.GetItem("Plane");
                if (currentPlane != null) plane.transform.localPosition = currentPlane.transform.localPosition + (Vector3.forward * 50f);
                plane.gameObject.SetActive(true);
                currentPlane = plane.GetComponent<Plane>();
                planeIndex++;
            }
        }
        public void GeneratePlaneOffset(int offsetIndex)
        {
            if (!hasWinPlaneCreated)
            {
                PoolItem plane = ObjectPoolHandler.Instance.GetItem("Plane");
                if (currentPlane != null) plane.transform.localPosition = currentPlane.transform.localPosition + (Vector3.forward * 50f);
                plane.gameObject.SetActive(true);
                currentPlane = plane.GetComponent<Plane>();
                if (planeIndex >= winIndex && !hasWinPlaneCreated)
                {
                    hasWinPlaneCreated = true;
                    plane.GetComponent<Plane>().FinishLine.SetActive(true);
                    plane.GetComponent<Plane>().IsFinal = true;
                }
                planeIndex++;
            }
        }
        public void TogglePlanesMovement(bool toggle)
        {
            if (OnTogglePlaneMovement != null) 
                OnTogglePlaneMovement.Invoke(toggle);
        }
    }
}
