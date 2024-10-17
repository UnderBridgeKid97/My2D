using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My2D
{
    public class Attack : MonoBehaviour
    {
        #region Variables
        [SerializeField]private float attackDamage = 10f;

        public Vector2 knocback = Vector2.zero;
        #endregion
        // ���ݷ�

        // �浹 üũ�ؼ� ���ݷ� ��ŭ ������ �ش�
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // ������ �Դ� ��ü ã��
            Damagerble damageable = collision.GetComponent<Damagerble>();

            if(damageable !=null)
            {
                // knocback�� ���� ����
                Vector2 deliveredknocback = transform.parent.localScale.x > 0 ? knocback : new Vector2(-knocback.x,knocback.y);


                //  Debug.Log($"{collision.name}��(��) �������� �Ծ���");
                damageable.TakeDamage(attackDamage, deliveredknocback);
            }
        }

       

    }
}