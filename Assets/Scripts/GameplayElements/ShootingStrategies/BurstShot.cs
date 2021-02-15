using System;
using GameplayElements.Bullets;
using UniRx;
using UnityEngine;

namespace GameplayElements.ShootingStrategies
{
    public class BurstShot : ShootingStrategy
    {
        public BurstShot(BulletPool pool, BulletType type) : base(pool, type)
        {
            base.name = "Burst";
            base.bulletPool = pool;
            base.type = type;
        }
        
        public override void Shoot(Transform shooter)
        {
            var shotDelay = 0.05f;
            var rotation = shooter.rotation;
            var position = shooter.position;

            bulletPool.Acquire(position, rotation, type);
            Observable.Timer(TimeSpan.FromSeconds(shotDelay))
                .Do(_ => bulletPool.Acquire(position, rotation, type))
                .Delay(TimeSpan.FromSeconds(shotDelay))
                .Do(_ => bulletPool.Acquire(position, rotation, type))
                .Subscribe();
        }
    }
}