using GameEvents;

namespace GameplayElements
{
    public interface VictoryCondition
    {
        bool CheckCondition(GameEvent gameEvent);
    }
}