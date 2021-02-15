using System;
using System.Collections.Generic;
using GameEvents;
using GameEvents.Screen;
using GameplayElements.User;
using Infrastructure;
using JetBrains.Annotations;
using Popups;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.SceneManagement.SceneManager;

public class GameScreen : MonoBehaviour
{
    [Header("Popups")] [SerializeField] private HowToPlayView howToPlayView = default;
    
    [Header("Buttons")]
    [SerializeField] private Button startGameButton = default;
    [SerializeField] private Button howToPlayButton = default;
    [SerializeField] private Button exitGameButton = default;
    
    [Header("Gameplay")]
    [SerializeField] private TMP_Text hpLabel = default;
    [SerializeField] private TMP_Text scoreLabel = default;
    [SerializeField] private TMP_Text powerUpLabel = default;

    [Header("Animations")]
    [SerializeField] private Animator screenAnimator = default;
    private static readonly int TransitionIn = Animator.StringToHash("Transition_In");
    private static readonly int TransitionOut = Animator.StringToHash("Transition_Out");
    private static readonly int ShowMenu = Animator.StringToHash("ShowMenu");
    private static readonly int ShowGameplay = Animator.StringToHash("ShowGameplay");

    private readonly Dictionary<int, Action> _screenDirectory = new Dictionary<int, Action>();
    private Dictionary<string, Action<GameEvent>> _eventMap = new Dictionary<string, Action<GameEvent>>();
    private int _sceneToLoad;
    private IObserver<GameEvent> _screenObservable;

    public void Initialize(IObserver<GameEvent> screenObservable)
    {
        _screenObservable = screenObservable;
        startGameButton.OnClickAsObservable().Subscribe(_ => { ShowTransition(); _sceneToLoad = (int)SceneId.Gameplay; });
        howToPlayButton.OnClickAsObservable().Subscribe(_ => { ShowHowToPlay();});
        exitGameButton.OnClickAsObservable().Subscribe(_ => UnityEngine.Application.Quit());
        sceneLoaded += OnSceneLoaded;
        LoadScreenDirectory();
        SetupGameplayEventMap();
    }

    #region Menu&Flow

    private void HideTransition()
    {
        ResetTriggers();
        screenAnimator.SetTrigger(TransitionOut);
    }
    
    private void ShowTransition()
    {
        ResetTriggers();
        screenAnimator.SetTrigger(TransitionIn);
    }

    private void ShowHowToPlay()
    {
        howToPlayView.Show();
    }
    
    [UsedImplicitly] //From Animator
    private void ShowScreenContent()
    {
        var currentScene = GetActiveScene().buildIndex;
        _screenObservable.OnNext(ScreenEvent.TransitionOut(currentScene));
        _screenDirectory[currentScene].Invoke();
    }
    
    [UsedImplicitly] //From Animator
    private void ScreenContentShown()
    {
        var currentScene = GetActiveScene().buildIndex;
        _screenObservable.OnNext(ScreenEvent.ScreenShown(currentScene));
    }
    
    [UsedImplicitly] //From Animator
    private void LoadNewScene()
    {
        LoadScene(_sceneToLoad);
    }

    private void ShowMenuScreen()
    {
        screenAnimator.SetTrigger(ShowMenu);
    }

    private void ShowGameplayScreen()
    {
        screenAnimator.SetTrigger(ShowGameplay);
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _screenObservable.OnNext(ScreenEvent.TransitionIn(scene.buildIndex));
        HideTransition();
    }

    private void LoadScreenDirectory()
    {
        _screenDirectory[(int) SceneId.Menu] = ShowMenuScreen;
        _screenDirectory[(int) SceneId.Gameplay] = ShowGameplayScreen;
    }
    
    private void ResetTriggers()
    {
        screenAnimator.ResetTrigger(TransitionIn);
        screenAnimator.ResetTrigger(TransitionOut);
        screenAnimator.ResetTrigger(ShowMenu);
        screenAnimator.ResetTrigger(ShowGameplay);
    }
    
    #endregion

    #region Gameplay

    public void ReceiveGameplayEvent(GameEvent e)
    {
        if(_eventMap.ContainsKey(e.name))
            _eventMap[e.name](e);
    }

    private void ShowExitPopup(GameEvent exitEvent)
    {
        ShowTransition();
        _sceneToLoad = (int)SceneId.Menu;
    }

    private void ShowDamageFeedback(GameEvent damageEvent)
    {
        hpLabel.text += damageEvent.parameters["HP"];
    }
    
    private void ShowPowerUpFeedback(GameEvent powerupEvent)
    {
        var name = powerupEvent.parameters["Name"];
    }

    private void ShowHealingFeedback(GameEvent healingEvent)
    {
        hpLabel.text += healingEvent.parameters["HP"];
    }

    private void ShowScore(GameEvent scoreEvent)
    {
        scoreLabel.text += scoreEvent.parameters["Score"];
    }

    private void ShowGameOverPopup(GameEvent deathEvent)
    {
        var score = int.Parse(deathEvent.parameters["FinalScore"]);
    }

    private void SetupGameplayEventMap()
    {
        _eventMap[PlayerEventNames.PlayerDeath] = ShowGameOverPopup;
        _eventMap[PlayerEventNames.PlayerDamaged] = ShowDamageFeedback;
        _eventMap[PlayerEventNames.PlayerHealed] = ShowHealingFeedback;
        _eventMap[PlayerEventNames.PlayerExit] = ShowExitPopup;
        _eventMap[PlayerEventNames.UpdateScore] = ShowScore;
        _eventMap[PlayerEventNames.GotPowerUp] = ShowPowerUpFeedback;
    }

    #endregion
}