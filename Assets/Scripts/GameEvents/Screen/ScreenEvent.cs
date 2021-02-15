using Infrastructure;

namespace GameEvents.Screen
{
    public static class ScreenEvent
    {
        public static ScreenShownEvent ScreenShown(int scene) => new ScreenShownEvent(scene);
        public static ScreenShownEvent ScreenShown(SceneId scene) => new ScreenShownEvent((int)scene);
        public static TransitionInEvent TransitionIn(int scene) => new TransitionInEvent(scene);
        public static TransitionInEvent TransitionIn(SceneId scene) => new TransitionInEvent((int)scene);
        public static TransitionOutEvent TransitionOut(int scene) => new TransitionOutEvent(scene);
        public static TransitionOutEvent TransitionOut(SceneId scene) => new TransitionOutEvent((int)scene);
    }
}