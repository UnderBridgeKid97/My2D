using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MySample
{
    /// <summary>
    ///  Ŀ���� ��ư : ���� ��ư ��ӹ޾� ��� Ȯ�� 
    /// </summary>
    /// 
    [RequireComponent(typeof(Animator),typeof(CanvasGroup))] // RequireComponent(typeof(@@) ������ ������Ʈ ���̱� 
    public class CustomButton : Button
    {
        #region Variables

     //   public AudioClip overrideClipSound; ���� �߰��ҰŸ� 

        private bool isClicked;                     // ��ư�� ��������?
        private readonly float cooldownTime = 0.5f; // ��ٿ� �ð� ���� ��ư Ŭ�� ���� 

        public new ButtonClickedEvent onClick;      // ��ư Ŭ���� ��ϵ� �Լ� ȣ��
        private new Animator animator;

        private static bool blockInput;             // ��� Ŀ���� ��ư ��� ���� ->
                                                    // ��ư�� ��ӹ޴� ��� ��ư�� ��� ����
        #endregion

        protected override void OnEnable()
        {
            base.OnEnable();
            animator = GetComponent<Animator>();
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (blockInput || isClicked) // Ŭ�� ���̸� 
            {
                return; 
            }

            // TODO: ������ ȿ���� ���  (������)
            

            press();

            isClicked = true;

            if(gameObject.activeInHierarchy)
            {
                StartCoroutine(cooldown());
            }

            base.OnPointerClick(eventData); // �������̵�ϱ� ������ ����� ������ 

        }

        private void press()
        {
            if(blockInput)
            {
                return;
            }

            onClick?.Invoke();
        }

        IEnumerator cooldown()
        {

            yield return new WaitForSeconds(cooldownTime);

            isClicked = false;
        }

        // ��ư �ִϸ��̼� üũ ( �ִϸ��̼��� Ȱ��ȭ������ üũ)
        private bool IsAnimationPlay()
        {
            var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            return stateInfo.loop || stateInfo.normalizedTime < 1;
        }

        // ��� Ŀ���� ��ư ��� ��Ȱ��ȭ/Ȱ��ȭ 
        public static void SetBlockInput(bool block) //
        {
            blockInput = block;
        }







    }
}