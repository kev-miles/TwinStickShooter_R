using UnityEngine;

namespace GameplayElements.Bullets.Strategies
{
    public class BlossomingBullet : BulletStrategy
    {
        public override void Execute(Rigidbody2D rigidbody, Transform transform)
        {
            rigidbody.velocity = (transform.right * Mathf.Sqrt(2)) * 2f;
        }
    }
}