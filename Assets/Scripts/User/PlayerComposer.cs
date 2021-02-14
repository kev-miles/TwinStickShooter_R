using Infrastructure;
using UniRx;

namespace User
{
    public static class PlayerComposer
    {
        public static Player Compose(PlayerView view, PlayerConfiguration configuration)
        {
            var playerObserver = new Subject<GameEvent>();
            var presenter = new PlayerPresenter(view, playerObserver, configuration);
            var input = new PlayerInput(view, presenter, configuration);

            return new Player(view, presenter, input, playerObserver);
        }
    }
}