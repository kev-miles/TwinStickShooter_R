using System;
using UnityEngine;
using User;

public class GameplayContext : MonoBehaviour
{
    [Header("Player")] 
    [SerializeField] private PlayerConfiguration playerConfiguration;
    [SerializeField] private PlayerView playerView;

    private Player _player;
    private GameScreen _screen;

    public void Initialize(GameScreen gameScreen)
    {
        _screen = gameScreen;
        _screen.HideTransition();
        SetupPlayer();
    }

    private void SetupPlayer()
    {
        _player = PlayerComposer.Compose(playerView, playerConfiguration);
        _player.OnPlayerDeath += EndGameOnPlayerDeath;
        _player.OnPlayerDamaged += _screen.ShowDamageFeedback;
        _player.OnPlayerHeal += _screen.ShowHealingFeedback;
    }

    private void EndGameOnPlayerDeath(int finalScore)
    {
        //TODO:stop gameplay
        _screen.ShowGameOverPopup(finalScore);
    }
}
