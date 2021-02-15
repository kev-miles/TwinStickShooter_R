namespace GameEvents.Screen
{
    public class ScreenShownEvent : GameEvent
    {
        public ScreenShownEvent(int sceneId)
        {
            parameters["Scene"] = sceneId.ToString();
        }
    }
}