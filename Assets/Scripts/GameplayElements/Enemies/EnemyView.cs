using System;
using System.Linq;
using GameplayElements.ShootingStrategies;
using GameplayElements.User;
using Infrastructure.Interfaces;
using UnityEngine;

namespace GameplayElements.Enemies
{
    public class EnemyView : MonoBehaviour, Poolable
    {
        [SerializeField] private Rigidbody2D rigidbody = default;
        [SerializeField] private SpriteRenderer[] graphics = new SpriteRenderer[2];

        public ShootingStrategy strategy;
        public EnemyEntityPool origin;
        public Func<Vector3> playerPosition;
        
        private EnemyPresenter _presenter;
        private float _movementSpeed;
        private Vector2 _nextPosition;

        public void SetPresenter(EnemyPresenter presenter)
        {
            _presenter = presenter;
            OnAcquire();
        }

        public void OnAcquire()
        {
            _presenter?.Setup(strategy);
        }

        public void OnRelease()
        {
            foreach (var renderer in graphics)
            {
                if(renderer != null)
                    renderer.enabled = true;
            }
            if (_presenter != null)
            {
                _presenter.OnRelease();
                _presenter = null;
            }
            origin.Release(this);
        }

        public void ShowDeath()
        {
            foreach (var renderer in graphics)
            {
                if(renderer != null)
                    renderer.enabled = false;
            }
            OnRelease();
        }

        public void Shoot(ShootingStrategy strategy)
        {
            strategy?.Shoot(transform);
        }

        public void Damage()
        {
            _presenter?.Damage();
        }
        
        public void ToggleMovement(float speed = 0)
        {
            _movementSpeed = speed;
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            graphics.First().color =_movementSpeed > 0 ? Color.red : Color.yellow;
            Vector2 direction = (playerPosition.Invoke() - transform.position).normalized;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            rigidbody.velocity = Vector2.right * _movementSpeed;
            rigidbody.MovePosition((Vector2)transform.position + (direction * _movementSpeed * Time.deltaTime));
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var player = collision.gameObject.GetComponent<PlayerView>();
            if(player != null)
                player.Damage();
        }

        private void OnDestroy()
        {
            _presenter?.DisposeObservables();
        }
    }
}