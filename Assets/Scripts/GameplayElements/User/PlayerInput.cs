using UnityEngine;
using UnityEngine.UIElements;

namespace GameplayElements.User
{
    public class PlayerInput
    {
        private PlayerPresenter _presenter;
        private EntityConfiguration _config;
        public PlayerInput(PlayerView view, PlayerPresenter presenter,
            EntityConfiguration entityConfiguration)
        {
            _presenter = presenter;
            _config = entityConfiguration;
            view.OnUpdate += Update;
        }

        private void Update()
        {
            if (_presenter.IsAlive())
                HandleInput();
            else
                StopMovement();
        }
        private void HandleInput()
        {
            Movement();
            Shoot();
        }

        private void Shoot()
        {
            if (Input.GetMouseButtonDown((int)MouseButton.LeftMouse))
                _presenter.Shoot();
        }

        private void Movement()
        {
            var x = Input.GetAxisRaw("Horizontal");
            var y = Input.GetAxisRaw("Vertical");
            var dir = new Vector2(x, y).normalized;
            _presenter.Move(dir);
        }

        private void StopMovement()
        {
            _presenter.Move(Vector2.zero);
        }
    }
}