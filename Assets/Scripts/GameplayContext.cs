using System;
using GameEvents;
using GameEvents.Player;
using GameplayElements;
using GameplayElements.Enemies;
using GameplayElements.User;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

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
    private IObservable<GameEvent> _screenObservable;

    public void Initialize(GameScreen gameScreen)
    {
        _screen = gameScreen;
        _screenObservable = _screen.Observable();
        _playerEventsObservable = new Subject<GameEvent>();
        _enemyEventsObservable = new Subject<GameEvent>();
        _enemyEventsObservable.Share();
        _victoryCondition = new WavesVictoryCondition();
        SubscribeToPlayerEvents();
        SubscribeToScreenEvents();
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
                EndGameOnPlayerExit(e);
                BroadcastEvents(e);
                EndGameOnPlayerVictory(e);
                CheckWaveChange(e);
                SpawnPowerUpOnChance(e);
            })
            .Subscribe();
    }

    private void SubscribeToPlayerEvents()
    {
        _playerEventsObservable
            .Do(EndGameOnPlayerExit)
            .Do(BroadcastEvents)
            .Where(e => e.name == EventNames.PlayerKilled)
            .Do(EndGameOnPlayerDefeat)
            .Subscribe();
    }
    
    private void SubscribeToScreenEvents()
    {
        _screenObservable
            .Subscribe(EndGameOnPlayerExit);
    }
    
    private void DisposeObservables()
    {
        _playerEventsObservable.Dispose();
        _enemyEventsObservable.Dispose();
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
            var chance = UnityEngine.Random.Range(0, 1f);
            if(chance > playerElements.Configuration.PowerUpSpawnChance)
                _powerUps.SpawnPowerUp();
        }
    }
    
    private void CheckWaveChange(GameEvent gameEvent)
    {
        if (gameEvent.name == EventNames.WaveFinished)
            _enemies.Stop();
        if (gameEvent.name == EventNames.WaveStart)
            _enemies.Start();
    }
    
    private void EndGameOnPlayerExit(GameEvent gameEvent)
    {
        if(gameEvent.name == EventNames.PlayerExit)
            _enemies.Stop(ExitGameplay);
    }

    private void EndGameOnPlayerDefeat(GameEvent gameEvent)
    {
        if(gameEvent.name == EventNames.PlayerKilled)
            _enemies.Stop(DisposeObservables);
    }
    
    private void EndGameOnPlayerVictory(GameEvent victoryEvent)
    {
        if(_victoryCondition.CheckCondition(victoryEvent))
            _playerEventsObservable.OnNext(PlayerEvent.Victory(_player.Score()));
    }

    private void ExitGameplay()
    {
        _screen.ShowExitTransition();
    }
}
