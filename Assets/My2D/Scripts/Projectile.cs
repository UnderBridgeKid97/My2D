using UnityEngine;

namespace My2D
{
    public class Projectile : MonoBehaviour
    {
        #region Variables
        private Rigidbody2D rb2D;

        // 화살 이동
        [SerializeField]private Vector2 moveSpeed = new Vector2(5f, 0f);

        // 데미지
        [SerializeField]private float attackDamage = 10f;
        [SerializeField] private Vector2 knocback = new Vector2(0f, 0f);

        // 데미지 이펙트
        public GameObject impectEffectPrefab;

        #endregion

        private void Awake()
        {
            // 참조
            rb2D = GetComponent<Rigidbody2D>(); 
        }

        private void Start()
        {
            rb2D.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y); 
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Damagerble damageable = collision.GetComponent<Damagerble>();   

            if(damageable != null)
            {
                Vector2 deliveredknocback = (transform.localScale.x > 0) ? knocback : new Vector2(-knocback.x, knocback.y);
                damageable.TakeDamage(attackDamage, knocback);

                // 데미지 이펙트
               GameObject effectGo= Instantiate(impectEffectPrefab, transform.position,Quaternion.identity);
                Destroy(effectGo);
                // 화살킬
                Destroy(gameObject);
            }
        }

    }
}