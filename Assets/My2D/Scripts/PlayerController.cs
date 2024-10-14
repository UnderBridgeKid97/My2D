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


        // 플레이어 이동 속도
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
                    return 0f;// 움직이지 못할때
                }
            }
        }

        // 이동여부
        public bool CanMove
        {
            get 
            {
                return animator.GetBool(Animation.CanMove);
            }

        }

        // 플레이어 이동과 관련된 입력값
        private Vector2 inputMove;

        // 걷기
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

        // 뛰기
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
        // 좌우반전
        [SerializeField] private bool isFacingRight = true;
        public bool IsFacingRight
        {
            get
            {
                return isFacingRight;
            }
            set
            {
                // 반전
                if (isFacingRight != value)
                {
                    transform.localScale *= new Vector2(-1, 1); // 스케일 x축을 음수로
                }
                isFacingRight = value;
            }
        }

        // 점프
        [SerializeField]private float jumpForce = 5f;

        #endregion

        private void Awake()
        {
            // 참조
            rb2D = this.GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            
            touchingDirections=GetComponent<TouchingDirections>();
        }

        private void FixedUpdate()
        {
            // 플레이어 좌우 이동
            rb2D.velocity = new Vector2(inputMove.x * currentMoveSpeed, rb2D.velocity.y);
            //  rb2D.velocity = new Vector2(inputMove.x * WalkSpeed,inputMove.y * WalkSpeed);

            // 애니메이션 값
            animator.SetFloat(Animation.YVelocity,rb2D.velocity.y);

        }

        // 바라보는 방향으로 전환
        void SetFacingDirection(Vector2 moveInput)
        {
            if (moveInput.x > 0f && IsFacingRight == false)  // 왼쪽을 보고 있을 때 키를 눌러서 오른쪽으로 반전
            {
                // 오른쪽을 바라본다
                IsFacingRight = true;
            }
            else if (moveInput.x < 0f && IsFacingRight == true)  // 반대 
            {
                // 왼쪽을 바라본다
                IsFacingRight = false;
            }
        }

        public void OnMove(InputAction.CallbackContext context)  // 공식
        {
            inputMove = context.ReadValue<Vector2>();            // 공식
            IsMove = (inputMove != Vector2.zero);

            // 방향전환
            SetFacingDirection(inputMove);

        }
        public void OnRun(InputAction.CallbackContext context)
        {
            // 누르기 시작하는 순간
            if (context.started)
            {
                IsRun = true;
            }
            // 손가락을 떼는 순간 & 릴리즈하는 순간 
            else if (context.canceled)
            {
                IsRun = false;
            }

        }

        public void OnJump(InputAction.CallbackContext context)
        {
            // 누르기 시작하는 순간, 이중 점프 x
            if (context.started && touchingDirections.IsGround)
            {
                animator.SetTrigger(Animation.JumpTrigger);
                rb2D.velocity = new Vector2(rb2D.velocity.x,jumpForce);
            }
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            // 누르기 시작하는 순간 Attack
            if (context.started && touchingDirections.IsGround)
            {
                animator.SetTrigger(Animation.AttackTrigger);

            }
        }


    }
}

/*public void OnMove(InputAction.CallbackContext context)  // 공식
{
    inputMove = context.ReadValue<Vector2>();            // 공식
}*/


