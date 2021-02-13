using System;
using UnityEngine;

namespace User
{
    public class PlayerView : MonoBehaviour
    {
        public Action OnUpdate = () => {};

        [SerializeField] private PlayerConfiguration _config;
        [SerializeField] private Rigidbody2D _rigidbody;

        private Vector2 _nextPosition;
        
        public void MoveTo(Vector2 position)
        {
            _nextPosition = position;
        }
        private void Update()
        {
            OnUpdate();
        }
        private void FixedUpdate()
        {
            Move();
        }
        private void Move()
        {
            _rigidbody.velocity = new Vector2(_nextPosition.x * _config.Speed, _nextPosition.y * _config.Speed);
        }
    }
}