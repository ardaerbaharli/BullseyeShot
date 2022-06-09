using UnityEngine;

namespace Game
{
    public class SpawnTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Target")) return;
            GameController.Instance.SpawnTargets();
        }
    }
}