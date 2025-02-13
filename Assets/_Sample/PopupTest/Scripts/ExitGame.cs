using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MySample
{
    /// <summary>
    ///  게임종료 창 : 예(게임종료), 아니요
    /// </summary>
    public class ExitGame : PopUp
    {
        #region Variables

        [SerializeField] private CustomButton yes;

        #endregion

        private void OnEnable()
        {
            yes.onClick.AddListener(QuitGame);
        }

        private void QuitGame()
        {
            // 게임 종료
            Application.Quit();
            Debug.Log("게임종료");
        }
    }
}