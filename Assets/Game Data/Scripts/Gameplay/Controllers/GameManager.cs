using ArcadianLab.DemoGame.Gameplay.Views.Win;
using UnityEngine;

namespace ArcadianLab.DemoGame.Gameplay.Controller
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        [SerializeField] private ParticleSystem confettiParticles;
        [SerializeField] private WinFailPanelView winFailPanel;

        public ParticleSystem ConfettiParticles => confettiParticles;

        private void Awake()
        {
            if (Instance == null) Instance = this;
        }
        public void ShowWinFailPanel(bool win)
        {
            winFailPanel.ShowView(win);
        }
    }
}
