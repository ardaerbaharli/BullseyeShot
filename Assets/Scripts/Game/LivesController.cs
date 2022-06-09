using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public class LivesController : MonoBehaviour
    {
        [SerializeField] private List<GameObject> lives;

        public void RemoveLife()
        {
            if (lives.Count <= 0) return;
            var live = lives.First();
            lives.Remove(live);
            Destroy(live);
        }
    }
}