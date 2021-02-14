using UnityEngine;

namespace GameplayElements.User
{
    [CreateAssetMenu]
    public class PlayerConfiguration : ScriptableObject
    {
        [SerializeField, Range(0,10)] private float _speed = 5f;

        public float Speed => _speed;
    }
}