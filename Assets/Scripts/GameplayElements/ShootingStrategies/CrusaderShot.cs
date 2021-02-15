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
            
            pool.Acquire(position, rotation, type);
            pool.Acquire(position,Quaternion.Euler(euler.x, euler.y, euler.z+90), type);
            pool.Acquire(position, Quaternion.Euler(euler.x, euler.y, euler.z+180) , type);
            pool.Acquire(position, Quaternion.Euler(euler.x, euler.y, euler.z+270) , type);
        }
    }
}