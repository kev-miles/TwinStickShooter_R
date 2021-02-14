using Infrastructure;
using User;

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