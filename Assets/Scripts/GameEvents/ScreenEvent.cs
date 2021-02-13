using Infrastructure;

namespace GameEvents
{
    public static class ScreenEvent
    {
        public static TransitionInEvent TransitionIn(int scene) => new TransitionInEvent(scene);
        public static TransitionInEvent TransitionIn(SceneId scene) => new TransitionInEvent((int)scene);
        public static TransitionOutEvent TransitionOut(int scene) => new TransitionOutEvent(scene);
        public static TransitionOutEvent TransitionOut(SceneId scene) => new TransitionOutEvent((int)scene);
    }
}