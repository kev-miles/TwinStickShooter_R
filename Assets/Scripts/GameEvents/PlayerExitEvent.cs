using GameplayElements.User;
using Infrastructure;

namespace GameEvents
{
    public class PlayerExitEvent : GameEvent
    {
        public PlayerExitEvent()
        {
            name = PlayerEventNames.PlayerExit;
        }
    }
}