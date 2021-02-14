using GameplayElements.User;
using Infrastructure;

namespace GameEvents
{
    public class PlayerShootEvent : GameEvent
    {
        public PlayerShootEvent()
        {
            name = PlayerEventNames.PlayerShoot;
        }
    }
}