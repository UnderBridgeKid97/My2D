using UnityEngine;
using UnityEngine.InputSystem;

namespace My2D
{
    public class PlayerController : MonoBehaviour
    {
        #region Variables

        private Rigidbody2D rb2D;

        // �÷��̾� �ȱ� �ӵ�
        [SerializeField] private float WalkSpeed = 4F;

        // �÷��̾� �̵��� ���õ� �Է°�
        private Vector2 inputMove;

        #endregion

        private void Awake()
        {
            // ����
            rb2D = this.GetComponent<Rigidbody2D>();
          //  rb2D.velocity
        }

        private void FixedUpdate()
        {
            // �÷��̾� �¿� �̵�
              rb2D.velocity = new Vector2(inputMove.x* WalkSpeed, rb2D.velocity.y);
          //  rb2D.velocity = new Vector2(inputMove.x * WalkSpeed,inputMove.y * WalkSpeed);
        }


        public void OnMove(InputAction.CallbackContext context)  // ����
        {
            inputMove = context.ReadValue<Vector2>();            // ����
            Debug.Log("context:"+inputMove);
        }


    }
}