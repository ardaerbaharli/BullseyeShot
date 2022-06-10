using System.Collections;
using Main;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace Game
{
    public class GamePanelController : MonoBehaviour
    {
        [SerializeField] private Image background;
        [SerializeField] private ToggleSwitch volumeToggle;
        [SerializeField] private ToggleSwitch pauseResumeToggle;
        [SerializeField] private Text remainingBulletsText;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private LivesController livesController;
        [SerializeField] private GameObject gameOverPanel;

        private void OnEnable()
        {
            volumeToggle.Toggle(Config.IsVolumeOn);

            volumeToggle.valueChanged += OnVolumeChanged;
            pauseResumeToggle.valueChanged += OnPauseResumeChanged;
            playerController.OnLostLife += OnLostLife;
            playerController.OnShot += OnShot;
            playerController.OnReloaded += OnReloaded;
            GameController.Instance.OnGameOver += OnGameOver;
        }


        private void OnGameOver()
        {
            gameOverPanel.SetActive(true);
            gameObject.SetActive(false);
        }


        private IEnumerator Start()
        {
            var activeBackground = Config.ActiveBackgroundName;
            background.sprite = Resources.Load<Sprite>($"Backgrounds/{activeBackground}");

            yield return LocalizationSettings.InitializationOperation;
            Config.LoadLocale(Config.ActiveLanguage);
        }

        private void OnDisable()
        {
            volumeToggle.valueChanged -= OnVolumeChanged;
            pauseResumeToggle.valueChanged -= OnPauseResumeChanged;
            playerController.OnLostLife -= OnLostLife;
            playerController.OnShot -= OnShot;
            playerController.OnReloaded -= OnReloaded;
            GameController.Instance.OnGameOver -= OnGameOver;
        }


        private void OnPauseResumeChanged(bool value)
        {
            Time.timeScale = value ? 1 : 0;
            GameController.Instance.isPaused = !value;
            SoundManager.instance.PauseResume(value);
        }

        private void OnVolumeChanged(bool value)
        {
            Config.IsVolumeOn = value;
            SoundManager.instance.SetSound(value);
        }

        public void Reload()
        {
            if (GameController.Instance.isPaused) return;
            playerController.Reload();
        }

        public void Shoot()
        {
            if (GameController.Instance.isPaused) return;
            playerController.Shoot();
        }

        private void OnShot(int remainingBullets)
        {
            UpdateBulletText(remainingBullets);
        }

        private void OnReloaded()
        {
            UpdateBulletText(Config.MaxBullets);
        }

        private void OnLostLife(int remainingLives, LoseLifeReason r)
        {
            livesController.RemoveLife();
        }

        private void UpdateBulletText(int remainingBullets)
        {
            remainingBulletsText.text = $"{remainingBullets}/{Config.MaxBullets}";
        }
    }
}