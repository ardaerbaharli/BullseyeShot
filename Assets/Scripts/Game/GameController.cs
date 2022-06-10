using System.Collections;
using UnityEngine;
using static UnityEngine.Random;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        public delegate void GameOver();

        public static GameController Instance;
        [SerializeField] private PlayerController playerController;

        [Tooltip("Must be even")] [SerializeField]
        private int numberOfTargetsOnStart;

        public int score;
        private Track lastSpawnedTrack;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
            
            // Fix fps
            Application.targetFrameRate = 30;
        }

        private IEnumerator Start()
        {
            lastSpawnedTrack = Track.Bottom;
            score = 0;

            yield return new WaitUntil(() => ObjectPool.instance.isSet);
            SpawnTargets(numberOfTargetsOnStart);
        }

        private void OnEnable()
        {
            playerController.OnScored += OnScored;
            playerController.OnLostLife += OnLostLife;
        }

        private void OnDisable()
        {
            playerController.OnScored -= OnScored;
            playerController.OnLostLife -= OnLostLife;
        }

        public event GameOver OnGameOver;

        private void OnLostLife(int remaininglives, LoseLifeReason reason)
        {
            if (remaininglives <= 0) StartCoroutine(FinishGame());
        }

        private IEnumerator FinishGame()
        {
            Vibration.Vibrate(3);
            yield return new WaitForSeconds(0.5f);
            OnGameOver?.Invoke();
        }


        public void SpawnTargets(int n = 1)
        {
            for (var i = 0; i < n; i++)
            {
                var track = lastSpawnedTrack == Track.Bottom ? Track.Top : Track.Bottom;
                lastSpawnedTrack = track;
                var pObject = ObjectPool.instance.GetPooledObject("Target");
                var target = pObject.target;
                var isActiveDetermine = Range(0, 2) == 0;
                var index = n > 1 ? Mathf.CeilToInt(i / 2f) : numberOfTargetsOnStart / 2;
                target.Set(track, isActiveDetermine, index);
                playerController.activeTargets.Add(target);
            }
        }


        private void OnScored()
        {
            score++;
        }
    }
}