using UnityEngine;

namespace GameplayElements.Bullets.Strategies
{
    public abstract class BulletStrategy
    {
        public virtual void Execute(Rigidbody2D rigidbody, Transform transform){}
    }
}