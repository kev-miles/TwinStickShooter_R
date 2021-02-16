using System;
using System.Collections.Generic;
using GameEvents;
using UniRx;
using UnityEngine;

namespace GameplayElements.User
{
    public class Player
    {
        private PlayerView _view;
        private PlayerPresenter _presenter;
        private PlayerInput _input;
        private IObserver<GameEvent> _gameplayObserver;
        private IObservable<GameEvent> _playerObservable;

        private Dictionary<string, Action> _eventMap = new Dictionary<string, Action>();

        public Player(PlayerView view, PlayerPresenter presenter, PlayerInput input, 
            IObserver<GameEvent> gameplayObserver, IObservable<GameEvent> playerObservable)
        {
            _view = view;
            _presenter = presenter;
            _input = input;
            _gameplayObserver = gameplayObserver;
            _playerObservable = playerObservable;
            SetupGameplayEventMap();
            SubscribeToEvents();
        }
        
        public void ReceiveEvents(GameEvent external)
        {
            if (_eventMap.ContainsKey(external.name))
                _eventMap[external.name]();
        }
        
        public int Score()
        {
            return _presenter.Score;
        }
        
        public Vector3 GetPlayerPosition()
        {
            return _view.transform.position;
        }

        private void SubscribeToEvents()
        {
            _playerObservable
                .Do(onNext: e => _gameplayObserver.OnNext(e))
                .Subscribe();
        }

        private void SetupGameplayEventMap()
        {
            _eventMap[EventNames.EnemyKilled] = _presenter.EnemyKilled;
        }
    }
}