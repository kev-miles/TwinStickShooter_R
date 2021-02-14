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
            view.OnUpdate += HandleInput;
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
            {
                _presenter.Shoot();
            }
        }

        private void Movement()
        {
            var x = Input.GetAxisRaw("Horizontal");
            var y = Input.GetAxisRaw("Vertical");
            var dir = new Vector2(x, y).normalized;
            _presenter.Move(dir);
        }

        private void ExitGameplay()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                _presenter.ExitGameplay();
        }
    }
}