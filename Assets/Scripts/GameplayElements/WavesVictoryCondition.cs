using GameEvents;
using GameplayElements.User;

namespace GameplayElements
{
    public class WavesVictoryCondition : VictoryCondition
    {
        public bool CheckCondition(GameEvent gameEvent)
        {
            return gameEvent.name == EventNames.AllWavesFinished;
        }
    }
}