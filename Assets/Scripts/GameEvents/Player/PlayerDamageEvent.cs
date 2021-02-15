using GameplayElements.User;

namespace GameEvents.Player
{
    public class PlayerDamageEvent : GameEvent
    {
        public PlayerDamageEvent(int hp)
        {
            name = PlayerEventNames.PlayerDamaged;
            parameters["HP"] = hp.ToString();
        }
    }
}