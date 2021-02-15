using System;
using JetBrains.Annotations;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GameEvents.Screen.Popups
{
    public class GameOverView : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField] private Button closeButton = default;
        [SerializeField] private TMP_Text _feedbackLabel = default;
        [SerializeField] private TMP_Text _scoreLabel = default;
        [SerializeField] private Animator _animator = default;

        private Action _callback;
        private void Awake()
        {
            closeButton.OnClickAsObservable().Subscribe(_ => Hide());
        }

        public void Show(string score, bool playerWon, Action callback)
        {
            _callback = callback;
            _feedbackLabel.text = playerWon ? "Congratulations!" : "Game Over";
            _scoreLabel.text = "Final score: " + score;
            _animator.Play("GameOverIntro");
        }

        private void Hide()
        {
            _animator.Play("GameOverOutro");
        }
    
        [UsedImplicitly] //From Animator
        private void PopupHidden()
        {
            _callback();
        }
    }
}
