using System;

namespace GameplayElements.Bullets
{
    public class EnemyBullets : BulletPool
    {
        private void Awake()
        {
            SetContainerName("Enemy Bullet Container");
            Load();
        }
    }
}