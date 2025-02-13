using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MySample
{
    /// <summary>
    /// 메인 팝업을 관리하는 싱글톤 클래스 
    /// </summary>
    public class MenuManager : PersistentSingleton<MenuManager>
    {
        #region Variables

        public List<PopUp> popupStack = new List<PopUp>();

        [SerializeField]private Canvas canvas;                              // 팝업메뉴의 부모 캔버스 오브젝트

        #endregion

        private void OnEnable()
        {
            // 팝업의 스태틱 이벤트 함수 등록하기
            PopUp.OnClosePopup += ClosePopup;
            PopUp.OnBeforeClosePopup += OnBeforeCloseAction;
        }

        private void OnDisable()
        {
            // 팝업의 스태틱 이벤트 함수 해제하기
            PopUp.OnClosePopup -= ClosePopup;
            PopUp.OnBeforeClosePopup -= OnBeforeCloseAction;

        }

        private void OnBeforeCloseAction(PopUp popupClose)
        {
            // TODO :페이드 효과 
        }

        private void ClosePopup(PopUp popupClose)
        {
            if(popupStack.Count >0)
            {
                popupStack.Remove(popupClose);
                if(popupStack.Count > 0 )
                {
                    var popup = popupStack.Last<PopUp>();
                    popup.Show();
                }
            }
        }

        // 팝업창 열기 / 제네릭클래스 공부 필요
        public T ShowPopup<T>(Action onShow = null,Action<popupResult> onClose = null) where T: PopUp
        {
            // 이미 창이 열려있는지 체크
            if(popupStack.OfType<T>().Any())           // ?????
            {
                return popupStack.OfType<T>().First(); // ?????
            }
                  
                // 제네릭으로 캐스팅  
            return (T) ShowPopup("Popups/"+typeof(T).Name,onShow, onClose); //리로스 로드 경로 지정 (???)

        }

        public PopUp ShowPopup(string pathWithType, Action onShow = null, Action<popupResult> onClose = null)
        {
            // 이미 창이 열렷는지 체크 
            if(popupStack.Any(p => p.GetType().Name == pathWithType.Split('/').Last()))
            {
                return popupStack.First(p => p.GetType().Name == pathWithType.Split('/').Last());
                // -> 이미 있으면 리턴
            }

            // 없으면 
            var popupPrefab = Resources.Load<PopUp>(pathWithType);
            if( popupPrefab == null )
            {
                Debug.Log("찾는 프리팹이 없습니다");
                return null;
            }

            return ShowPopup(popupPrefab, onShow, onClose);
        }

        public PopUp ShowPopup(PopUp popUpPrefab, Action onShow = null, Action<popupResult> onClose = null)
        {
            var popup = Instantiate(popUpPrefab, canvas.transform);

            if(popupStack.Count > 0)
            {
                popupStack.Last().Hide(); 
            }

            popupStack.Add(popup);
            popup.Show<PopUp>(onShow, onClose);
            var recTransform = popup.GetComponent<RectTransform>();
            recTransform.anchoredPosition = Vector2.zero;
            recTransform.sizeDelta = Vector2.zero;

            return popup;
        }

        // 특정타입(T)의 팝업 찾기 
        public T GetPopupOpen<T>() where T : PopUp
        {
            foreach( var popup in popupStack)
            {
                if(popup.GetType() == typeof(T))
                {
                     return (T)popup;
                }
            }
            return null;
        }

        // 열려있는 모든 창 닫기 
        public void CloseAllPopups()
        {
            for (int i= 0; i< popupStack.Count; i++)
            {
                var popup = popupStack[i];
                popup.Close();
            }
            popupStack.Clear(); // 리스트에 스택도 클리어 
        }

        // 창이 하나라도 열려 있나?
        public bool IsAnyPopupOpen()
        {
            return popupStack.Count > 0; 
        }

        public PopUp GetLastPopup()
        {
            return popupStack.Last();
        }

    }
}