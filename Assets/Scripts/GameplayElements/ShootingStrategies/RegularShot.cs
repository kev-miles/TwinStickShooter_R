using GameplayElements.Bullets;
using UnityEngine;

namespace GameplayElements.ShootingStrategies
{
    public class RegularShot : ShootingStrategy
    {
        public RegularShot(BulletPool pool, BulletType type) : base(pool, type)
        {
            base.name = "Regular";
            base.bulletPool = pool;
            base.type = type;
        }

        public override void Shoot(Transform shooter)
        {
            bulletPool.Acquire(shooter.position, shooter.rotation, type);
        }
    }
}