using ArcadianLab.DemoGame.Sound.Controller;
using ArcadianLab.DemoGame.View.Unit;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ArcadianLab.DemoGame.Gameplay.Views.Win
{
    public class WinFailPanelView : MonoBehaviour, IView
    {
        [SerializeField] private GameObject parent;
        [SerializeField] private Text headingText;

        public void ShowView()
        {
            parent.SetActive(true);
        }
        public void HideView()
        {
            parent.SetActive(false);
        }
        public void ShowView(bool win)
        {
            SoundHandler.Instance?.StopMusic();
            headingText.text = win ? "WIN" : "LOSE";
            ShowView();
        }
        public void ReplayBtnClick()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}