using System.Collections.Generic;
using GameplayElements.Bullets.Strategies;
using UnityEngine;

namespace GameplayElements.Bullets
{
    public class BulletPool : MonoBehaviour
    {
        Stack<Bullet> usable;
        GameObject bulletcontainer;
        public Bullet _bulletprefab;
        public int amount;

        private string _objectName = "BulletContainer";

        protected void SetContainerName(string name)
        {
            _objectName = name;
        }

        protected void Load()
        {
            bulletcontainer = new GameObject();
            bulletcontainer.name = _objectName;
            usable = new Stack<Bullet>();

            for (int i=0; i<=amount; i++)
            {
                var prefab = GameObject.Instantiate(_bulletprefab);
                prefab.transform.position = new Vector2(100, 100);
                prefab.gameObject.SetActive(false);
                usable.Push(prefab);
                Add(prefab);
            }
        }

        public Bullet Acquire (Vector3 origin, Quaternion rotation , BulletType type, BulletStrategy strategy = null)
        {
            var obj = usable.Pop();
            obj.gameObject.SetActive(true);
            obj.transform.position = origin;
            obj.transform.rotation = rotation;
            obj.strategy = strategy ?? new RegularBullet();
            obj.origin = this;
            obj.bulletType = type;
            obj.OnAcquire();
            return obj;
        }

        void Add (Bullet obj)
        {
            obj.transform.parent = bulletcontainer.transform;
            obj.gameObject.SetActive(false);
        }

        public void Release(Bullet obj)
        {
            usable.Push(obj);
            obj.transform.position = new Vector2(100, 100);
            Add(obj);
        }
    }
}