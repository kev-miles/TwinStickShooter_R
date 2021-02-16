using GameplayElements.User;

namespace GameEvents.Player
{
    public class PlayerVictoryEvent : GameEvent
    {
        public PlayerVictoryEvent(int score)
        {
            name = EventNames.PlayerVictory;
            parameters["FinalScore"] = score.ToString();
        }
    }
}