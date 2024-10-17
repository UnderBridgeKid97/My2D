using UnityEngine;
using TMPro;

namespace My2D
{
    public class UIManager : MonoBehaviour
    {
        #region Variables
        public GameObject damageTextPrefab;
        public GameObject healTextPrefab;

        private Canvas canvas;
        [SerializeField] private Vector3 healthTextOffset = Vector3.zero;

        #endregion

        private void Awake()
        {
            // ����
            canvas = FindObjectOfType<Canvas>();


        }


        private void OnEnable()
        {
            // ĳ���� ���� �̺�Ʈ �Լ� ���
            CharactorEvent.characterDamaged += CharacterDamaged;
            CharactorEvent.characterHealed += CharacterHealed;
        }
        private void OnDisable()
        {
            CharactorEvent.characterDamaged -= CharacterDamaged;
            CharactorEvent.characterHealed -= CharacterHealed;
        }


        public void CharacterDamaged(GameObject character, float damage)
        {
            // damageTextPrefab ���� 
            Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

            GameObject textGo = Instantiate(damageTextPrefab, spawnPosition + healthTextOffset, Quaternion.identity, canvas.transform.transform);
            TextMeshProUGUI damageText = textGo.GetComponent<TextMeshProUGUI>();
            damageText.text = damage.ToString();

        }

        public void CharacterHealed(GameObject character, float restore)
        {
            // damageTextPrefab ���� 
            Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

            GameObject textGo = Instantiate(healTextPrefab, spawnPosition + healthTextOffset, Quaternion.identity, canvas.transform.transform);
            TextMeshProUGUI healText = textGo.GetComponent<TextMeshProUGUI>();
            healText.text = restore.ToString();
        }
    }
}
