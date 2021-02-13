using Infrastructure;

namespace GameEvents
{
    public class TransitionOutEvent : GameEvent
    {
        public TransitionOutEvent(int scene)
        {
            parameters["NewScene"] = scene.ToString();
        }
    }
}