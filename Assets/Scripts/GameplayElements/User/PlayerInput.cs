using UnityEngine;
using UnityEngine.UIElements;

namespace GameplayElements.User
{
    public class PlayerInput
    {
        private PlayerPresenter _presenter;
        private PlayerConfiguration _config;
        public PlayerInput(PlayerView view, PlayerPresenter presenter,
            PlayerConfiguration playerConfiguration)
        {
            _presenter = presenter;
            _config = playerConfiguration;
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
            ExitGameplay();
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

        private void ExitGameplay()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                _presenter.ExitGameplay();
        }
    }
}