using System;
using GameplayElements.ShootingStrategies;
using UniRx;

namespace GameplayElements.Enemies.Behaviours
{
    public class ShootBehaviour : Behaviour
    {
        private ShootingStrategy _strategy;
        private EnemyView _view;
        private IDisposable _disposable;

        public ShootBehaviour(ShootingStrategy strategy, EnemyView view)
        {
            _strategy = strategy;
            _view = view;
        }
        
        public void Execute()
        {
            _disposable = Observable.Interval(TimeSpan.FromSeconds(1.5f))
                .Do(_ => CallMethod())
                .Delay(TimeSpan.FromSeconds(0.2f))
                .Do(_ => CallMethod())
                .Delay(TimeSpan.FromSeconds(0.2f))
                .Do(_ => CallMethod())
                .Delay(TimeSpan.FromSeconds(0.2f))
                .Subscribe();
        }

        public void Stop()
        {
            _disposable?.Dispose();
        }
        
        private void CallMethod()
        {
            if(_view != null)
                _view.Shoot(_strategy);
        }
    }
}