using GameplayElements.User;

namespace GameEvents.Enemies
{
    public class AllWavesFinishedEvent : GameEvent
    {
        public AllWavesFinishedEvent()
        {
            name = EventNames.AllWavesFinished;
        }
    }
}