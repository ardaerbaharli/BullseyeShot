using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Main
{
    public class BackgroundMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private Image backgroundImage;
    
        public void GoBackButton()
        {
            gameObject.SetActive(false);
            mainMenu.SetActive(true);
        }

        public void ChangeBackgroundButton()
        {
            var buttonName = EventSystem.current.currentSelectedGameObject.name;
            Config.ActiveBackgroundName = buttonName;
            backgroundImage.sprite = Resources.Load<Sprite>("Backgrounds/" + buttonName);
        }
    }
}