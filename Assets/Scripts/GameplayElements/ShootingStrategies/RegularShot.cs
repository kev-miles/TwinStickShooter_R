using GameplayElements.Bullets;
using UnityEngine;

namespace GameplayElements.ShootingStrategies
{
    public class RegularShot : ShootingStrategy
    {
        public RegularShot() : base()
        {
            base.name = "Regular";
        }

        public override void Shoot(Transform shooter)
        {
            pool.Acquire(shooter.position, shooter.rotation, type);
        }
    }
}