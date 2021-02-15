using System;
using GameEvents;
using GameplayElements.Bullets;
using GameplayElements.Strategies;
using UnityEngine;

namespace GameplayElements.User
{
    public class PlayerPresenter
    {
        private readonly PlayerView _view;
        private readonly PlayerConfiguration _config;
        private readonly IObserver<GameEvent> _observer;
        private readonly BulletPool _pool;

        private ShootingStrategy _shootingStrategy;

        private int _hp = 5;
        private int _score = 0;

        public PlayerPresenter(PlayerView view, IObserver<GameEvent> playerObserver,
            PlayerConfiguration playerConfiguration, BulletPool pool)
        {
            _view = view;
            _config = playerConfiguration;
            _observer = playerObserver;
            _pool = pool;
            ApplyShootingStrategy(new RegularShot(_pool, BulletType.Player));
        }

        public void Move(Vector2 to)
        {
            _view.MoveTo(to, _config.Speed);
        }

        public void ExitGameplay()
        {
            _observer.OnNext(PlayerEvent.Exit());
        }

        public void ApplyShootingStrategy(ShootingStrategy strategy)
        {
            _shootingStrategy = strategy;
            _observer.OnNext(PlayerEvent.PowerUp(strategy.Name));
        }

        public void Shoot()
        {
            _observer.OnNext(PlayerEvent.Shoot());
            _view.Shoot(_shootingStrategy);
        }
    }
}