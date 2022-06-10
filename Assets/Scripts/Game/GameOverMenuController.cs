using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameOverMenuController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI score;

        private void Awake()
        {
            score.text = $"{GameController.Instance.score}";
        }

        public void BackToMainMenuButton()
        {
            SceneManager.LoadScene("Main");
        }
    }
}