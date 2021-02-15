namespace GameEvents.Player
{
    public static class PlayerEvent
    {
        public static PlayerExitEvent Exit() => new PlayerExitEvent();
        public static PlayerDamageEvent Damage(int hp) => new PlayerDamageEvent(hp);
        public static UpdateScoreEvent UpdateScore(int newScore) => new UpdateScoreEvent(newScore);
        public static PowerUpEvent PowerUp(string powerUpName) => new PowerUpEvent(powerUpName);

        public static PlayerShootEvent Shoot() => new PlayerShootEvent();
    }
}