using ArcadianLab.DemoGame.Gameplay.Unit;
using ArcadianLab.DemoGame.Player.Unit;
using UnityEngine;

namespace ArcadianLab.DemoGame.Player.Controller
{
    public class PlayerHandler : MonoBehaviour
    {
        public static PlayerHandler Instance = null;

        [SerializeField] private PlayerControl playerControl;
        [SerializeField] private Trail trailPrefab;

        public PlayerControl Player => playerControl;
        public Trail TrailPrefab => trailPrefab;

        private void Awake()
        {
            if (Instance == null) Instance = this;
        }
    }
}
