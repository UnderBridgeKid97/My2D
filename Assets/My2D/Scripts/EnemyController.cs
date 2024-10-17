using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

namespace My2D
{
    public class EnemyController : MonoBehaviour
    {
        #region Variables
        private Rigidbody2D rb2D;
        private Animator animator;
        private TouchingDirections touchingDirections;
        private Damagerble damagerble;

        // 플레이어 감지
        public DetectionZone detectionZone;
        // 낭떨어지 감지
        public DetectionZone detectionCliff;
        // 이동 속도
        [SerializeField] private float runSpeed = 4f;
        // 이동 방향
        private Vector2 directionVector = Vector2.right;

        // 이동 가능 방향 
        public enum WalkableDirection { Left, Right }

        // 현재 이동방향 
        private WalkableDirection walkDiretion = WalkableDirection.Right;
        public WalkableDirection WalkDiretion
        {
            get { return walkDiretion; }
            private set {
                // 이미지 플립
                transform.localScale *= new Vector2(-1, 1);

                // 실제 이동하는 방향값
                if (value == WalkableDirection.Left)
                {
                    directionVector = Vector2.left;
                }

                else if (value == WalkableDirection.Right)
                {
                    directionVector = Vector2.right;
                }
                walkDiretion = value;

            }
        }

        // 공격 타겟 설정
        [SerializeField] private bool hasTarget = false;
        public bool HasTarget
        {
            get { return hasTarget; }
           private set 
            {
                hasTarget = value;
                animator.SetBool(Animation.HasTarget, value);
            }
        }

        // 이동 가능 상태/ 불가능 상태 - 이동제한
        public bool CanMove
        {
            get { return animator.GetBool(Animation.CanMove); }
        }

        // 감속 계수 
        [SerializeField]private float stopRate = 0.2f;
        #endregion


        private void Awake()
        {
            rb2D= GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            touchingDirections = GetComponent<TouchingDirections>();

            damagerble = GetComponent<Damagerble>();
            damagerble.hitAction += OnHit;

            detectionCliff.noColliderRamain += OnCliffDetection;
        }

        private void Update()
        {
            // 적 감지 충돌체의 리스트 개수가 0보다 크면 적이 감지 된 것 
           HasTarget = (detectionZone.detectedColliders.Count > 0);
        }


        private void FixedUpdate()
        {
            if(touchingDirections.Iswall && touchingDirections.IsGround)
            {
                // 반전
                Flip();
            }
            
            if(!damagerble.LockVelocity)
            {
                // 이동
                if (CanMove)
                {
                    rb2D.velocity = new Vector2(directionVector.x * runSpeed, rb2D.velocity.y);
                }
                else
                {
                    // rb2.velocity.x -> 0  : lerp 멈춤 
                    rb2D.velocity = new Vector2(Mathf.Lerp(rb2D.velocity.x, 0f, stopRate), rb2D.velocity.y);
                }
            }
        }

        // 방향 전환 반전
        private void Flip()
        {
            if (WalkDiretion == WalkableDirection.Left)
            {
                WalkDiretion = WalkableDirection.Right;
            }
            else if (WalkDiretion == WalkableDirection.Right)
            {
                WalkDiretion = WalkableDirection.Left;
            }
            else
            {
                Debug.Log("Error Flip Driection");
            }
        }

        public void OnHit(float damage, Vector2 knocback)
        {
            rb2D.velocity = new Vector2(knocback.x, rb2D.velocity.y + knocback.y);
        }

        public void OnCliffDetection()
        {
            if(touchingDirections.IsGround)
            {
                Flip();
            }
        }

    }
}