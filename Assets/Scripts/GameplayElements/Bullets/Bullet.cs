using GameplayElements.Enemies;
using GameplayElements.User;
using Infrastructure.Interfaces;
using UnityEngine;

namespace GameplayElements.Bullets
{
    public class Bullet : MonoBehaviour, Poolable
    {
        [HideInInspector] public BulletPool origin;
        [HideInInspector] public BulletType bulletType;
        [HideInInspector] public Transform shooter;

        [SerializeField] private Rigidbody2D rigidbody = default;
        [SerializeField] private SpriteRenderer spriteR;
        [SerializeField] private Color tint;
        [SerializeField] private  Sprite[] allSprites = default;

        public void OnAcquire()
        {
            spriteR = this.gameObject.GetComponent<SpriteRenderer>();
            InitialSetup();
        }

        public void OnRelease()
        {
            spriteR.sprite = default(Sprite);
            bulletType = BulletType.None;
            origin.Release(this);
        }

        void Update ()
        {
            ImplementBehaviour();
        }

        void ImplementBehaviour()
        {
            //TODO: Add Stategy for enemies and player
            switch (bulletType)
            {
                case BulletType.Player:
                    rigidbody.velocity = transform.right * 15f;
                    break;
                case BulletType.Enemy:
                    rigidbody.velocity = transform.right * 15f;
                    break;
            }
        }

        void InitialSetup()
        {
            switch (bulletType)
            {
                case BulletType.Player:
                    gameObject.layer = LayerMask.NameToLayer("PlayerBullets");
                    spriteR.sprite = allSprites[(int)bulletType];
                    spriteR.color = Color.blue;
                    break;
                case BulletType.Enemy:
                    gameObject.layer = LayerMask.NameToLayer("EnemyBullets");
                    spriteR.sprite = allSprites[(int)bulletType];
                    spriteR.color = Color.red;
                    break;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (bulletType == BulletType.Player)
            {
                var enemy = collision.gameObject.GetComponent<EnemyView>();
                if(enemy != null)
                {
                    //TODO: Do Something   
                }
            }
            else if(bulletType == BulletType.Enemy)
            {
                var player = collision.gameObject.GetComponent<PlayerView>();
                if(player != null)
                {
                    //TODO: Do Something   
                }
            }

            OnRelease();
        }
    }
}