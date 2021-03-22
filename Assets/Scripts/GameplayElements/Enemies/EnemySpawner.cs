using System;
using System.Collections.Generic;
using System.Linq;
using GameEvents;
using GameEvents.Enemies;
using GameplayElements.Bullets;
using GameplayElements.ShootingStrategies;
using GameplayElements.User;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameplayElements.Enemies
{
    public class EnemySpawner
    {
        private readonly Transform[] _spawnPoints;
        private readonly EnemyEntityPool _enemyPool;
        private readonly BulletPool _enemyBullets;
        private readonly EntityConfiguration _config;
        private Subject<GameEvent> _enemySubject;
        private CompositeDisposable _disposable = new CompositeDisposable();
        private Func<Vector3> _playerPosition;
        
        private int _currentWave = 0;
        private int _enemiesDead = 0;
        private List<EnemyView> _allenemies = new List<EnemyView>();

        public EnemySpawner(EnemyEntityPool enemyPool, Transform[] spawnPoints, Subject<GameEvent> enemyObserver,
            EntityConfiguration configuration, BulletPool enemyBullets, Func<Vector3> playerPosition)
        {
            _enemyPool = enemyPool;
            _spawnPoints = spawnPoints;
            _enemyBullets = enemyBullets;
            _config = configuration;
            _enemySubject = enemyObserver;
            _playerPosition = playerPosition;
            SubscribeToEvents();
            Start();
        }

        public void Start()
        {
            GenerateEnemies();
        }

        public void Stop(Action callback = null)
        {
            ClearEntities(callback);
            ClearDisposables();
        }

        private void ClearEntities(Action callback = null)
        {
            foreach (var enemy in _allenemies)
            {
                enemy.OnRelease();
            }

            callback?.Invoke();
        }

        private void SubscribeToEvents()
        {
            _enemySubject.Subscribe(e =>
            {
                if (e.name == EventNames.EnemyKilled)
                {
                    _enemiesDead++;
                }
            });
        }
        
        private void OnGameFinished()
        {
            Stop();
            _enemySubject.OnNext(EnemyEvent.AllWavesFinished());
        }

        private void GenerateEnemies()
        {
            Spawn();
            Observable.Interval(TimeSpan.FromSeconds(1))
                .AsUnitObservable()
                .Do(_ =>
                {
                    if(ShouldSpawn())
                        Spawn();
                    else if (IsObjectiveMet())
                    {
                        ClearEntities();
                        CheckGameEnd();
                    }
                })
                .Subscribe()
                .AddTo(_disposable);
        }

        private void Spawn()
        {
            var strategy = new BlossomingMultiShot().WithPool(_enemyBullets).WithType(BulletType.Enemy);
            var spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
            var enemyView = _enemyPool.Acquire(spawnPoint, _playerPosition, strategy);
            enemyView.SetPresenter(new EnemyPresenter(enemyView, _enemySubject, _config, _enemyBullets));
            _allenemies.Add(enemyView);
        }

        private bool IsObjectiveMet()
        {
            return _enemiesDead >= _config.EnemiesPerWave[_currentWave];
        }

        private bool ShouldSpawn()
        {
            return _allenemies.Count+1 <= _config.EnemiesPerWave[_currentWave];
        }

        private void CheckGameEnd()
        {
            if(_currentWave == _config.EnemiesPerWave.Length-1)
                OnGameFinished();
            else
                ResetWave();
        }

        private void ResetWave()
        {
            _enemySubject.OnNext(EnemyEvent.WaveFinished());
            Observable.Timer(TimeSpan.FromSeconds(_config.WaveResetTime))
                .Do(_ =>
                {
                    _currentWave = (_currentWave + 1) % _config.EnemiesPerWave.Length;
                    _allenemies.Clear();
                    _enemiesDead = 0;
                    _enemySubject.OnNext(EnemyEvent.WaveStart());
                })
                .Subscribe()
                .AddTo(_disposable);
        }

        private void ClearDisposables()
        {
            _disposable.Clear();
        }
    }
}
