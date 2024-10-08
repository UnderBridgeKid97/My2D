using UnityEngine;
using UnityEngine.InputSystem;

namespace My2D
{
    public class PlayerController : MonoBehaviour
    {
        #region Variables

        private Rigidbody2D rb2D;

        // �÷��̾� �ȱ� �ӵ�
        [SerializeField] private float WALKSpeed = 4F;

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
            rb2D.velocity = new Vector2(inputMove.x*WALKSpeed,rb2D.velocity.y);
        }


        public void OnMove(InputAction.CallbackContext context)
        {
            inputMove = context.ReadValue<Vector2>();
            Debug.Log("context:"+inputMove);
        }


    }
}