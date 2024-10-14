using UnityEngine;
using UnityEngine.InputSystem;

namespace My2D
{
    public class PlayerController : MonoBehaviour
    {
        #region Variables

        private Rigidbody2D rb2D;
        private Animator animator;
        private TouchingDirections touchingDirections;


        // �÷��̾� �̵� �ӵ�
        [SerializeField] private float WalkSpeed = 4f;
        [SerializeField] private float runSpeed = 8f;
        [SerializeField] private float airSpeed = 2f;

        public float currentMoveSpeed
        {
            get
            {
                if (CanMove)
                {
                    if (IsMove && touchingDirections.Iswall == false)
                    {
                        if (touchingDirections.IsGround)
                        {
                            if (isRun)
                            {
                                return runSpeed;
                            }
                            else
                            {
                                return WalkSpeed;
                            }
                        }
                        else
                        {
                            return airSpeed;
                        }
                    }
                    else
                    {
                        return 0f; // idle state
                    }

                }
                else
                {
                    return 0f;// �������� ���Ҷ�
                }
            }
        }

        // �̵�����
        public bool CanMove
        {
            get 
            {
                return animator.GetBool(Animation.CanMove);
            }

        }

        // �÷��̾� �̵��� ���õ� �Է°�
        private Vector2 inputMove;

        // �ȱ�
        [SerializeField] private bool isMove = false;
        public bool IsMove
        {
            get
            {
                return isMove;
            }
            set
            {
                isMove = value;
                animator.SetBool(Animation.IsMove, value);
            }
        }

        // �ٱ�
        [SerializeField] private bool isRun = false;
        public bool IsRun
        {
            get
            {
                return isRun;
            }
            set
            {
                isRun = value;
                animator.SetBool(Animation.IsRun, value);
            }
        }
        // �¿����
        [SerializeField] private bool isFacingRight = true;
        public bool IsFacingRight
        {
            get
            {
                return isFacingRight;
            }
            set
            {
                // ����
                if (isFacingRight != value)
                {
                    transform.localScale *= new Vector2(-1, 1); // ������ x���� ������
                }
                isFacingRight = value;
            }
        }

        // ����
        [SerializeField]private float jumpForce = 5f;

        #endregion

        private void Awake()
        {
            // ����
            rb2D = this.GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            
            touchingDirections=GetComponent<TouchingDirections>();
        }

        private void FixedUpdate()
        {
            // �÷��̾� �¿� �̵�
            rb2D.velocity = new Vector2(inputMove.x * currentMoveSpeed, rb2D.velocity.y);
            //  rb2D.velocity = new Vector2(inputMove.x * WalkSpeed,inputMove.y * WalkSpeed);

            // �ִϸ��̼� ��
            animator.SetFloat(Animation.YVelocity,rb2D.velocity.y);

        }

        // �ٶ󺸴� �������� ��ȯ
        void SetFacingDirection(Vector2 moveInput)
        {
            if (moveInput.x > 0f && IsFacingRight == false)  // ������ ���� ���� �� Ű�� ������ ���������� ����
            {
                // �������� �ٶ󺻴�
                IsFacingRight = true;
            }
            else if (moveInput.x < 0f && IsFacingRight == true)  // �ݴ� 
            {
                // ������ �ٶ󺻴�
                IsFacingRight = false;
            }
        }

        public void OnMove(InputAction.CallbackContext context)  // ����
        {
            inputMove = context.ReadValue<Vector2>();            // ����
            IsMove = (inputMove != Vector2.zero);

            // ������ȯ
            SetFacingDirection(inputMove);

        }
        public void OnRun(InputAction.CallbackContext context)
        {
            // ������ �����ϴ� ����
            if (context.started)
            {
                IsRun = true;
            }
            // �հ����� ���� ���� & �������ϴ� ���� 
            else if (context.canceled)
            {
                IsRun = false;
            }

        }

        public void OnJump(InputAction.CallbackContext context)
        {
            // ������ �����ϴ� ����, ���� ���� x
            if (context.started && touchingDirections.IsGround)
            {
                animator.SetTrigger(Animation.JumpTrigger);
                rb2D.velocity = new Vector2(rb2D.velocity.x,jumpForce);
            }
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            // ������ �����ϴ� ���� Attack
            if (context.started && touchingDirections.IsGround)
            {
                animator.SetTrigger(Animation.AttackTrigger);

            }
        }


    }
}

/*public void OnMove(InputAction.CallbackContext context)  // ����
{
    inputMove = context.ReadValue<Vector2>();            // ����
}*/


