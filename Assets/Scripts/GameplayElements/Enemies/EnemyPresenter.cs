using System;
using GameEvents;
using GameEvents.Enemies;
using GameEvents.Player;
using GameplayElements.Bullets;
using GameplayElements.ShootingStrategies;
using GameplayElements.User;
using UnityEngine;

namespace GameplayElements.Enemies
{
    public class EnemyPresenter
    {
        private readonly EnemyView _view;
        private readonly EntityConfiguration _config;
        private readonly IObserver<GameEvent> _observer;
        private readonly BulletPool _pool;

        private ShootingStrategy _shootingStrategy;

        private int _hp;

        public EnemyPresenter(EnemyView view, IObserver<GameEvent> enemyObserver,
            EntityConfiguration entityConfiguration, BulletPool pool)
        {
            _view = view;
            _config = entityConfiguration;
            _observer = enemyObserver;
            _pool = pool;
            _hp = _config.EnemyHp;
            ApplyShootingStrategy(new RegularShot().WithPool(pool).WithType(BulletType.Enemy));
        }

        public void Damage()
        {
            _hp--;
            _observer.OnNext(PlayerEvent.Damage(_hp));

            if (_hp == 0)
                Death();
        }

        public void Move(Vector2 to)
        {
            _view.MoveTo(to, _config.EnemySpeed);
        }
        
        public void Shoot()
        {
            _view.Shoot(_shootingStrategy);
        }
        
        public void ApplyShootingStrategy(ShootingStrategy strategy)
        {
            _shootingStrategy = strategy;
        }

        private void Death()
        {
            _observer.OnNext(EnemyEvents.Death());
            _view.ShowDeath();
        }
    }
}