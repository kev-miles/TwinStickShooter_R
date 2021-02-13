using GameplayElements.Bullets;
using Infrastructure;
using UnityEngine;
using User;

namespace GameplayElements
{
    public class Bullet : MonoBehaviour, Poolable
    {
        [HideInInspector] public SpriteRenderer spriteR;
        [HideInInspector] public BulletType bulletType;
        [HideInInspector] public BulletPool _origin;
        [HideInInspector] public Color tint;

        public Sprite[] _allsprites;

        public void OnAcquire()
        {
            spriteR = this.gameObject.GetComponent<SpriteRenderer>();
            ImplementSprite();
        }

        public void OnRelease()
        {
            spriteR.sprite = default(Sprite);
            bulletType = BulletType.None;
            _origin.Release(this);
        }

        void Update ()
        {
            ImplementBehaviour();
        }

        void ImplementBehaviour()
        {
            //TODO: Add Stategy for enemies and player
            /*switch (bulletType)
            {
                case 0:
                    this.transform.Translate(Vector3.up * 15f * Time.deltaTime);
                    break;
                case 1:
                    this.transform.Translate(Vector3.down * 15f * Time.deltaTime);
                    break;
            }*/
        }

        void ImplementSprite()
        {
            switch (bulletType)
            {
                case BulletType.Player:
                    spriteR.sprite = _allsprites[(int)bulletType];
                    break;
                case BulletType.Enemy:
                    spriteR.sprite = _allsprites[(int)bulletType];
                    break;
            }
        } 
    }
}