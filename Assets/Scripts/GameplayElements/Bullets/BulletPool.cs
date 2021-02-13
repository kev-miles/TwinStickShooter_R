using System.Collections.Generic;
using GameplayElements;
using GameplayElements.Bullets;
using UnityEngine;

namespace User
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
                Add(prefab);
                usable.Push(prefab);
            }
        }

        public Bullet Acquire (Transform shooter, BulletType type)
        {
            var obj = usable.Pop();
            obj.gameObject.SetActive(true);
            obj.transform.position = shooter.position;
            obj._origin = this;
            obj.bulletType = type;
            obj.OnAcquire();
            return obj;
        }

        void Add (Bullet obj)
        {
            obj.transform.parent = bulletcontainer.transform;
            obj.gameObject.SetActive(false);
            usable.Push(obj);
        }

        public void Release(Bullet obj)
        {
            usable.Push(obj);
            obj.transform.position = new Vector2(100, 100);
            Add(obj);
        }
    }
}