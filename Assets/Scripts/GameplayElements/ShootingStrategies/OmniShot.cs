using GameplayElements.Bullets;
using UnityEngine;

namespace GameplayElements.ShootingStrategies
{
    public class OmniShot : ShootingStrategy
    {
        public OmniShot() : base()
        {
            base.name = "OmniShot";
        }

        public override void Shoot(Transform shooter)
        {
            var rotation = shooter.rotation;
            var position = shooter.position;
            pool.Acquire(position, rotation, type);
            
            var euler = rotation.eulerAngles;
            pool.Acquire(position,Quaternion.Euler(euler.x, euler.y, euler.z+45), type);
            pool.Acquire(position, Quaternion.Euler(euler.x, euler.y, euler.z+90) , type);
            pool.Acquire(position, Quaternion.Euler(euler.x, euler.y, euler.z+135) , type);
            pool.Acquire(position, Quaternion.Euler(euler.x, euler.y, euler.z+180) , type);
            pool.Acquire(position, Quaternion.Euler(euler.x, euler.y, euler.z+225) , type);
            pool.Acquire(position, Quaternion.Euler(euler.x, euler.y, euler.z+270) , type);
            pool.Acquire(position, Quaternion.Euler(euler.x, euler.y, euler.z+315) , type);
        }
    }
}