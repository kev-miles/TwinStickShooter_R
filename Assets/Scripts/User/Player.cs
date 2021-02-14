using System;
using System.Collections.Generic;
using Infrastructure;
using UniRx;

namespace User
{
    public class Player
    {
        public Action<GameEvent> OnPlayerDeath = (GameEvent p) => {  };
        public Action<GameEvent> OnPlayerDamaged = (GameEvent p) => {  };
        public Action<GameEvent> OnPlayerHeal= (GameEvent p) => {  };
        public Action<GameEvent> OnPlayerShoot = (GameEvent p) => {  };
        public Action<GameEvent> OnPlayerExit = (GameEvent p) => {  };
        
        private PlayerView _view;
        private PlayerPresenter _presenter;
        private PlayerInput _input;
        private IObservable<GameEvent> _observable;

        private Dictionary<string, Action<GameEvent>> _eventMap = new Dictionary<string, Action<GameEvent>>();

        public Player(PlayerView view, PlayerPresenter presenter, PlayerInput input, IObservable<GameEvent> playerObservable)
        {
            _view = view;
            _presenter = presenter;
            _input = input;
            _observable = playerObservable;
            SetupMap();
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            _observable
                .Do(onNext: e => _eventMap[e.name](e))
                .Subscribe();
        }

        private void SetupMap()
        {
            _eventMap[PlayerEventNames.PlayerDeath] = OnPlayerDeath;
            _eventMap[PlayerEventNames.PlayerDamaged] = OnPlayerDamaged;
            _eventMap[PlayerEventNames.PlayerHealed] = OnPlayerHeal;
            _eventMap[PlayerEventNames.PlayerShoot] = OnPlayerShoot;
            _eventMap[PlayerEventNames.PlayerExit] = OnPlayerExit;
        }
    }
}