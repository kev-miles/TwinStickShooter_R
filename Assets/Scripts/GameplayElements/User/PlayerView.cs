using System;
using GameplayElements.ShootingStrategies;
using UnityEngine;

namespace GameplayElements.User
{
    public class PlayerView : MonoBehaviour
    {
        public Action OnUpdate = () => {};
        
        [SerializeField] private Rigidbody2D rigidbody = default;
        [SerializeField] private SpriteRenderer[] graphics = new SpriteRenderer[2];
        [SerializeField] private GameObject explosionEffect = default;

        private PlayerPresenter _presenter;
        private float _movementSpeed;
        private Vector2 _nextPosition;

        public void SetPresenter(PlayerPresenter presenter)
        {
            _presenter = presenter;
        }

        public void ShowDeath()
        {
            foreach (var renderer in graphics)
            {
                renderer.enabled = false;
            }
            explosionEffect.SetActive(true);
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

        public void ApplyShootingStrategy(ShootingStrategy strategy)
        {
            _presenter.ApplyShootingStrategy(strategy);
        }
        
        private void Update()
        {
            OnUpdate();
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
            var direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}