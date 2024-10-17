using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My2D
{
    public class PickupHealth : MonoBehaviour
    {
        #region Variables
        // 힐수치
        [SerializeField]private float restoreHealth = 20f;

        [SerializeField] private Vector3 rotateSpeed = new Vector3(0f,180f,0f);

        #endregion

        private void Update()
        {
            // 아이템 회전
            transform.eulerAngles += rotateSpeed * Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // 충돌한 오브젝트 damageable을 검사하여 힐한다
            Damagerble damagerble = collision.GetComponent<Damagerble>();

            if (damagerble != null ) 
            {
                
                bool isHeal = damagerble.Heal(restoreHealth);

                if (isHeal)
                {
                    // 먹고나면 아이템 킬 
                    Destroy(gameObject);
                }

            }
        }
    }
    
}