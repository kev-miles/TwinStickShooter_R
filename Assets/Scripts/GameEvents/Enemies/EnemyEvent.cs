using GameEvents.Player;
using GameplayElements.User;

namespace GameEvents.Enemies
{
    public static class EnemyEvent
    {
        public static EnemyDeathEvent Death() => new EnemyDeathEvent();
        public static WaveFinishedEvent WaveFinished() => new WaveFinishedEvent();
        public static AllWavesFinishedEvent AllWavesFinished() => new AllWavesFinishedEvent();

        public static GameEvent WaveStart() => new GameEvent(EventNames.WaveStart);
    }
}