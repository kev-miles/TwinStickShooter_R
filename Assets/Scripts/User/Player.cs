using System;
using System.Collections.Generic;
using System.Linq;
using GameEvents;
using UniRx;

namespace User
{
    public class Player
    {
        public Action<int> OnPlayerDeath = (int p) => {  };
        public Action<int> OnPlayerDamaged = (int p) => {  };
        public Action<int> OnPlayerHeal= (int p) => {  };
        public Action<int> OnPlayerShoot = (int p) => {  };
        
        private PlayerView _view;
        private PlayerPresenter _presenter;
        private PlayerInput _input;
        private IObservable<PlayerEvent> _observable;

        private Dictionary<string, Action<int>> _eventMap = new Dictionary<string, Action<int>>();

        public Player(PlayerView view, PlayerPresenter presenter, PlayerInput input, IObservable<PlayerEvent> playerObservable)
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
                .Do(onNext: e => _eventMap[e.name](int.Parse(e.parameters.FirstOrDefault().ToString())))
                .Subscribe();
        }

        private void SetupMap()
        {
            _eventMap["PlayerDeath"] = OnPlayerDeath;
            _eventMap["PlayerDamaged"] = OnPlayerDamaged;
            _eventMap["PlayerHealed"] = OnPlayerHeal;
            _eventMap["PlayerShoot"] = OnPlayerShoot;
        }
    }
}