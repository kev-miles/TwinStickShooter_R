using System;
using GameEvents;
using GameplayElements.Bullets;
using UniRx;

namespace GameplayElements.User
{
    public static class PlayerComposer
    {
        public static Player Compose(PlayerView view, IObserver<GameEvent> gameplayObserver, 
            PlayerConfiguration configuration, BulletPool pool)
        {
            var playerSubject = new Subject<GameEvent>();
            var presenter = new PlayerPresenter(view, playerSubject, configuration, pool);
            var input = new PlayerInput(view, presenter, configuration);

            return new Player(view, presenter, input, gameplayObserver, playerSubject);
        }
    }
}