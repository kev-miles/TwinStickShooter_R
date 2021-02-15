using GameplayElements.Bullets;
using UnityEngine;

namespace GameplayElements.User
{
    public class PlayerElements : MonoBehaviour
    {
        [SerializeField] private PlayerView playerView = default;
        [SerializeField] private PlayerConfiguration playerConfiguration = default;
        [SerializeField] private BulletPool playerBulletPool = default;

        public PlayerView View => playerView;
        public PlayerConfiguration Configuration => playerConfiguration;
        public BulletPool BulletPool => playerBulletPool;
    }
}