using GameplayElements.User;

namespace GameEvents.Player
{
    public class UpdateScoreEvent : GameEvent
    {
        public UpdateScoreEvent(int newScore)
        {
            name = PlayerEventNames.UpdateScore;
            parameters["Score"] = newScore.ToString();
        }
    }
}