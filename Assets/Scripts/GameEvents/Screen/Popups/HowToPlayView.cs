using JetBrains.Annotations;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GameEvents.Screen.Popups
{
    public class HowToPlayView : MonoBehaviour
    {
        [SerializeField] private Button closeButton = default;
        [SerializeField] private Animator _animator = default;

        private void Awake()
        {
            closeButton.OnClickAsObservable().Subscribe(_ => Hide());
        }

        public void Show()
        {
            _animator.Play("HowToIntro");
        }

        private void Hide()
        {
            _animator.Play("HowToOutro");
        }
        
        [UsedImplicitly] //From Animator
        private void PopupHidden()
        {
            
        }
    }
}