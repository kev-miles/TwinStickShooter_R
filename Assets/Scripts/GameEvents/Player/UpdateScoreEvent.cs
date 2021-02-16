using GameplayElements.User;

namespace GameEvents.Player
{
    public class UpdateScoreEvent : GameEvent
    {
        public UpdateScoreEvent(int newScore)
        {
            name = EventNames.UpdateScore;
            parameters["Score"] = newScore.ToString();
        }
    }
}