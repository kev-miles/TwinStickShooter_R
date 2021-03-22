using System;
using UniRx;
using UnityEngine;

namespace GameplayElements.ShootingStrategies
{
    public class BurstShot : ShootingStrategy
    {
        public BurstShot()
        {
            base.name = "Burst";
        }

        public override void Shoot(Transform shooter)
        {
            var shotDelay = 0.05f;
            var rotation = shooter.rotation;
            var position = shooter.position;

            pool.Acquire(position, rotation, type);
            disposable = Observable.Timer(TimeSpan.FromSeconds(shotDelay))
                .Do(_ => pool.Acquire(position, rotation, type))
                .Delay(TimeSpan.FromSeconds(shotDelay))
                .Do(_ => pool.Acquire(position, rotation, type))
                .Subscribe();
        }
    }
}