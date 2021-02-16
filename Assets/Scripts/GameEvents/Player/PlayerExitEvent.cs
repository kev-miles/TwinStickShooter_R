using GameplayElements.User;

namespace GameEvents.Player
{
    public class PlayerExitEvent : GameEvent
    {
        public PlayerExitEvent()
        {
            name = EventNames.PlayerExit;
        }
    }
}