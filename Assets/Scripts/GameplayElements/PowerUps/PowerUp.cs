using System.Collections.Generic;
using GameplayElements.ShootingStrategies;
using GameplayElements.User;
using UnityEngine;

namespace GameplayElements
{
    public class PowerUp : MonoBehaviour
    {
        public PowerUpSpawner Origin;
        
        private Vector2 offPosition = new Vector2(100, 100);
        private List<ShootingStrategy> strategies = new List<ShootingStrategy>()
        {
            new BurstShot(),
            new CrusaderShot(),
            new MultiShot()
        };

        private ShootingStrategy _strategy;

        public void Spawn(Vector3 position)
        {
            var index = Random.Range(0, strategies.Count);
            var newStrategy = strategies[index];
            if(_strategy != null)
                strategies.Add(_strategy);
            _strategy = newStrategy;
            strategies.RemoveAt(index);
            gameObject.transform.position = position;
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            collision.gameObject.GetComponent<PlayerView>().ApplyShootingStrategy(_strategy);
            gameObject.transform.position = offPosition;
            Origin.ClearActivePowerUp();
        }
    }
}