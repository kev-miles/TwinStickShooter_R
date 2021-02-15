using GameplayElements.Bullets;
using UnityEngine;

namespace GameplayElements.ShootingStrategies
{
    public class MultiShot : ShootingStrategy
    {
        public MultiShot(BulletPool pool, BulletType type) : base(pool, type)
        {
            base.name = "MultiShot";
            base.bulletPool = pool;
            base.type = type;
        }
        
        public override void Shoot(Transform shooter)
        {
            var rotation = shooter.rotation;
            var position = shooter.position;
            bulletPool.Acquire(position, rotation, type);
            
            var euler = rotation.eulerAngles;
            bulletPool.Acquire(position,Quaternion.Euler(euler.x, euler.y, euler.z-45), type);
            bulletPool.Acquire(position, Quaternion.Euler(euler.x, euler.y, euler.z+45) , type);
        }
    }
}