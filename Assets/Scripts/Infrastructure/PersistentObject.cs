using UnityEngine;

namespace Infrastructure
{
    public class PersistentObject : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
