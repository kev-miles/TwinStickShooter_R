using GameplayElements.User;

namespace GameEvents.Enemies
{
    public class EnemyDeathEvent : GameEvent
    {
        public EnemyDeathEvent()
        {
            name = PlayerEventNames.EnemyKilled;
        }
    }
}