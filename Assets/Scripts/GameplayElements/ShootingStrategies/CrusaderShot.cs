using UnityEngine;

namespace GameplayElements.ShootingStrategies
{
    public class CrusaderShot : ShootingStrategy
    {
        public CrusaderShot() : base()
        {
            base.name = "Crusader";
        }

        public override void Shoot(Transform shooter)
        {
            var rotation = shooter.rotation;
            var position = shooter.position;
            var euler = rotation.eulerAngles;
            
            pool.Acquire(position,Quaternion.Euler(euler.x, euler.y, euler.z+5), type);
            pool.Acquire(position,Quaternion.Euler(euler.x, euler.y, euler.z-5), type);
            pool.Acquire(position,Quaternion.Euler(euler.x, euler.y, euler.z+85), type);
            pool.Acquire(position,Quaternion.Euler(euler.x, euler.y, euler.z+95), type);
            pool.Acquire(position, Quaternion.Euler(euler.x, euler.y, euler.z+175) , type);
            pool.Acquire(position, Quaternion.Euler(euler.x, euler.y, euler.z+185) , type);
            pool.Acquire(position, Quaternion.Euler(euler.x, euler.y, euler.z+265) , type);
            pool.Acquire(position, Quaternion.Euler(euler.x, euler.y, euler.z+275) , type);
        }
    }
}