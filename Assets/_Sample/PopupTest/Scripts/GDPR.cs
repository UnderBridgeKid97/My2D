using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MySample
{
    /// <summary>
    ///  �������� ���� â: ����, ���, �������� ������ ��ũ
    /// </summary>
    public class GDPR :PopUp
    {
        #region Variables
        #endregion

        // ���� ���� ����
        public void OnUserClickAcept()
        {
            // ����������������
            Close();
        }

        // �������� ���� â ���
        public void OnUserClickCancel()
        {
            // �������� �ź� ����
            Close();
        }
        
        // �������� ���� ���ͳ� ������ ���� 
        public void OnUserClickPrivacyPolicy()
        {
         //   Application.OpenURL("HomePage_ULR");
        }

    }
}