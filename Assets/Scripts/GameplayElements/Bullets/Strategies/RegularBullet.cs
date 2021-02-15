﻿using UnityEngine;

namespace GameplayElements.Bullets
{
    public class RegularBullet : BulletStrategy
    {
        public override void Execute(Rigidbody2D rigidbody, Transform transform)
        {
            rigidbody.velocity = transform.right * 15f;
        }
    }
}