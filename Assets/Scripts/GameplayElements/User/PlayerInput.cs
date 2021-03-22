using System;
using GameEvents;
using UniRx;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameplayElements.User
{
    public class PlayerInput
    {
        private PlayerPresenter _presenter;
        private IObservable<GameEvent> _observer;
        private bool _receiveInput = true;

        public PlayerInput(PlayerView view, PlayerPresenter presenter, IObservable<GameEvent> playerObserver)
        {
            _presenter = presenter;
            view.OnUpdate += Update;
            _observer = playerObserver;
            SubscribeToObservable();
        }

        private void SubscribeToObservable()
        {
            _observer
                .Where(e => e.name == EventNames.PlayerExit || e.name == EventNames.PlayerKilled)
                .Select(_ => ToggleInput())
                .Where(input => input == false)
                .Do(_ => StopMovement())
                .Subscribe();
        }

        private bool ToggleInput()
        {
            return _receiveInput = !_receiveInput;
        }

        private void Update()
        {
            if (_receiveInput)
                HandleInput();
        }
        
        private void HandleInput()
        {
            ExitGameplay();
            Movement();
            Shoot();
        }
        
        private void ExitGameplay()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                _presenter.ExitGameplay();
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