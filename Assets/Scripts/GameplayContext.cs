using System;
using GameEvents;
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

    public void Initialize(GameScreen gameScreen)
    {
        _screen = gameScreen;
        _playerEventsObservable = new Subject<GameEvent>();
        _enemyEventsObservable = new Subject<GameEvent>();
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
            .Where(e => e.name == PlayerEventNames.EnemyKilled)
            .Do(_ => SpawnPowerUpOnChance())
            .Subscribe(EndGameOnPlayerVictory);
    }
    
    private void SubscribeToPlayerEvents()
    {
        _playerEventsObservable
            .Do(BroadcastEvents)
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
            playerElements.Configuration, gameElements.EnemyBulletPool);
    }

    private void SpawnPowerUpOnChance()
    {
        var chance = UnityEngine.Random.Range(0, 1);
        if(chance > playerElements.Configuration.PowerUpSpawnChance)
            _powerUps.SpawnPowerUp();
    }

    private void EndGameOnPlayerVictory(GameEvent victoryEvent)
    {
        
    }
}
