using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Main
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject backgroundMenu;
        [SerializeField] private GameObject yourFeedbackMenu;
        [SerializeField] private GameObject settingsMenu;
        [SerializeField] private Image background;
        

        private IEnumerator Start()
        {
            var activeBackground = Config.ActiveBackgroundName;
            background.sprite = Resources.Load<Sprite>($"Backgrounds/{activeBackground}");

            yield return LocalizationSettings.InitializationOperation;
            Config.LoadLocale(Config.ActiveLanguage);
        }

        public void StartButton()
        {
            SceneManager.LoadScene("Game");
        }

        public void BackgroundButton()
        {
            gameObject.SetActive(false);
            backgroundMenu.SetActive(true);
        }

        public void YourFeedbackButton()
        {
            gameObject.SetActive(false);
            yourFeedbackMenu.SetActive(true);
        }

        public void SettingsButton()
        {
            gameObject.SetActive(false);
            settingsMenu.SetActive(true);
        }
    }
}
