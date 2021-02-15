using GameplayElements.ShootingStrategies;
using GameplayElements.User;
using UnityEngine;

namespace GameplayElements
{
    public class PowerUp : MonoBehaviour
    {
        public PowerUpSpawner Origin;
        
        private Vector2 offPosition = new Vector2(100, 100);
        private ShootingStrategy[] strategies =
        {
            new BurstShot(),
            new CrusaderShot(),
            new MultiShot()
        };

        public void Spawn(Vector3 position)
        {
            gameObject.transform.position = position;
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            collision.gameObject.GetComponent<PlayerView>().ApplyShootingStrategy(strategies[Random.Range(0,strategies.Length)]);
            gameObject.transform.position = offPosition;
            Origin.ClearActivePowerUp();
        }
    }
}