using UniRx.Triggers;
using UnityEngine;

namespace GameplayElements
{
    public class PowerUpSpawner
    {
        private PowerUp _powerUpView;
        private bool powerUpActive;
        private Transform[] _spawnPositions;

        public PowerUpSpawner(PowerUp powerUpView, Transform[] spawnPositions)
        {
            _powerUpView = powerUpView;
            _powerUpView.Origin = this;
            _spawnPositions = spawnPositions;
        }
        
        public void SpawnPowerUp()
        {
            if (powerUpActive) return;
            var position = _spawnPositions[Random.Range(0, _spawnPositions.Length)].position;
            _powerUpView.Spawn(position);
            powerUpActive = true;
        }

        public void ClearActivePowerUp()
        {
            powerUpActive = false;
        }
    }
}