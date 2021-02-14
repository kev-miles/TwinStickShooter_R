using System;
using GameEvents;
using UniRx;

namespace GameplayElements.User
{
    public static class PlayerComposer
    {
        public static Player Compose(PlayerView view, IObserver<GameEvent> gameplayObserver, 
            PlayerConfiguration configuration)
        {
            var playerSubject = new Subject<GameEvent>();
            var presenter = new PlayerPresenter(view, playerSubject, configuration);
            var input = new PlayerInput(view, presenter, configuration);

            return new Player(view, presenter, input, gameplayObserver, playerSubject);
        }
    }
}