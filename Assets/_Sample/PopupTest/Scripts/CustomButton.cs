using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MySample
{
    /// <summary>
    ///  커스텀 버튼 : 기존 버튼 상속받아 기능 확장 
    /// </summary>
    /// 
    [RequireComponent(typeof(Animator),typeof(CanvasGroup))] // RequireComponent(typeof(@@) 강제로 컴포넌트 붙이기 
    public class CustomButton : Button
    {
        #region Variables

     //   public AudioClip overrideClipSound; 사운드 추가할거면 

        private bool isClicked;                     // 버튼이 눌러졌나?
        private readonly float cooldownTime = 0.5f; // 쿨다운 시간 동안 버튼 클릭 방지 

        public new ButtonClickedEvent onClick;      // 버튼 클릭시 등록된 함수 호출
        private new Animator animator;

        private static bool blockInput;             // 모든 커스텀 버튼 기능 정지 ->
                                                    // 버튼을 상속받는 모든 버튼을 기능 정지
        #endregion

        protected override void OnEnable()
        {
            base.OnEnable();
            animator = GetComponent<Animator>();
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (blockInput || isClicked) // 클릭 중이면 
            {
                return; 
            }

            // TODO: 누르는 효과음 재생  (쓰려면)
            

            press();

            isClicked = true;

            if(gameObject.activeInHierarchy)
            {
                StartCoroutine(cooldown());
            }

            base.OnPointerClick(eventData); // 오버라이드니까 원본에 기능이 있으면 

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

        // 버튼 애니메이션 체크 ( 애니메이션이 활성화중인지 체크)
        private bool IsAnimationPlay()
        {
            var stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            return stateInfo.loop || stateInfo.normalizedTime < 1;
        }

        // 모든 커스텀 버튼 기능 비활성화/활성화 
        public static void SetBlockInput(bool block) //
        {
            blockInput = block;
        }







    }
}