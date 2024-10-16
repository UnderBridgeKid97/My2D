using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My2D
{
    public class Attack : MonoBehaviour
    {
        #region Variables
        [SerializeField]private float attackDamage = 10f;

        #endregion
        // ���ݷ�

        // �浹 üũ�ؼ� ���ݷ� ��ŭ ������ �ش�
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // ������ �Դ� ��ü ã��
            Damagerble damageable = collision.GetComponent<Damagerble>();

            if(damageable !=null)
            {
                //  Debug.Log($"{collision.name}��(��) �������� �Ծ���");
                damageable.TakeDamage(attackDamage);
            }
        }


    }
}