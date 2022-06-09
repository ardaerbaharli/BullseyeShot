using UnityEngine;
using UnityEngine.UI;

namespace Main
{
    public class Star : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Sprite onSprite;
        [SerializeField] private Sprite offSprite;
        [SerializeField] private bool isOn;


        public void Activate()
        {
            image.sprite = onSprite;
            isOn = true;
        }

        public void Deactivate()
        {
            image.sprite = offSprite;
            isOn = false;
        }
    }
}