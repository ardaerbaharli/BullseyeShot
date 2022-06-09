using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Main
{
    public class RatingStars : MonoBehaviour
    {
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private List<Star> stars;
        [SerializeField] private GameObject thankFeedback;

        [SerializeField] private bool rated;

        public void Rate()
        {
            if (rated) return;
            var buttonName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
            var rating = int.Parse(buttonName.Last().ToString());
            Rate(rating);
            rated = true;
            thankFeedback.SetActive(true);
        }

        private void Rate(int numberOfStars)
        {
            for (var i = 0; i < numberOfStars; i++)
            {
                stars[i].Activate();
            }
        }

        public void GoBack()
        {
            mainMenu.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}