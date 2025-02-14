using UnityEngine;
using System;

namespace MySample
{
    /// <summary>
    ///  �˾� UI�� �����ϴ� �θ� Ŭ����:
    ///  show, hid(�ִϸ��̼����� ����),  close(��ư) -> ����� ���� 
    /// </summary>

    [RequireComponent(typeof(Animator), typeof(CanvasGroup))] // �ڵ����� ������Ʈ �߰� 
    public class PopUp : MonoBehaviour
    {
        #region Variables

        public bool fade = false;                      // �˾� ���̵� ��� 

        private Animator animator;
        private CanvasGroup canvasGroup; 

        public CustomButton closeButton;               // â �ݱ� ��ư 

        // ��ɱ��� 
        public Action OnShowAction;                    // �˾�â show�Ҷ� ��ϵ� �Լ� ȣ�� 
        public Action<popupResult> OnCloseAction;      // �˾�â close�Ҷ� ��ϵ� �Լ� ȣ��
        protected popupResult result;                  // enum

     // ===========================================
        public delegate void PopupEvents(PopUp popup); // ��������Ʈ �Լ�

        public static event PopupEvents OnOpenPopup; 
        public static event PopupEvents OnClosePopup;
        public static event PopupEvents OnBeforeClosePopup; // �ݱ� ������ ȣ��Ǵ� 

        #endregion

        private void Awake()
        {
            // ����
            animator = GetComponent<Animator>();
            canvasGroup = GetComponent<CanvasGroup>();

            // close ��ưŬ���� ȣ��Ǵ� �Լ� ���
            if(closeButton != null)
            {
                closeButton.onClick.AddListener(Close);
            }
        }

        // ���׸��Լ� /       ���� �� �͵���  ������(�Ű������� ������) �׳� ���� �ϴ� = null
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

        //Show �ִϸ��̼� �߰��� �̺�Ʈ �Լ� ����Ͽ� ���� ȿ���� ���
        public virtual void ShowAnimationSound()
        {
            //TODO: ���� ȿ���� ���
        }

        //Show �ִϸ��̼� �������� �̺�Ʈ �Լ� ����Ͽ� ȣ��
        public virtual void AfterShowAnimation()
        {
            OnShowAction?.Invoke();
        }

        //Hide �ִϸ��̼� �߰��� �̺�Ʈ �Լ� ����Ͽ� ���� ȿ���� ���
        public virtual void HideAnimationSound()
        {
            //TODO: ���� ȿ���� ���
        }

        //Hide �ִϸ��̼� �������� �̺�Ʈ �Լ� ����Ͽ� ȣ��
        public virtual void AfterHideAnimation()
        {
            OnClosePopup?.Invoke(this);
            OnCloseAction?.Invoke(result);
            Destroy(gameObject, 0.5f);
        }

        public virtual void Close() // �ٸ������� �ҷ� ���� �������̵� �ؼ� ��� �߰� 
        {
            if(closeButton != null)
            {
                closeButton .interactable = false; // ��ư ��Ȱ��ȭ
            }

            canvasGroup.interactable = false; // ĵ���� ��ɵ� ��Ȱ��ȭ

            OnBeforeClosePopup?.Invoke(this); 
            if(animator != null)
            {
                animator.Play("popup_Hide");
            }
        }

        public void Show()
        {
            canvasGroup.interactable = true; // ĵ�ٽ� ��� Ȱ��ȭ
            // ���̵� ȿ�� 
            canvasGroup.alpha = 1.0f;        // ĵ�ٽ� ���İ� 1��
        }

        public virtual void Hide()
        {
            canvasGroup.interactable = false;

            // ���̵�ȿ��
            canvasGroup.alpha = 0.0f;        // ĵ�ٽ� ���İ� 0��
        }

        protected void StopInteraction()
        {
            canvasGroup.interactable = false;
        }

    }

}