using GameEvents;
using Infrastructure;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private GameScreen gameScreen = default;

    private Subject<GameEvent> _screenObservable = new Subject<GameEvent>();
    void Start()
    {
        SetupGameScreen();
        SetupGameplayContext();
        InitialMenuLoad();
    }

    private void InitialMenuLoad()
    {
        SceneManager.LoadScene((int) SceneId.Menu);
        gameScreen.HideTransition();
    }

    private void SetupGameplayContext()
    {
        _screenObservable
            .Where(e => e.parameters.ContainsKey("NewScene") && int.Parse(e.parameters["NewScene"]) == (int)SceneId.Gameplay)
            .Do(onNext: _ =>
            {
                var context = FindObjectOfType<GameplayContext>();
                context.Initialize(gameScreen);
            })
            .Subscribe();
    }
    
    private void SetupGameScreen()
    {
        _screenObservable.Share();
        gameScreen.Initialize(_screenObservable);
    }
}
