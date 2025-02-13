using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MySample
{
    /// <summary>
    ///  개인정보 동의 창: 동의, 취소, 개인정보 페이지 링크
    /// </summary>
    public class GDPR :PopUp
    {
        #region Variables
        #endregion

        // 개인 정보 동의
        public void OnUserClickAcept()
        {
            // 개인정보동의저장
            Close();
        }

        // 개인정보 동의 창 취소
        public void OnUserClickCancel()
        {
            // 개인정보 거부 저장
            Close();
        }
        
        // 개인정보 동의 인터넷 페이지 연결 
        public void OnUserClickPrivacyPolicy()
        {
         //   Application.OpenURL("HomePage_ULR");
        }

    }
}