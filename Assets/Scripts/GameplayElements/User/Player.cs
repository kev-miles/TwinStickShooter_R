﻿using System;
using System.Collections.Generic;
using GameEvents;
using UniRx;

namespace GameplayElements.User
{
    public class Player
    {
        private PlayerView _view;
        private PlayerPresenter _presenter;
        private PlayerInput _input;
        private IObserver<GameEvent> _gameplayObserver;
        private IObservable<GameEvent> _playerObservable;

        private Dictionary<string, Action<GameEvent>> _eventMap = new Dictionary<string, Action<GameEvent>>();

        public Player(PlayerView view, PlayerPresenter presenter, PlayerInput input, 
            IObserver<GameEvent> gameplayObserver, IObservable<GameEvent> playerObservable)
        {
            _view = view;
            _presenter = presenter;
            _input = input;
            _gameplayObserver = gameplayObserver;
            _playerObservable = playerObservable;
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            _playerObservable
                .Do(onNext: e => _gameplayObserver.OnNext(e))
                .Subscribe();
        }
    }
}