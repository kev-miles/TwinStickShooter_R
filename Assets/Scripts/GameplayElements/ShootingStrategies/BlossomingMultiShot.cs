using GameplayElements.Bullets.Strategies;
using UnityEngine;

namespace GameplayElements.ShootingStrategies
{
    public class BlossomingMultiShot : ShootingStrategy
    {
        public BlossomingMultiShot() : base()
        {
            base.name = "MultiShot";
        }
        
        public override void Shoot(Transform shooter)
        {
            if(shooter == null) return;
            var rotation = shooter.rotation;
            var position = shooter.position;
            var euler = rotation.eulerAngles;
            pool.Acquire(position,Quaternion.Euler(euler.x, euler.y, euler.z-25), type, new BlossomingBullet());
            pool.Acquire(position, Quaternion.Euler(euler.x, euler.y, euler.z+25) , type, new BlossomingBullet());
        }
    }
}