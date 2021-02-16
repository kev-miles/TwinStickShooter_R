using GameplayElements.User;

namespace GameEvents.Player
{
    public class PlayerDamageEvent : GameEvent
    {
        public PlayerDamageEvent(int hp)
        {
            name = EventNames.PlayerDamaged;
            parameters["HP"] = hp.ToString();
        }
    }
}