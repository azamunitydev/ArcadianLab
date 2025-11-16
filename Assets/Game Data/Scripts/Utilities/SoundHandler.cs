using UnityEngine;

namespace ArcadianLab.DemoGame.Sound.Controller
{
    public class SoundHandler : MonoBehaviour
    {
        public static SoundHandler Instance;

        [Header(" ------- Audio Sources ------- ")]
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioSource musicSource;

        [Header(" -------- Audio Clips -------- ")]
        [SerializeField] private AudioClip musicBGMusic;
        [SerializeField] private AudioClip addSfx;
        [SerializeField] private AudioClip subSfx;
        [SerializeField] private AudioClip winSfx;
        [SerializeField] private AudioClip failSfx;
        [SerializeField] private AudioClip confettiSfx;

        private void Awake()
        {
            if (Instance == null) Instance = this;
        }
        private void Start()
        {
            PlayMusic();
        }
        public void PlayMusic()
        {
            musicSource.clip = musicBGMusic;
            musicSource.Play();
        }
        public void StopMusic() => musicSource.Stop();
        public void PlayAddSfx() => sfxSource.PlayOneShot(addSfx);
        public void PlaySubSfx() => sfxSource.PlayOneShot(subSfx);
        public void PlayWinSfx() => sfxSource.PlayOneShot(winSfx);
        public void PlayFailSfx() => sfxSource.PlayOneShot(failSfx);
        public void PlayConfettiSfx() => sfxSource.PlayOneShot(confettiSfx, 0.7f);
    }
}
