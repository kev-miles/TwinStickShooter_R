using GameplayElements.Bullets;
using UnityEngine;

namespace GameplayElements.ShootingStrategies
{
    public abstract class ShootingStrategy
    {
        public string Name => name;
        
        protected string name;
        protected BulletPool pool;
        protected BulletType type;

        public virtual void Shoot(Transform shooter){}

        public ShootingStrategy WithPool(BulletPool pool)
        {
            this.pool = pool;
            return this;
        }

        public ShootingStrategy WithType(BulletType type)
        {
            this.type = type;
            return this;
        }
    }
}
