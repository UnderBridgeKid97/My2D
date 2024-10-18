using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace My2D
{
    public class Damagerble : MonoBehaviour
    {
        #region Variables
        private Animator animator;

        // 데미지입을때 등록된 함수 호출 
        public UnityAction<float,Vector2> hitAction;

        // 체력
        [SerializeField] private float maxHealth = 100f;
        public float MaxHealth
        {
            get { return maxHealth; }
            private set { maxHealth = value; }
        }

        [SerializeField]private float currentHealth;
        public float CurrentHealth
        {
            get { return currentHealth; }
            private set
            {
                currentHealth = value;

                // 죽음처리
                if (currentHealth <= 0)
                {
                    IsDeath = true;
                }

            }
        }
        private bool isDeath = false;
        public bool IsDeath
        {
            get { return isDeath; }
            private set
            {
                isDeath = value;
                // 애니메이션
                animator.SetBool(Animation.IsDeath, value);
            }
        }

        // 무적시간
        private bool isInvincible= false;
        [SerializeField]private float invincibleTimer = 3f;
        private float countdown = 0f;

        //
        public bool LockVelocity
        {
            get
            {
              return  animator.GetBool(Animation.LockVelocity);
            }
            private set
            {
                animator.SetBool(Animation.LockVelocity, value);
            }
        }
        #endregion

        private void Awake()
        {
            // 참조
            animator = GetComponent<Animator>();
            countdown = invincibleTimer;
        }

        private void Start()
        {
            // 초기화 
            CurrentHealth = MaxHealth;
            countdown = invincibleTimer;
        }

        private void Update()
        {
            // 무적 상태이면 무적 타이머 돌리기
            if (isInvincible)
            {
                if(countdown <=0)
                {
                    isInvincible =false;

                    //
                    countdown = invincibleTimer;
                }
                countdown -= Time.deltaTime;
            }
        }

        public void TakeDamage(float damage, Vector2 knocback)
        {
            if (!IsDeath && !isInvincible)
            {
                // 무적모드 초기화
                isInvincible = true;

                // 힐 전의 hp
                float beforeHealth = CurrentHealth;

                CurrentHealth -= damage;
                Debug.Log($"{transform.name}가 현재 체력은{CurrentHealth}");

                LockVelocity = true;
                animator.SetTrigger(Animation.HitTrigger); // 애니메이션

                /* // 데미지 효과
                 if(hitAction != null)
                 {
                     hitAction.Invoke(damage, knocback);
                 }*/

                float realDamage = beforeHealth - currentHealth; 

                hitAction?.Invoke(damage,knocback);  // ?과 위의 if문이랑 같은 뜻 
                CharactorEvent.characterDamaged?.Invoke(gameObject,damage);
            }
        }

        //
        public bool Heal(float amount)
        {
            if(CurrentHealth >= MaxHealth)
            {
                return false;
            }
            // 힐 전의 hp
            float beforeHealth = CurrentHealth;

            CurrentHealth += amount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

            // 실제 힐 hp값
            float realHealth = CurrentHealth- beforeHealth;

            CharactorEvent.characterHealed?.Invoke(gameObject, realHealth);

            return true;
        }

    }
}