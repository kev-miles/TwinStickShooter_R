using System;
using System.Collections.Generic;
using System.Linq;
using GameEvents;
using GameEvents.Enemies;
using GameEvents.Player;
using GameplayElements.Bullets;
using GameplayElements.Enemies.Behaviours;
using GameplayElements.ShootingStrategies;
using GameplayElements.User;
using UniRx;
using Behaviour = GameplayElements.Enemies.Behaviours.Behaviour;

namespace GameplayElements.Enemies
{
    public class EnemyPresenter
    {
        private readonly EnemyView _view;
        private readonly EntityConfiguration _config;
        private readonly IObserver<GameEvent> _observer;
        private readonly BulletPool _pool;

        private ShootingStrategy _shootingStrategy;
        private IDisposable _disposable;
        private List<Behaviour> _behaviours = new List<Behaviour>();
        
        private int _hp;
        private int _currentBehaviour;

        public EnemyPresenter(EnemyView view, IObserver<GameEvent> enemyObserver,
            EntityConfiguration entityConfiguration, BulletPool pool)
        {
            _view = view;
            _config = entityConfiguration;
            _observer = enemyObserver;
            _pool = pool;
            _hp = _config.EnemyHp;
        }

        public void Setup(ShootingStrategy strategy)
        {
            _shootingStrategy = strategy;
            _behaviours.Add(new ShootBehaviour(_shootingStrategy, _view));
            _behaviours.Add(new ChaseBehaviour(_config.EnemySpeed, _view));
            ExecuteBehaviours();
        }

        public void OnRelease()
        {
            foreach (var behaviour in _behaviours)
            {
                behaviour.Stop();
            }
            _hp = _config.EnemyHp;
            _disposable.Dispose();
        }
        
        public void Damage()
        {
            if(_hp-1 > 0)
                _hp--;
            else
            {
                _hp--;
                Death();
            }
        }
        
        private void ExecuteBehaviours()
        {
            _behaviours[UnityEngine.Random.Range(0,_behaviours.Count)].Execute();
            _disposable = Observable.Interval(TimeSpan.FromSeconds(5))
                .Do(_ =>
                {
                    _behaviours[_currentBehaviour].Stop();
                    _currentBehaviour = (_currentBehaviour + 1) % _behaviours.Count;
                    _behaviours[_currentBehaviour].Execute();
                }).Subscribe();
        }

        private void Death()
        {
            _observer.OnNext(EnemyEvent.Death());
            _view.ShowDeath();
        }
    }
}