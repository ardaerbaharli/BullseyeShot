using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Main
{
    public class ToggleSwitch : MonoBehaviour, IPointerDownHandler
    {
        [Header("Configuration")] [Tooltip("Use Toggle() method for button clicks.")] [SerializeField]
        private bool isButton;

        [Space] [SerializeField] private bool changeColor;
        [SerializeField] private Color onColor;
        [SerializeField] private Color offColor;
        [SerializeField] private float colorChangeDuration;

        [Space] [SerializeField] private bool changeText;
        [SerializeField] private bool isTMP;
        [SerializeField] private TextMeshProUGUI tmpText;
        [SerializeField] private Text text;

        [SerializeField] private string onText;
        [SerializeField] private string offText;

        [Space] [SerializeField] private bool changeImage;
        [SerializeField] private Image imageComponent;
        [SerializeField] private Sprite onImage;
        [SerializeField] private Sprite offImage;

        public bool isOn;

        public delegate void ValueChanged(bool value);

        public event ValueChanged valueChanged;

        public void Toggle(bool value)
        {
            isOn = value;
            ToggleColor(isOn);

            if (valueChanged != null)
                valueChanged(isOn);
        }


        public void Toggle()
        {
            Toggle(!isOn);
        }

        private void ToggleColor(bool value)
        {
            if (changeColor)
                StartCoroutine(ColorTo(value ? onColor : offColor, colorChangeDuration));

            if (changeText)
            {
                if (isTMP)
                    tmpText.text = value ? onText : offText;
                else
                    text.text = value ? onText : offText;
            }

            if (changeImage)
            {
                imageComponent.sprite = value ? onImage : offImage;
            }
        }

        private IEnumerator ColorTo(Color color, float duration)
        {
            var startColor = text.color;
            var t = 0f;
            while (t < duration)
            {
                t += Time.deltaTime;
                text.color = Color.Lerp(startColor, color, t / duration);
                yield return null;
            }
        }


        public void OnPointerDown(PointerEventData eventData)
        {
            if (!isButton)
                Toggle(!isOn);
        }
    }
}