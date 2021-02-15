using GameplayElements.Bullets;
using UnityEngine;

namespace GameplayElements.ShootingStrategies
{
    public abstract class ShootingStrategy
    {
        public string Name => name;
        
        protected string name;
        protected BulletPool bulletPool;
        protected BulletType type;

        public ShootingStrategy(BulletPool pool, BulletType type)
        {
            bulletPool = pool;
        }

        public virtual void Shoot(Transform shooter){}
    }
}
