using UnityEngine;

namespace GameplayElements.ShootingStrategies
{
    public class MultiShot : ShootingStrategy
    {
        public MultiShot() : base()
        {
            base.name = "MultiShot";
        }
        
        public override void Shoot(Transform shooter)
        {
            var rotation = shooter.rotation;
            var position = shooter.position;
            pool.Acquire(position, rotation, type);
            
            var euler = rotation.eulerAngles;
            pool.Acquire(position,Quaternion.Euler(euler.x, euler.y, euler.z-10), type);
            pool.Acquire(position,Quaternion.Euler(euler.x, euler.y, euler.z-20), type);
            pool.Acquire(position, Quaternion.Euler(euler.x, euler.y, euler.z+10) , type);
            pool.Acquire(position,Quaternion.Euler(euler.x, euler.y, euler.z+20), type);
        }
    }
}