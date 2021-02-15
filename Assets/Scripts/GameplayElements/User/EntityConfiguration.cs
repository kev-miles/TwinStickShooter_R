using UnityEngine;

namespace GameplayElements.User
{
    [CreateAssetMenu]
    public class EntityConfiguration : ScriptableObject
    {
        [SerializeField, Range(0,10)] private float playerSpeed = 5f;
        [SerializeField] private int playerHp = 5;
        [SerializeField, Range(0,10)] private float enemySpeed = 5f;
        [SerializeField] private int enemyHp = 3;
        [SerializeField] private int scorePowerUp = 100;
        [SerializeField] private int scoreDamage = -150;
        [SerializeField] private int scoreDeath = -350;
        [SerializeField] private int scoreEnemy = 25;
        
        public int PlayerHp => playerHp;
        public float PlayerSpeed => playerSpeed;
        public int EnemyHp => enemyHp;
        public float EnemySpeed => enemySpeed;
        public int ScoreDamage => scoreDamage;
        public int ScoreEnemy => scoreEnemy;
        public int ScorePowerUp => scorePowerUp;
        public int ScoreDeath => scoreDeath;
    }
}