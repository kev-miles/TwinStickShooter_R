using GameEvents;
using UniRx;
using UnityEngine;

namespace User
{
    public class PlayerInput
    {
        private PlayerPresenter _presenter;
        private PlayerConfiguration _config;
        public PlayerInput(PlayerView view, PlayerPresenter presenter, Subject<PlayerEvent> playerObserver,
            PlayerConfiguration playerConfiguration)
        {
            _presenter = presenter;
            _config = playerConfiguration;
            view.OnUpdate += HandleInput;
        }
        void HandleInput()
        {
            var x = Input.GetAxisRaw("Horizontal");
            var y = Input.GetAxisRaw("Vertical");
            var dir = new Vector2(x, y).normalized;
            _presenter.Move(dir);
        }
    }
}