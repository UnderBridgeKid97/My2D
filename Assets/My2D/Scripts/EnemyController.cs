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

        // �÷��̾� ����
        public DetectionZone detectionZone;
        // �������� ����
        public DetectionZone detectionCliff;
        // �̵� �ӵ�
        [SerializeField] private float runSpeed = 4f;
        // �̵� ����
        private Vector2 directionVector = Vector2.right;

        // �̵� ���� ���� 
        public enum WalkableDirection { Left, Right }

        // ���� �̵����� 
        private WalkableDirection walkDiretion = WalkableDirection.Right;
        public WalkableDirection WalkDiretion
        {
            get { return walkDiretion; }
            private set {
                // �̹��� �ø�
                transform.localScale *= new Vector2(-1, 1);

                // ���� �̵��ϴ� ���Ⱚ
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

        // ���� Ÿ�� ����
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

        // �̵� ���� ����/ �Ұ��� ���� - �̵�����
        public bool CanMove
        {
            get { return animator.GetBool(Animation.CanMove); }
        }

        // ���� ��� 
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
            // �� ���� �浹ü�� ����Ʈ ������ 0���� ũ�� ���� ���� �� �� 
           HasTarget = (detectionZone.detectedColliders.Count > 0);
        }


        private void FixedUpdate()
        {
            if(touchingDirections.Iswall && touchingDirections.IsGround)
            {
                // ����
                Flip();
            }
            
            if(!damagerble.LockVelocity)
            {
                // �̵�
                if (CanMove)
                {
                    rb2D.velocity = new Vector2(directionVector.x * runSpeed, rb2D.velocity.y);
                }
                else
                {
                    // rb2.velocity.x -> 0  : lerp ���� 
                    rb2D.velocity = new Vector2(Mathf.Lerp(rb2D.velocity.x, 0f, stopRate), rb2D.velocity.y);
                }
            }
        }

        // ���� ��ȯ ����
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