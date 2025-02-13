using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MySample
{
    /// <summary>
    ///  옵션창
    /// </summary>
    public class Settings : PopUp
    {
        #region Variables
        [SerializeField]private CustomButton back;             // 게임종료
        [SerializeField] private CustomButton privacy;   // 개인정보 동의 창
        #endregion

        private void OnEnable()
        {
            // 옵션창 버튼 클릭시 호출되는 함수 등록 

            back.onClick.AddListener(BackToMain);
            privacy.onClick.AddListener(PrivacyPolicy);
        }

        private void BackToMain()
        {
            StopInteraction();

            Close();

            // 게임 종료창 오픈
            MenuManager.Instance.ShowPopup<ExitGame>();


        }

        private void PrivacyPolicy()
        {
            StopInteraction();

            Close();

            // 개인정보 동의 창 오픈
            MenuManager.Instance.ShowPopup<GDPR>();

        }
    }
}