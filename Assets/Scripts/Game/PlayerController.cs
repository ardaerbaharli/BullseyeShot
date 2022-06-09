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

        [SerializeField] public int remainingBullets;
        [SerializeField] private int remainingLives;
        [SerializeField] private Collider2D aimCollider;
        [SerializeField] private GameObject aim;
        [SerializeField] private Joystick joystick;
        [SerializeField] private float aimSpeed = 1000;

        public List<Target> activeTargets;

        private void Awake()
        {
            remainingBullets = Config.MaxBullets;
            remainingLives = Config.MaxLives;
        }

        private void Start()
        {
            aimCollider = aim.GetComponent<Collider2D>();
        }

        private void FixedUpdate()
        {
            if (joystick.Horizontal == 0 && joystick.Vertical == 0) return;
            aim.transform.position +=
                new Vector3(joystick.Horizontal, joystick.Vertical, 0) * aimSpeed * Time.deltaTime;
        }

        public event LostLife OnLostLife;

        public event Shot OnShot;

        public event Scored OnScored;

        public void Reload()
        {
            remainingBullets = Config.MaxBullets;
        }

        public void Shoot()
        {
            if (remainingBullets < 1) return;
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
            else
            {
                OnLostLife?.Invoke(remainingLives, LoseLifeReason.Missed);
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