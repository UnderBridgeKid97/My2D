using UnityEngine;
using TMPro;

namespace My2D
{
    public class HealthText : MonoBehaviour
    {
        #region Varialbes

        private TextMeshProUGUI textHealth;
        private RectTransform textTransform;

        // �̵�
        [SerializeField]private float moveSpeed = 5f;

        // ���̵� ȿ��
        private Color startColor;
        public float fadeTimer = 1f;
        private float countdown = 0f;
        #endregion

        private void Awake()
        {
            //  ����
            textHealth = GetComponent<TextMeshProUGUI>();
            textTransform= GetComponent<RectTransform>();
        }

        private void Start()
        {
            // �ʱ�ȭ 
            startColor = textHealth.color;
            countdown = fadeTimer;
        }

        private void Update()
        {
            // �̵� 
            textTransform.position += Vector3.up * moveSpeed * Time.deltaTime;

            // ���̵� ȿ��
            // ���̵�ƿ� ȿ�� spriteRenderer.color.a : 1->0
            countdown -= Time.deltaTime;

            // float newAlpha = startColor.a*(1- countdown /fadeTimer) ; =  0 ->1 ���̵� �� ȿ���� �̰ɷ� 
            float newAlpha = startColor.a * (countdown / fadeTimer);  // ��  ���İ��� ó������ ���ΰ�� ������ 1�� �ʱ�ȭ�̴ٰ� 0�� �Ǵ�  ���� ���İ��� �����ش�.
            textHealth.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);

            // ���̵� Ÿ�� ��
            if (countdown <= 0)
            {
                Destroy(gameObject);
            }

        }

















    }
}