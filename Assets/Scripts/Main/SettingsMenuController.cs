using UnityEngine;

namespace Main
{
    public class SettingsMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private ToggleSwitch volumeToggle;
        [SerializeField] private ToggleSwitch vibrationToggle;
        [SerializeField] private ImageGallery languages;

        private void Awake()
        {
            volumeToggle.Toggle(Config.IsVolumeOn);
            vibrationToggle.Toggle(Config.IsVibrationOn);
            languages.Set(Config.ActiveLanguage);

            volumeToggle.valueChanged += OnVolumeChanged;
            vibrationToggle.valueChanged += OnVibrationChanged;
            languages.valueChanged += OnLanguageChanged;
        }

        private void OnDestroy()
        {
            volumeToggle.valueChanged -= OnVolumeChanged;
            vibrationToggle.valueChanged -= OnVibrationChanged;
            languages.valueChanged -= OnLanguageChanged;
        }

        private void OnLanguageChanged(string value)
        {
            Config.ActiveLanguage = value;
            Config.LoadLocale(Config.ActiveLanguage);
        }

        private void OnVibrationChanged(bool value)
        {
            Config.IsVibrationOn = value;
        }

        private void OnVolumeChanged(bool value)
        {
            Config.IsVolumeOn = value;
        }

        public void GoBack()
        {
            mainMenu.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}