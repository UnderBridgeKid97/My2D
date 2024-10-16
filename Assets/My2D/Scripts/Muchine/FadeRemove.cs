using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My2D
{
    // 게임 스프라이트 오브젝트를 페이드 아웃 후 킬
    public class FadeRemove : StateMachineBehaviour
    {
        #region Variables

        private SpriteRenderer spriteRenderer;
        private GameObject Removeobject;
        private Color startColor;

        // fade 효과
        public float fadeTimer = 1f;
        private float countdown = 0f;

        // 딜레이 시간 후 에 페이드 아웃 효과 처리
        public float delayTime = 2f;
        private float delayCountdown = 0f;

        #endregion
        // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // 참조
            spriteRenderer = animator.GetComponent<SpriteRenderer>();
            startColor = spriteRenderer.color;
            Removeobject = animator.gameObject;

            // 초기화
            countdown = fadeTimer;


        }

        // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
       override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // delayTime만큼 딜레이 
            if(delayCountdown < delayTime)
            {
                delayCountdown += Time.deltaTime;
                return;
            }

            // 페이드아웃 효과 spriteRenderer.color.a : 1->0
            countdown -= Time.deltaTime;

            // float newAlpha = startColor.a*(1- countdown /fadeTimer) ; =  0 ->1 페이드 인 효과는 이걸로 
            float newAlpha = startColor.a*( countdown /fadeTimer) ;  // ★  알파값이 처음부터 반인경우 죽으면 1로 초기화됫다가 0이 되니  시작 알파값을 곱해준다.
            spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);

            // 페이드 타임 끝
            if (countdown <= 0)
            {
                Destroy(Removeobject);
            }






        }

        // OnStateExit is called before OnStateExit is called on any state inside this state machine
        //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateMove is called before OnStateMove is called on any state inside this state machine
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateIK is called before OnStateIK is called on any state inside this state machine
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateMachineEnter is called when entering a state machine via its Entry Node
        //override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
        //{
        //    
        //}

        // OnStateMachineExit is called when exiting a state machine via its Exit Node
        //override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
        //{
        //    
        //}
    }
}