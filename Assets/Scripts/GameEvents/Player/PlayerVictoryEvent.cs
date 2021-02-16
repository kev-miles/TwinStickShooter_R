using GameplayElements.User;

namespace GameEvents.Player
{
    public class PlayerVictoryEvent : GameEvent
    {
        public PlayerVictoryEvent()
        {
            name = EventNames.PlayerVictory;
        }
    }
}