using GameplayElements.Bullets;
using UnityEngine;

namespace GameplayElements.User
{
    public class PlayerElements : MonoBehaviour
    {
        [SerializeField] private PlayerView playerView = default;
        [SerializeField] private EntityConfiguration entityConfiguration = default;
        [SerializeField] private BulletPool playerBulletPool = default;

        public PlayerView View => playerView;
        public EntityConfiguration Configuration => entityConfiguration;
        public BulletPool BulletPool => playerBulletPool;
    }
}