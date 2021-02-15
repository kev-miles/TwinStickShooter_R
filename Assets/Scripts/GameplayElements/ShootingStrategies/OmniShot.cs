using GameplayElements.Bullets;
using UnityEngine;

namespace GameplayElements.ShootingStrategies
{
    public class OmniShot : ShootingStrategy
    {
        public OmniShot(BulletPool pool, BulletType type) : base(pool, type)
        {
            base.name = "OmniShot";
            base.bulletPool = pool;
            base.type = type;
        }

        public override void Shoot(Transform shooter)
        {
            var rotation = shooter.rotation;
            var position = shooter.position;
            bulletPool.Acquire(position, rotation, type);
            
            var euler = rotation.eulerAngles;
            bulletPool.Acquire(position,Quaternion.Euler(euler.x, euler.y, euler.z+45), type);
            bulletPool.Acquire(position, Quaternion.Euler(euler.x, euler.y, euler.z+90) , type);
            bulletPool.Acquire(position, Quaternion.Euler(euler.x, euler.y, euler.z+135) , type);
            bulletPool.Acquire(position, Quaternion.Euler(euler.x, euler.y, euler.z+180) , type);
            bulletPool.Acquire(position, Quaternion.Euler(euler.x, euler.y, euler.z+225) , type);
            bulletPool.Acquire(position, Quaternion.Euler(euler.x, euler.y, euler.z+270) , type);
            bulletPool.Acquire(position, Quaternion.Euler(euler.x, euler.y, euler.z+315) , type);
        }
    }
}