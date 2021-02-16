using System;
using GameEvents;
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
        private IObservable<bool> _enemyGenerator;

        private bool _isPlaying = true;

        public EnemySpawner(EnemyEntityPool enemyPool, Transform[] spawnPoints, IObserver<GameEvent> enemyObserver,
            EntityConfiguration configuration, BulletPool enemyBullets)
        {
            _enemyPool = enemyPool;
            _spawnPoints = spawnPoints;
            _enemyBullets = enemyBullets;
            _config = configuration;
            _observer = enemyObserver;
        }

        public void GenerateEnemies()
        {
            //TODO: Use observable to build generator
            //_enemyGenerator.TakeWhile(_isPlaying).Subscribe();
        }

        public void Stop()
        {
            _isPlaying = false;
        }

        private void Spawn()
        {
            var strategy = new OmniShot().WithPool(_enemyBullets).WithType(BulletType.Enemy);
            var spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;
            var enemyView = _enemyPool.Acquire(spawnPoint, strategy);
            enemyView.SetPresenter(new EnemyPresenter(enemyView, _observer, _config, _enemyBullets));
        }
    }
}