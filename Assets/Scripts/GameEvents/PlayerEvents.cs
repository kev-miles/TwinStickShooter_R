using Infrastructure;

namespace GameEvents
{
    public static class PlayerEvent
    {
        public static PlayerExitEvent Exit() => new PlayerExitEvent();

        public static GameEvent Shoot() => new PlayerShootEvent();
    }
}