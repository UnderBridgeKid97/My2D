using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My2D
{
    // 바닥, 벽,  천장 체크 

    public class TouchingDirections : MonoBehaviour
    {
        #region Variables

        private CapsuleCollider2D touchingColldier;
        private Animator animator;

        [SerializeField]private ContactFilter2D contactFilter;
        [SerializeField] private float groundDistance = 0.05f;
        [SerializeField] private float ceilingDistance = 0.05f;
        [SerializeField] private float wallDistance = 0.2f;

        private RaycastHit2D[] groundHits = new RaycastHit2D[5];
        private RaycastHit2D[] ceilingHits = new RaycastHit2D[5];
        private RaycastHit2D[] wallHits = new RaycastHit2D[5];

        [SerializeField]private bool isGround;
        public bool IsGround
        {
            get { return isGround; }
            private set { isGround = value; animator.SetBool(Animation.IsGround, value); }
        }

        [SerializeField] private bool isCeiling;
        public bool IsCeiling
        {
            get { return isCeiling; }
            private set
            {
                isCeiling = value;
                animator.SetBool(Animation.IsCeiling, value);
            }
        }

        [SerializeField] private bool iswall;
        public bool Iswall
        {
            get { return iswall; }
            private set { iswall = value; animator.SetBool(Animation.Iswall , value); }
        }
        private Vector2 walkDirection => (transform.localScale.x >0)? Vector2.right : Vector2.left;

        #endregion

        private void Awake()
        {
            touchingColldier = GetComponent<CapsuleCollider2D>();
            animator = GetComponent<Animator>();
        }

        private void FixedUpdate()
        {
            IsGround = touchingColldier.Cast(Vector2.down, contactFilter, groundHits, groundDistance) > 0;
            Iswall = touchingColldier.Cast(walkDirection, /* or ,Vector2.right*/ contactFilter, wallHits, wallDistance) > 0;
            IsCeiling = touchingColldier.Cast(Vector2.left, /* or ,Vector2.right*/ contactFilter, wallHits, ceilingDistance) > 0;
        }

    }

}