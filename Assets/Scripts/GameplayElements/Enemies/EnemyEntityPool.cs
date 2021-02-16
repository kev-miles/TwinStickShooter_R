using System;
using System.Collections.Generic;
using GameplayElements.ShootingStrategies;
using GameplayElements.User;
using UnityEngine;

namespace GameplayElements.Enemies
{
    public class EnemyEntityPool : MonoBehaviour {

        [SerializeField] private EnemyView enemyPrefab;
        [HideInInspector] public GameObject enemyContainer;
        
        Stack<EnemyView> _usable;
        string objectname = "Enemy Container";
        public int amount;

        public void Awake()
        {
            enemyContainer = new GameObject();
            enemyContainer.name = objectname;
            _usable = new Stack<EnemyView>();

            for (int i = 0; i <= amount; i++)
            {
                var _enemyprefab = GameObject.Instantiate(enemyPrefab);
                _enemyprefab.transform.position = new Vector2(100, 100);
                _enemyprefab.gameObject.SetActive(false);
                _usable.Push(_enemyprefab);
                Add(_enemyprefab);
            }
        }

        public EnemyView Acquire(Vector2 pos, Func<Vector3> playerPosition, ShootingStrategy strategy)
        {
            var obj = _usable.Pop();
            obj.gameObject.SetActive(true);
            obj.transform.position = pos;
            obj.playerPosition = playerPosition;
            obj.origin = this;
            obj.strategy = strategy;
            return obj;
        }

        void Add(EnemyView obj)
        {
            obj.transform.parent = enemyContainer.transform;
            obj.gameObject.SetActive(false);
        }

        public void Release(EnemyView obj)
        {
            if (obj == null) return;
            _usable.Push(obj);
            obj.transform.position = new Vector2(100, 100);
            Add(obj);
        }
    }
}