using System;
using GameEvents;
using GameEvents.Player;
using GameplayElements.Bullets;
using GameplayElements.ShootingStrategies;
using UnityEngine;

namespace GameplayElements.User
{
    public class PlayerPresenter
    {
        private readonly PlayerView _view;
        private readonly EntityConfiguration _config;
        private readonly IObserver<GameEvent> _observer;
        private readonly BulletPool _pool;

        private ShootingStrategy _shootingStrategy;

        private int _hp;
        private int _score = 0;
        
        public int Score => _score;

        public PlayerPresenter(PlayerView view, IObserver<GameEvent> playerObserver,
            EntityConfiguration entityConfiguration, BulletPool pool)
        {
            _view = view;
            _view.SetPresenter(this);
            _config = entityConfiguration;
            _observer = playerObserver;
            _pool = pool;
            _hp = _config.PlayerHp;
            ApplyShootingStrategy(new RegularShot().WithPool(pool).WithType(BulletType.Player));
        }

        public void Damage()
        {
            if (_hp - 1 > 0)
            {
                _hp--;
                _observer.OnNext(PlayerEvent.Damage(_hp));
                UpdateScore(_config.ScoreDamage);
            }
            else
                Death();
        }

        public void Move(Vector2 to)
        {
            _view.MoveTo(to, _config.PlayerSpeed);
        }
        
        public void Shoot()
        {
            _observer.OnNext(PlayerEvent.Shoot());
            _view.Shoot(_shootingStrategy);
        }

        public void ExitGameplay()
        {
            _shootingStrategy.Dispose();
            _observer.OnNext(PlayerEvent.Exit());
        }

        public void EnemyKilled()
        {
            UpdateScore(_config.ScoreEnemy);
        }

        public void ApplyShootingStrategy(ShootingStrategy strategy)
        {
            _shootingStrategy = strategy;
            _shootingStrategy
                .WithPool(_pool)
                .WithType(BulletType.Player);
            UpdateScore(_config.ScorePowerUp);
            _observer.OnNext(PlayerEvent.PowerUp(strategy.Name));
        }

        private void Death()
        {
            UpdateScore(_config.ScoreDeath);
            _shootingStrategy.Dispose();
            _observer.OnNext(PlayerEvent.Death(_score));
            _view.ShowDeath();
        }

        private void UpdateScore(int scoreAmount)
        {
            var sum = _score += scoreAmount;
            _score = sum > 0 ? _score : 0;
            
            _observer.OnNext(PlayerEvent.UpdateScore(_score));
        }
    }
}