using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My2D
{
    // ���� ��������Ʈ ������Ʈ�� ���̵� �ƿ� �� ų
    public class FadeRemove : StateMachineBehaviour
    {
        #region Variables

        private SpriteRenderer spriteRenderer;
        private GameObject Removeobject;
        private Color startColor;

        // fade ȿ��
        public float fadeTimer = 1f;
        private float countdown = 0f;

        // ������ �ð� �� �� ���̵� �ƿ� ȿ�� ó��
        public float delayTime = 2f;
        private float delayCountdown = 0f;

        #endregion
        // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // ����
            spriteRenderer = animator.GetComponent<SpriteRenderer>();
            startColor = spriteRenderer.color;
            Removeobject = animator.gameObject;

            // �ʱ�ȭ
            countdown = fadeTimer;


        }

        // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
       override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            // delayTime��ŭ ������ 
            if(delayCountdown < delayTime)
            {
                delayCountdown += Time.deltaTime;
                return;
            }

            // ���̵�ƿ� ȿ�� spriteRenderer.color.a : 1->0
            countdown -= Time.deltaTime;

            // float newAlpha = startColor.a*(1- countdown /fadeTimer) ; =  0 ->1 ���̵� �� ȿ���� �̰ɷ� 
            float newAlpha = startColor.a*( countdown /fadeTimer) ;  // ��  ���İ��� ó������ ���ΰ�� ������ 1�� �ʱ�ȭ�̴ٰ� 0�� �Ǵ�  ���� ���İ��� �����ش�.
            spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);

            // ���̵� Ÿ�� ��
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