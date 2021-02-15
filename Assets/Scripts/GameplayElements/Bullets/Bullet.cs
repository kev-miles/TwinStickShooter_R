using GameplayElements.Bullets.Strategies;
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
        [HideInInspector] public BulletStrategy strategy;

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
            strategy.Execute(rigidbody,transform);
        }

        void InitialSetup()
        {
            switch (bulletType)
            {
                case BulletType.Player:
                    gameObject.layer = LayerMask.NameToLayer("PlayerBullets");
                    spriteR.sprite = allSprites[(int)bulletType];
                    spriteR.color = Color.green;
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
            var enemy = collision.gameObject.GetComponent<EnemyView>();
            var player = collision.gameObject.GetComponent<PlayerView>();

            if(enemy != null)
            {
                enemy.Damage();
            }
            if(player != null)
            {
                player.Damage();
            }

            OnRelease();
        }
    }
}