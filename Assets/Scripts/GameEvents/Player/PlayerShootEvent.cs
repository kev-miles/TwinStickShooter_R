using GameplayElements.User;

namespace GameEvents.Player
{
    public class PlayerShootEvent : GameEvent
    {
        public PlayerShootEvent()
        {
            name = EventNames.PlayerShoot;
        }
    }
}