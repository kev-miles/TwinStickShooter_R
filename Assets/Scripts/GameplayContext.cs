using UnityEngine;

public class GameplayContext : MonoBehaviour
{
    public void Initialize(GameScreen gameScreen)
    {
        gameScreen.HideTransition();
    }
}
