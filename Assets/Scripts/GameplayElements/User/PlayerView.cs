using System;
using GameplayElements.Bullets;
using GameplayElements.Strategies;
using UnityEngine;

namespace GameplayElements.User
{
    public class PlayerView : MonoBehaviour
    {
        public Action OnUpdate = () => {};
        
        [SerializeField] private BulletPool pool = default;
        [SerializeField] private Rigidbody2D _rigidbody = default;

        private float _movementSpeed;
        private Vector2 _nextPosition;
        
        public void MoveTo(Vector2 position, float speed)
        {
            _movementSpeed = speed;
            _nextPosition = position;
        }
        
        public void Shoot(ShootingStrategy strategy)
        {
            strategy.Shoot(transform);
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
            _rigidbody.velocity = new Vector2(_nextPosition.x * _movementSpeed, _nextPosition.y * _movementSpeed);
        }
        
        private void Rotate()
        {
            var direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}