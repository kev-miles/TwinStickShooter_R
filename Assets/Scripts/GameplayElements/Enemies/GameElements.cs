using GameplayElements.Bullets;
using UnityEngine;

namespace GameplayElements.Enemies
{
    public class GameElements : MonoBehaviour
    {
        [SerializeField] private EnemyView enemyView = default;
        [SerializeField] private BulletPool enemyBulletPool = default;
        [SerializeField] private Transform[] spawnPoints = default;
        [SerializeField] private PowerUp powerUp = default;
        
        public EnemyView EnemyView => enemyView;
        public BulletPool EnemyBulletPool => enemyBulletPool;
        public Transform[] SpawnPoints => spawnPoints;
        public PowerUp PowerUp => powerUp;
    }
}