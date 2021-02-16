using GameplayElements.User;

namespace GameEvents.Enemies
{
    public class WaveFinishedEvent : GameEvent
    {
        public WaveFinishedEvent()
        {
            name = EventNames.WaveFinished;
        }
    }
}