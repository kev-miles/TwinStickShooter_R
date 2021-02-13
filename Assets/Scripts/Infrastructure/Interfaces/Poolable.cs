namespace Infrastructure
{
    public interface Poolable
    {
        void OnAcquire();
        void OnRelease();
    }
}