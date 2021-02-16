using GameplayElements.User;

namespace GameEvents.Player
{
    public class PlayerDeathEvent : GameEvent
    {
        public PlayerDeathEvent(int score)
        {
            name = EventNames.PlayerKilled;
            parameters["FinalScore"] = score.ToString();
        }
    }
}