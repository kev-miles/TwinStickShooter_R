using GameEvents;
using GameplayElements.User;
using Infrastructure;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

public class GameplayContext : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private PlayerElements _playerElements = default;

    private Player _player;
    private GameScreen _screen;
    private Subject<GameEvent> _gameplayObservable;

    public void Initialize(GameScreen gameScreen)
    {
        _screen = gameScreen;
        _screen.HideTransition();
        _gameplayObservable = new Subject<GameEvent>();
        SubscribeToEvents();
        SetupPlayer();
    }

    private void SetupPlayer()
    {
        _player = PlayerComposer.Compose(_playerElements.View, _gameplayObservable, _playerElements.Configuration);
    }

    private void SubscribeToEvents()
    {
        _gameplayObservable.Do(BroadcastEvents).Subscribe(EndGameOnPlayerDeath);
    }

    private void BroadcastEvents(GameEvent gameEvent)
    {
        _screen.ReceiveGameplayEvent(gameEvent);
    }

    private void EndGameOnPlayerDeath(GameEvent deathEvent)
    {
        //TODO:stop gameplay;
    }
}
