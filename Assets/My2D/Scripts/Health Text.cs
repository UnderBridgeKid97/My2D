using UnityEngine;
using TMPro;

namespace My2D
{
    public class HealthText : MonoBehaviour
    {
        #region Varialbes

        private TextMeshProUGUI textHealth;
        private RectTransform textTransform;

        // 이동
        [SerializeField]private float moveSpeed = 5f;

        // 페이드 효과
        private Color startColor;
        public float fadeTimer = 1f;
        private float countdown = 0f;
        #endregion

        private void Awake()
        {
            //  참조
            textHealth = GetComponent<TextMeshProUGUI>();
            textTransform= GetComponent<RectTransform>();
        }

        private void Start()
        {
            // 초기화 
            startColor = textHealth.color;
            countdown = fadeTimer;
        }

        private void Update()
        {
            // 이동 
            textTransform.position += Vector3.up * moveSpeed * Time.deltaTime;

            // 페이드 효과
            // 페이드아웃 효과 spriteRenderer.color.a : 1->0
            countdown -= Time.deltaTime;

            // float newAlpha = startColor.a*(1- countdown /fadeTimer) ; =  0 ->1 페이드 인 효과는 이걸로 
            float newAlpha = startColor.a * (countdown / fadeTimer);  // ★  알파값이 처음부터 반인경우 죽으면 1로 초기화됫다가 0이 되니  시작 알파값을 곱해준다.
            textHealth.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);

            // 페이드 타임 끝
            if (countdown <= 0)
            {
                Destroy(gameObject);
            }

        }

















    }
}