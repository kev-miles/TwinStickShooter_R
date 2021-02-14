namespace GameplayElements.Bullets
{
    public class PlayerBullets : BulletPool
    {
        private void Awake()
        {
            SetContainerName("Player Bullet Container");
            Load();
        }
    }
}