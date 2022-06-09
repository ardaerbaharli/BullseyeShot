using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    [RequireComponent(typeof(Collider2D))]
    public class Target : MonoBehaviour
    {
        [SerializeField] private Vector3 topTrackPosition;
        [SerializeField] private Vector3 bottomTrackPosition;
        [SerializeField] private float speed;

        public bool isOutlined;
        private Collider2D collider;
        private Image image;
        private MoveDirection moveDirection;
        private Vector3 spacing;
        private Track track;


        private void Awake()
        {
            collider = GetComponent<Collider2D>();
            image = GetComponent<Image>();
            spacing = new Vector3(image.rectTransform.sizeDelta.x * 2, 0, 0);
        }


        private void FixedUpdate()
        {
            switch (moveDirection)
            {
                case MoveDirection.Left:
                    transform.Translate(Vector2.left * (speed * Time.deltaTime));
                    break;
                case MoveDirection.Right:
                    transform.Translate(Vector2.right * speed * Time.deltaTime);
                    break;
            }
        }

        public void Set(Track t, bool isActive, int index = 0)
        {
            if (isActive) SetActiveTarget();
            else DeactivateTarget();

            track = t;
            switch (track)
            {
                case Track.Bottom:
                    moveDirection = MoveDirection.Right;
                    break;
                case Track.Top:
                    moveDirection = MoveDirection.Left;
                    break;
            }

            Vector3 pos;
            if (track == Track.Bottom)
            {
                pos = bottomTrackPosition;
                pos -= index * spacing;
            }
            else
            {
                pos = topTrackPosition;
                pos += index * spacing;
            }

            gameObject.GetComponent<RectTransform>().localPosition = pos;
            gameObject.SetActive(true);
        }

        public bool IsTouching(Collider2D c)
        {
            return collider.IsTouching(c);
        }

        public void SetActiveTarget()
        {
            isOutlined = true;
            image.sprite = Resources.Load<Sprite>(Config.TargetActiveSpriteName);
        }

        public void DeactivateTarget()
        {
            isOutlined = false;
            image.sprite = Resources.Load<Sprite>(Config.TargetDeactiveSpriteName);
        }
    }
}