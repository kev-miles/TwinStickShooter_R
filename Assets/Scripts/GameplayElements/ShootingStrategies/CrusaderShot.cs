using GameplayElements.Bullets;
using UnityEngine;

namespace GameplayElements.ShootingStrategies
{
    public class CrusaderShot : ShootingStrategy
    {
        public CrusaderShot(BulletPool pool, BulletType type) : base(pool, type)
        {
            base.name = "Crusader";
            base.bulletPool = pool;
            base.type = type;
        }

        public override void Shoot(Transform shooter)
        {
            var rotation = shooter.rotation;
            var position = shooter.position;
            bulletPool.Acquire(position, rotation, type);
            
            var euler = rotation.eulerAngles;
            bulletPool.Acquire(position,Quaternion.Euler(euler.x, euler.y, euler.z+90), type);
            bulletPool.Acquire(position, Quaternion.Euler(euler.x, euler.y, euler.z+180) , type);
            bulletPool.Acquire(position, Quaternion.Euler(euler.x, euler.y, euler.z+270) , type);
        }
    }
}