using System;
using GameEvents;
using GameplayElements;
using GameplayElements.Enemies;
using GameplayElements.User;
using UniRx;
using UnityEngine;

public class GameplayContext : MonoBehaviour
{
    [SerializeField] private PlayerElements playerElements = default;
    [SerializeField] private GameElements gameElements = default;

    private Player _player;
    private PowerUpSpawner _powerUps;
    private GameScreen _screen;
    private Subject<GameEvent> _enemyEventsObservable;
    private Subject<GameEvent> _playerEventsObservable;

    public void Initialize(GameScreen gameScreen)
    {
        _screen = gameScreen;
        _playerEventsObservable = new Subject<GameEvent>();
        _enemyEventsObservable = new Subject<GameEvent>();
        SubscribeToEvents();
        SetupPlayer();
        SetupPowerUpSpawner();
    }

    private void SetupPlayer()
    {
        _player = PlayerComposer.Compose(playerElements.View, _playerEventsObservable,
            playerElements.Configuration, playerElements.BulletPool);
        
        //TODO: Move this to SubscribeToEvents()
        _enemyEventsObservable.Do(_player.ReceiveEvents).Subscribe(EndGameOnPlayerVictory);
    }

    private void SetupPowerUpSpawner()
    {
        _powerUps = new PowerUpSpawner(gameElements.PowerUp, gameElements.SpawnPoints);
    }

    private void SubscribeToEvents()
    {
        _playerEventsObservable
            .Do(BroadcastEvents)
            .Delay(TimeSpan.FromSeconds(1))
            .Do(_ => _powerUps.SpawnPowerUp())
            .Subscribe(EndGameOnPlayerDeath);
    }

    private void BroadcastEvents(GameEvent gameEvent)
    {
        _screen.ReceiveGameplayEvent(gameEvent);
    }

    private void EndGameOnPlayerVictory(GameEvent victoryEvent)
    {
        
    }

    private void EndGameOnPlayerDeath(GameEvent deathEvent)
    {
        BroadcastEvents(deathEvent);
    }
}
