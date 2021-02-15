namespace GameEvents.Screen
{
    public class TransitionInEvent : GameEvent
    {
        public TransitionInEvent(int scene)
        {
            parameters["NewScene"] = scene.ToString();
        }
    }
}