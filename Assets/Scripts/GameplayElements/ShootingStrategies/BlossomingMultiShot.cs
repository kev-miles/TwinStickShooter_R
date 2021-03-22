using GameplayElements.Bullets.Strategies;
using UnityEngine;

namespace GameplayElements.ShootingStrategies
{
    public class BlossomingMultiShot : ShootingStrategy
    {
        public BlossomingMultiShot() : base()
        {
            base.name = "BlossomingMultiShot";
        }
        
        public override void Shoot(Transform shooter)
        {
            var rotation = shooter.rotation;
            var position = shooter.position;
            var euler = rotation.eulerAngles;
            pool.Acquire(position,Quaternion.Euler(euler.x, euler.y, euler.z-25), type, new BlossomingBullet());
            pool.Acquire(position, Quaternion.Euler(euler.x, euler.y, euler.z+25) , type, new BlossomingBullet());
        }
    }
}