namespace Infrastructure.Interfaces
{
    public interface Poolable
    {
        void OnAcquire();
        void OnRelease();
    }
}