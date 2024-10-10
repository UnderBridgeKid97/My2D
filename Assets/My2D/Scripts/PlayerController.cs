using UnityEngine;
using UnityEngine.InputSystem;

namespace My2D
{
    public class PlayerController : MonoBehaviour
    {
        #region Variables

        private Rigidbody2D rb2D;

        // 플레이어 걷기 속도
        [SerializeField] private float WalkSpeed = 4F;

        // 플레이어 이동과 관련된 입력값
        private Vector2 inputMove;

        #endregion

        private void Awake()
        {
            // 참조
            rb2D = this.GetComponent<Rigidbody2D>();
          //  rb2D.velocity
        }

        private void FixedUpdate()
        {
            // 플레이어 좌우 이동
              rb2D.velocity = new Vector2(inputMove.x* WalkSpeed, rb2D.velocity.y);
          //  rb2D.velocity = new Vector2(inputMove.x * WalkSpeed,inputMove.y * WalkSpeed);
        }


        public void OnMove(InputAction.CallbackContext context)  // 공식
        {
            inputMove = context.ReadValue<Vector2>();            // 공식
            Debug.Log("context:"+inputMove);
        }


    }
}