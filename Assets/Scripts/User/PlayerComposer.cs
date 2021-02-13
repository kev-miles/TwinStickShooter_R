using GameEvents;
using UniRx;

namespace User
{
    public static class PlayerComposer
    {
        public static Player Compose(PlayerView view, PlayerConfiguration configuration)
        {
            var playerObserver = new Subject<PlayerEvent>();
            var presenter = new PlayerPresenter(view, playerObserver, configuration);
            var input = new PlayerInput(view, presenter, playerObserver, configuration);

            return new Player(view, presenter, input, playerObserver);
        }
    }
}