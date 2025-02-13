using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MySample
{
    /// <summary>
    /// ���� �˾��� �����ϴ� �̱��� Ŭ���� 
    /// </summary>
    public class MenuManager : PersistentSingleton<MenuManager>
    {
        #region Variables

        public List<PopUp> popupStack = new List<PopUp>();

        [SerializeField]private Canvas canvas;                              // �˾��޴��� �θ� ĵ���� ������Ʈ

        #endregion

        private void OnEnable()
        {
            // �˾��� ����ƽ �̺�Ʈ �Լ� ����ϱ�
            PopUp.OnClosePopup += ClosePopup;
            PopUp.OnBeforeClosePopup += OnBeforeCloseAction;
        }

        private void OnDisable()
        {
            // �˾��� ����ƽ �̺�Ʈ �Լ� �����ϱ�
            PopUp.OnClosePopup -= ClosePopup;
            PopUp.OnBeforeClosePopup -= OnBeforeCloseAction;

        }

        private void OnBeforeCloseAction(PopUp popupClose)
        {
            // TODO :���̵� ȿ�� 
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

        // �˾�â ���� / ���׸�Ŭ���� ���� �ʿ�
        public T ShowPopup<T>(Action onShow = null,Action<popupResult> onClose = null) where T: PopUp
        {
            // �̹� â�� �����ִ��� üũ
            if(popupStack.OfType<T>().Any())           // ?????
            {
                return popupStack.OfType<T>().First(); // ?????
            }
                  
                // ���׸����� ĳ����  
            return (T) ShowPopup("Popups/"+typeof(T).Name,onShow, onClose); //���ν� �ε� ��� ���� (???)

        }

        public PopUp ShowPopup(string pathWithType, Action onShow = null, Action<popupResult> onClose = null)
        {
            // �̹� â�� ���Ǵ��� üũ 
            if(popupStack.Any(p => p.GetType().Name == pathWithType.Split('/').Last()))
            {
                return popupStack.First(p => p.GetType().Name == pathWithType.Split('/').Last());
                // -> �̹� ������ ����
            }

            // ������ 
            var popupPrefab = Resources.Load<PopUp>(pathWithType);
            if( popupPrefab == null )
            {
                Debug.Log("ã�� �������� �����ϴ�");
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

        // Ư��Ÿ��(T)�� �˾� ã�� 
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

        // �����ִ� ��� â �ݱ� 
        public void CloseAllPopups()
        {
            for (int i= 0; i< popupStack.Count; i++)
            {
                var popup = popupStack[i];
                popup.Close();
            }
            popupStack.Clear(); // ����Ʈ�� ���õ� Ŭ���� 
        }

        // â�� �ϳ��� ���� �ֳ�?
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