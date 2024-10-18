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

        // ������������ ��ϵ� �Լ� ȣ�� 
        public UnityAction<float,Vector2> hitAction;

        // ü��
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

                // ����ó��
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
                // �ִϸ��̼�
                animator.SetBool(Animation.IsDeath, value);
            }
        }

        // �����ð�
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
            // ����
            animator = GetComponent<Animator>();
            countdown = invincibleTimer;
        }

        private void Start()
        {
            // �ʱ�ȭ 
            CurrentHealth = MaxHealth;
            countdown = invincibleTimer;
        }

        private void Update()
        {
            // ���� �����̸� ���� Ÿ�̸� ������
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
                // ������� �ʱ�ȭ
                isInvincible = true;

                // �� ���� hp
                float beforeHealth = CurrentHealth;

                CurrentHealth -= damage;
                Debug.Log($"{transform.name}�� ���� ü����{CurrentHealth}");

                LockVelocity = true;
                animator.SetTrigger(Animation.HitTrigger); // �ִϸ��̼�

                /* // ������ ȿ��
                 if(hitAction != null)
                 {
                     hitAction.Invoke(damage, knocback);
                 }*/

                float realDamage = beforeHealth - currentHealth; 

                hitAction?.Invoke(damage,knocback);  // ?�� ���� if���̶� ���� �� 
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
            // �� ���� hp
            float beforeHealth = CurrentHealth;

            CurrentHealth += amount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

            // ���� �� hp��
            float realHealth = CurrentHealth- beforeHealth;

            CharactorEvent.characterHealed?.Invoke(gameObject, realHealth);

            return true;
        }

    }
}