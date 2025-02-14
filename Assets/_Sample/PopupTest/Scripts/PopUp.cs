using UnityEngine;
using System;

namespace MySample
{
    /// <summary>
    ///  팝업 UI를 관리하는 부모 클래스:
    ///  show, hid(애니메이션으로 구현),  close(버튼) -> 결과를 리턴 
    /// </summary>

    [RequireComponent(typeof(Animator), typeof(CanvasGroup))] // 자동으로 컴포넌트 추가 
    public class PopUp : MonoBehaviour
    {
        #region Variables

        public bool fade = false;                      // 팝업 페이드 기능 

        private Animator animator;
        private CanvasGroup canvasGroup; 

        public CustomButton closeButton;               // 창 닫기 버튼 

        // 기능구현 
        public Action OnShowAction;                    // 팝업창 show할때 등록된 함수 호출 
        public Action<popupResult> OnCloseAction;      // 팝업창 close할때 등록된 함수 호출
        protected popupResult result;                  // enum

     // ===========================================
        public delegate void PopupEvents(PopUp popup); // 델리게이트 함수

        public static event PopupEvents OnOpenPopup; 
        public static event PopupEvents OnClosePopup;
        public static event PopupEvents OnBeforeClosePopup; // 닫기 직전에 호출되는 

        #endregion

        private void Awake()
        {
            // 참조
            animator = GetComponent<Animator>();
            canvasGroup = GetComponent<CanvasGroup>();

            // close 버튼클릭시 호출되는 함수 등록
            if(closeButton != null)
            {
                closeButton.onClick.AddListener(Close);
            }
        }

        // 제네릭함수 /       만약 저 것들이  없으면(매개변수가 없으면) 그냥 실행 하는 = null
        public void Show<T>(Action OnShow = null, Action<popupResult> OnClose = null) 
        {
            if(OnShow != null)
            {
                OnShowAction = OnShow;
            }
            if(OnClose != null)
            {
                OnCloseAction = OnClose;
            }
            OnOpenPopup?.Invoke(this);
            PlayhowAnimation();
        }

        private void PlayhowAnimation()
        {
            if (animator != null)
            {
                animator.Play("popup_Show");
            }
        }

        //Show 애니메이션 중간에 이벤트 함수 등록하여 등장 효과음 재생
        public virtual void ShowAnimationSound()
        {
            //TODO: 등장 효과음 재생
        }

        //Show 애니메이션 마지막에 이벤트 함수 등록하여 호출
        public virtual void AfterShowAnimation()
        {
            OnShowAction?.Invoke();
        }

        //Hide 애니메이션 중간에 이벤트 함수 등록하여 퇴장 효과음 재생
        public virtual void HideAnimationSound()
        {
            //TODO: 퇴장 효과음 재생
        }

        //Hide 애니메이션 마지막에 이벤트 함수 등록하여 호출
        public virtual void AfterHideAnimation()
        {
            OnClosePopup?.Invoke(this);
            OnCloseAction?.Invoke(result);
            Destroy(gameObject, 0.5f);
        }

        public virtual void Close() // 다른곳에서 불러 쓸떄 오버라이드 해서 기능 추가 
        {
            if(closeButton != null)
            {
                closeButton .interactable = false; // 버튼 비활성화
            }

            canvasGroup.interactable = false; // 캔버스 기능도 비활성화

            OnBeforeClosePopup?.Invoke(this); 
            if(animator != null)
            {
                animator.Play("popup_Hide");
            }
        }

        public void Show()
        {
            canvasGroup.interactable = true; // 캔바스 기능 활성화
            // 페이드 효과 
            canvasGroup.alpha = 1.0f;        // 캔바스 알파값 1로
        }

        public virtual void Hide()
        {
            canvasGroup.interactable = false;

            // 페이드효과
            canvasGroup.alpha = 0.0f;        // 캔바스 알파값 0로
        }

        protected void StopInteraction()
        {
            canvasGroup.interactable = false;
        }

    }

}