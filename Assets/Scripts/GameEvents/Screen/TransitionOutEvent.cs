namespace GameEvents.Screen
{
    public class TransitionOutEvent : GameEvent
    {
        public TransitionOutEvent(int scene)
        {
            parameters["NewScene"] = scene.ToString();
        }
    }
}