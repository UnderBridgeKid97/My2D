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
        // 공격력

        // 충돌 체크해서 공격력 만큼 데미지 준다
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // 데미지 입는 객체 찾기
            Damagerble damageable = collision.GetComponent<Damagerble>();

            if(damageable !=null)
            {
                // knocback의 방향 설정
                Vector2 deliveredknocback = transform.parent.localScale.x > 0 ? knocback : new Vector2(-knocback.x,knocback.y);


                //  Debug.Log($"{collision.name}가(이) 데미지를 입없다");
                damageable.TakeDamage(attackDamage, deliveredknocback);
            }
        }

       

    }
}