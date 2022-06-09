using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Main
{
    public class ImageGallery : MonoBehaviour
    {
        [SerializeField] private Image imageComponent;
        [SerializeField] private List<Sprite> sprites;

        [SerializeField] private string imgName;
        [SerializeField] private int imgIndex;

        public delegate void ValueChanged(string value);

        public event ValueChanged valueChanged;

        public void Set(string imgName)
        {
            this.imgName = imgName;
            imgIndex = sprites.FindIndex(x => x.name == imgName);
            Set();
        }

        public void Set(int index)
        {
            imgIndex = index;
            Set();
        }

        private void Set()
        {
            if (imgIndex >= sprites.Count)
                imgIndex = 0;

            if (imgIndex >= 0)
                imageComponent.sprite = sprites[imgIndex];

            imgName = sprites[imgIndex].name;

            valueChanged?.Invoke(imgName);
        }

        public void Next()
        {
            imgIndex++;
            Set(imgIndex);
        }
    }
}