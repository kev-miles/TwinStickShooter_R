﻿using System;
using GameplayElements.ShootingStrategies;
using Infrastructure.Interfaces;
using UnityEngine;

namespace GameplayElements.Enemies
{
    public class EnemyView : MonoBehaviour, Poolable
    {
        [SerializeField] private Rigidbody2D rigidbody = default;
        [SerializeField] private SpriteRenderer[] graphics = new SpriteRenderer[2];
        [SerializeField] private GameObject explosionEffect = default;

        public ShootingStrategy strategy;
        public EnemyEntityPool origin;

        private EnemyPresenter _presenter;
        private float _movementSpeed;
        private Vector2 _nextPosition;
        public Func<Vector3> playerPosition;

        public void SetPresenter(EnemyPresenter presenter)
        {
            _presenter = presenter;
            OnAcquire();
        }
        
        public void OnAcquire()
        {
            _presenter.ApplyShootingStrategy(strategy);
        }

        public void OnRelease()
        {
            foreach (var renderer in graphics)
            {
                renderer.enabled = true;
            }
            explosionEffect.SetActive(false);
            origin.Release(this);
        }

        public void ShowDeath()
        {
            foreach (var renderer in graphics)
            {
                renderer.enabled = false;
            }
            explosionEffect.SetActive(true);
            OnRelease();
        }
        
        public void MoveTo(Vector2 position, float speed)
        {
            _movementSpeed = speed;
            _nextPosition = position;
        }
        
        public void Shoot(ShootingStrategy strategy)
        {
            strategy.Shoot(transform);
        }

        public void Damage()
        {
            _presenter.Damage();
        }

        private void FixedUpdate()
        {
            Rotate();
            Move();
        }
        
        private void Move()
        {
            rigidbody.velocity = new Vector2(_nextPosition.x * _movementSpeed, _nextPosition.y * _movementSpeed);
        }
        
        private void Rotate()
        {
            var direction = (playerPosition.Invoke() - transform.position).normalized;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}