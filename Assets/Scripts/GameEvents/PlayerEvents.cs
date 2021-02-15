using Infrastructure;

namespace GameEvents
{
    public static class PlayerEvent
    {
        public static PlayerExitEvent Exit() => new PlayerExitEvent();
        public static PowerUpEvent PowerUp(string powerUpName) => new PowerUpEvent(powerUpName);

        public static PlayerShootEvent Shoot() => new PlayerShootEvent();
    }
}