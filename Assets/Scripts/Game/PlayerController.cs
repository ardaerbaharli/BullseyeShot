using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public class PlayerController : MonoBehaviour
    {
        public delegate void LostLife(int remainingLives, LoseLifeReason reason);

        public delegate void Scored();

        public delegate void Shot(int remainingBullets);

        public delegate void Reloaded();

        [SerializeField] public int remainingBullets;
        [SerializeField] private int remainingLives;
        [SerializeField] private Collider2D aimCollider;
        [SerializeField] private GameObject aim;
        [SerializeField] private Joystick joystick;
        [SerializeField] private float aimSpeed = 1000;

        public List<Target> activeTargets;
        [SerializeField] private float xBound, yBound;

        private RectTransform aimRect;

        private void Awake()
        {
            remainingBullets = Config.MaxBullets;
            remainingLives = Config.MaxLives;
        }

        private void Start()
        {
            aimCollider = aim.GetComponent<Collider2D>();
            activeTargets = new List<Target>();
            // get aim width from image

            aimRect = aim.GetComponent<RectTransform>();
            var sizeDelta = aimRect.sizeDelta;
            var aimWidth = sizeDelta.x;
            var aimHeight = sizeDelta.y;
            xBound = Screen.width / 2f - aimWidth / 4f;
            yBound = Screen.height / 2f - aimHeight / 4f;
        }

        private void FixedUpdate()
        {
            if (joystick.Horizontal == 0 && joystick.Vertical == 0) return;
            var deltaX = joystick.Horizontal * aimSpeed * Time.deltaTime;
            var deltaY = joystick.Vertical * aimSpeed * Time.deltaTime;
            var aimPos = aimRect.localPosition;
            // if aim is going to be out of bounds, clamp it
            var newX = Mathf.Clamp(aimPos.x + deltaX, -xBound, xBound);
            var newY = Mathf.Clamp(aimPos.y + deltaY, -yBound, yBound);
            var newPos = new Vector3(newX, newY, 0);
            aimRect.anchoredPosition = newPos;
        }

        public event LostLife OnLostLife;

        public event Shot OnShot;

        public event Scored OnScored;
        public event Reloaded OnReloaded;

        public void Reload()
        {
            if (remainingBullets == Config.MaxBullets) return;
            SoundManager.instance.PlayReloadSound();
            remainingBullets = Config.MaxBullets;
            OnReloaded?.Invoke();
        }

        public void Shoot()
        {
            if (remainingBullets < 1) return;
            SoundManager.instance.PlayShootingSound();
            Vibration.Vibrate(1);

            remainingBullets--;
            OnShot?.Invoke(remainingBullets);
            if (activeTargets.Any(x => x.IsTouching(aimCollider)))
            {
                var hittedTarget = activeTargets.First(x => x.IsTouching(aimCollider));
                activeTargets.Remove(hittedTarget);
                hittedTarget.gameObject.SetActive(false);
                if (hittedTarget.isOutlined)
                {
                    OnScored?.Invoke();
                }
                else
                {
                    remainingLives--;
                    OnLostLife?.Invoke(remainingLives, LoseLifeReason.WrongTarget);
                }
            }
        }

        public void TargetEscaped(Target target)
        {
            activeTargets.Remove(target);
            remainingLives--;
            OnLostLife?.Invoke(remainingLives, LoseLifeReason.TargetEscaped);
        }
    }
}