using JetBrains.Annotations;
using UnityEngine;

namespace GameEvents.Screen.Popups
{
    public class WaveFeedbackView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private void OnEnable()
        {
            _animator.Play("WaveFeedback");
        }

        [UsedImplicitly] //From Animator
        private void Hide()
        {
            this.gameObject.SetActive(false);
        }
    }
}