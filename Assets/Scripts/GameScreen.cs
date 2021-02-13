using System;
using System.Collections.Generic;
using GameEvents;
using Infrastructure;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.SceneManagement.SceneManager;

public class GameScreen : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button startGameButton = default;
    [SerializeField] private Button toggleAudioButton = default;
    [SerializeField] private Button exitGameButton = default;

    [Header("Animations")]
    [SerializeField] private Animator screenAnimator = default;
    private static readonly int TransitionIn = Animator.StringToHash("Transition_In");
    private static readonly int TransitionOut = Animator.StringToHash("Transition_Out");
    private static readonly int ShowMenu = Animator.StringToHash("ShowMenu");
    private static readonly int ShowGameplay = Animator.StringToHash("ShowGameplay");

    private readonly Dictionary<int, Action> _screenDirectory = new Dictionary<int, Action>();
    private int _sceneToLoad;
    private IObserver<GameEvent> _screenObservable;

    public void Initialize(IObserver<GameEvent> screenObservable)
    {
        _screenObservable = screenObservable;
        startGameButton.OnClickAsObservable().Subscribe(_ => { ShowTransition(); _sceneToLoad = (int)SceneId.Gameplay; });
        toggleAudioButton.OnClickAsObservable().Subscribe(_ => { });
        exitGameButton.OnClickAsObservable().Subscribe(_ => UnityEngine.Application.Quit());
        sceneLoaded += OnSceneLoaded;
        LoadScreenDirectory();
    }

    public void ShowTransition()
    {
        screenAnimator.SetTrigger(TransitionIn);
    }

    public void HideTransition()
    {
        screenAnimator.SetTrigger(TransitionOut);
    }

    [UsedImplicitly] //From Animator
    private void ShowScreenContent()
    {
        var currentScene = GetActiveScene().buildIndex;
        _screenObservable.OnNext(ScreenEvent.TransitionOut(currentScene));
        _screenDirectory[currentScene].Invoke();
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
    }

    private void LoadScreenDirectory()
    {
        _screenDirectory[(int) SceneId.Menu] = ShowMenuScreen;
        _screenDirectory[(int) SceneId.Gameplay] = ShowGameplayScreen;
    }
}