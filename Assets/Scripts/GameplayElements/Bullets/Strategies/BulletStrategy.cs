using UnityEngine;

namespace GameplayElements.Bullets
{
    public abstract class BulletStrategy
    {
        public virtual void Execute(Rigidbody2D rigidbody, Transform transform){}
    }
}