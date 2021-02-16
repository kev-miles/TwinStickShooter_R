using System;
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
        private IObserver<GameEvent> _observer;
        private CompositeDisposable _disposable = new CompositeDisposable();
        private Func<Vector3> _playerPosition;
        
        private int _currentWave = 0;
        private int _enemiesSpawned = 0;

        public EnemySpawner(EnemyEntityPool enemyPool, Transform[] spawnPoints, IObserver<GameEvent> enemyObserver,
            EntityConfiguration configuration, BulletPool enemyBullets, Func<Vector3> playerPosition)
        {
            _enemyPool = enemyPool;
            _spawnPoints = spawnPoints;
            _enemyBullets = enemyBullets;
            _config = configuration;
            _observer = enemyObserver;
            _playerPosition = playerPosition;
            Start();
        }

        public void Start()
        {
            GenerateEnemies(WhenDone());
        }

        public void Stop()
        {
            ClearDisposables();
        }
        
        private void OnGameFinished()
        {
            Stop();
            _observer.OnNext(EnemyEvent.AllWavesFinished());
        }

        private void GenerateEnemies(IObservable<Unit> whenDone)
        {
            Observable.Interval(TimeSpan.FromSeconds(1))
                .AsUnitObservable()
                .Concat(whenDone)
                .TakeUntil(whenDone)
                .Do(_ => Spawn())
                .Subscribe()
                .AddTo(_disposable);

            whenDone
                .Do(_ => CheckGameEnd())
                .Subscribe()
                .AddTo(_disposable);
        }

        private void Spawn()
        {
            var strategy = new OmniShot().WithPool(_enemyBullets).WithType(BulletType.Enemy);
            var spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
            var enemyView = _enemyPool.Acquire(spawnPoint, _playerPosition, strategy);
            enemyView.SetPresenter(new EnemyPresenter(enemyView, _observer, _config, _enemyBullets));
        }

        private IObservable<Unit> WhenDone()
        {
            return Observable.Create<Unit>(emitter =>
                {
                    if (IsObjectiveMet())
                    {
                        _observer.OnNext(EnemyEvent.WaveFinished());
                        emitter.OnNext(Unit.Default);
                        emitter.OnCompleted();
                    }
                    return Disposable.Empty;
                });
        }

        private bool IsObjectiveMet()
        {
            return _config.EnemiesPerWave[_currentWave] <= _enemiesSpawned;
        }

        private void CheckGameEnd()
        {
            if(_currentWave == _config.EnemiesPerWave.Length)
                OnGameFinished();
            else
                ResetWave();
        }

        private void ResetWave()
        {
            Observable.Timer(TimeSpan.FromSeconds(3))
                .Do(_ =>
                {
                    Stop();
                    _currentWave++;
                    _enemiesSpawned = 0;
                    Start();
                })
                .Subscribe();
        }

        private void ClearDisposables()
        {
            _disposable.Clear();
        }
    }
}