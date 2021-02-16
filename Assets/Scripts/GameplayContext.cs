using System;
using GameEvents;
using GameEvents.Player;
using GameplayElements;
using GameplayElements.Enemies;
using GameplayElements.User;
using UniRx;
using UnityEngine;
using Random = System.Random;

public class GameplayContext : MonoBehaviour
{
    [SerializeField] private PlayerElements playerElements = default;
    [SerializeField] private GameElements gameElements = default;

    private GameScreen _screen;
    private Player _player;
    private EnemySpawner _enemies;
    private PowerUpSpawner _powerUps;
    private Subject<GameEvent> _enemyEventsObservable;
    private Subject<GameEvent> _playerEventsObservable;
    private VictoryCondition _victoryCondition;

    public void Initialize(GameScreen gameScreen)
    {
        _screen = gameScreen;
        _playerEventsObservable = new Subject<GameEvent>();
        _enemyEventsObservable = new Subject<GameEvent>();
        _victoryCondition = new WavesVictoryCondition();
        SubscribeToPlayerEvents();
        SetupPlayer();
        SubscribeToEnemyEvents();
        SetupEnemySpawner();
        SetupPowerUpSpawner();
    }

    private void SetupPlayer()
    {
        _player = PlayerComposer.Compose(playerElements.View, _playerEventsObservable,
            playerElements.Configuration, playerElements.BulletPool);
    }

    private void SubscribeToEnemyEvents()
    {
        _enemyEventsObservable
            .Do(_player.ReceiveEvents)
            .Do(e =>
            {
                EndGameOnPlayerVictory(e);
                SpawnPowerUpOnChance(e);
            })
            .Subscribe();
    }
    
    private void SubscribeToPlayerEvents()
    {
        _playerEventsObservable
            .Do(BroadcastEvents)
            .Where(e => e.name == EventNames.PlayerKilled)
            .Do(EndGameOnPlayerDefeat)
            .Subscribe();
    }

    private void BroadcastEvents(GameEvent gameEvent)
    {
        _screen.ReceiveGameplayEvent(gameEvent);
    }
    
    private void SetupPowerUpSpawner()
    {
        _powerUps = new PowerUpSpawner(gameElements.PowerUp, gameElements.SpawnPoints);
    }

    private void SetupEnemySpawner()
    {
        _enemies = new EnemySpawner(gameElements.EnemyEntityPool, gameElements.SpawnPoints, _enemyEventsObservable,
            playerElements.Configuration, gameElements.EnemyBulletPool, () => _player.GetPlayerPosition());
    }

    private void SpawnPowerUpOnChance(GameEvent gameEvent)
    {
        if (gameEvent.name == EventNames.EnemyKilled)
        {
            var chance = UnityEngine.Random.Range(0, 1);
            if(chance > playerElements.Configuration.PowerUpSpawnChance)
                _powerUps.SpawnPowerUp();
        }
    }

    private void EndGameOnPlayerDefeat(GameEvent defeatEvent)
    {
        _enemies.Stop();
    }
    
    private void EndGameOnPlayerVictory(GameEvent victoryEvent)
    {
        if(_victoryCondition.CheckCondition(victoryEvent));
            _playerEventsObservable.OnNext(PlayerEvent.Victory());
    }
}
