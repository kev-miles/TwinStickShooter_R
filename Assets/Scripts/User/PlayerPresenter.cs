using System;
using GameEvents;
using Infrastructure;
using UnityEngine;

namespace User
{
    public class PlayerPresenter
    {
        private readonly PlayerView _view;
        private readonly PlayerConfiguration _config;
        private readonly IObserver<GameEvent> _observer;
        
        private int _score = 0;
        
        public PlayerPresenter(PlayerView view, IObserver<GameEvent> playerObserver,
            PlayerConfiguration playerConfiguration)
        {
            _view = view;
            _config = playerConfiguration;
            _observer = playerObserver;
        }

        public void Move(Vector2 to)
        {
            _view.MoveTo(to);
        }

        public void ExitGameplay()
        {
            _observer.OnNext(PlayerEvent.Exit());
        }
    }
}