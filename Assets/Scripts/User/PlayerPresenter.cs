using GameEvents;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace User
{
    public class PlayerPresenter
    {
        private PlayerView _view;
        private PlayerConfiguration _config;

        public PlayerPresenter(PlayerView view, Subject<PlayerEvent> playerObserver,
            PlayerConfiguration playerConfiguration)
        {
            _view = view;
            _config = playerConfiguration;
        }

        public void Move(Vector2 to)
        {
            _view.MoveTo(to);
        }
    }
}