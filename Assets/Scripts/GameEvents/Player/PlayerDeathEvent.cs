using GameplayElements.User;

namespace GameEvents.Player
{
    public class PlayerDeathEvent : GameEvent
    {
        public PlayerDeathEvent(int score)
        {
            name = PlayerEventNames.PlayerDeath;
            parameters["FinalScore"] = score.ToString();
        }
    }
}