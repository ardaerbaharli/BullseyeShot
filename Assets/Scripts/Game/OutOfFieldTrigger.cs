using UnityEngine;

namespace Game
{
    public class OutOfFieldTrigger : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Target")) return;
            var target = other.gameObject.GetComponent<Target>();
            if (target.isOutlined)
                playerController.TargetEscaped(target);

            target.DeactivateTarget();
            other.gameObject.SetActive(false);
        }
    }
}