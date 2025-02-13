using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My2D
{
    public class Ghost : MonoBehaviour
    {
        #region Variables

        //
        private bool isTrailActive = false;
        [SerializeField]private float trailActiveTime = 2f;    // �ܻ�ȿ�� ��ȿ�ð�
        [SerializeField]private float trailRefreshRate = 0.1f;  // �ܻ���� �߻� ����
        [SerializeField]private float trailDestroyDeley = 1f;   // 1�� �� ų - > ���̵�ƿ� 

        private SpriteRenderer playerRenderer;
        public Material ghostMaterial;  // �ܻ� ���׸���
        [SerializeField]private string shaderValueRef = "_Alpha";
        [SerializeField]private float shaderValueRate = 0.1f; //���İ� ���� ����
        [SerializeField] private float shaderValueRefreshRate = 0.05f; // ���İ� ���ҵǴ� �ð� ����
        #endregion

        private void Awake()
        {
            playerRenderer = GetComponent<SpriteRenderer>();
        }

        // 2�ʵ��� �ܻ�ȿ�� �߻�
        public void StartActiveTrail()
        {
            if(isTrailActive)
            {
                return;
            }

            isTrailActive = true;
            StartCoroutine(ActiveTrail(trailActiveTime));


        }
        //activeTime ���� �ܻ�ȿ�� �߻�
        IEnumerator ActiveTrail(float activeTime)
        {
            while(activeTime >0)
            {
                activeTime -= Time.deltaTime;

                // �ܻ�ȿ������� - ������ġ��
                GameObject ghostObject = new GameObject(); // ���̶�Űâ�� �� ������Ʈ ����� 
                ghostObject.transform.SetLocalPositionAndRotation(transform.position, transform.rotation);
                ghostObject.transform.localScale = transform.localScale;

                // SpriteRenderer ���� 
                SpriteRenderer renderer = ghostObject.AddComponent<SpriteRenderer>();
                renderer.sprite = playerRenderer.sprite;
                renderer.sortingLayerName = playerRenderer.sortingLayerName;
                renderer.sortingOrder = playerRenderer.sortingOrder - 1;
                renderer.material = ghostMaterial;

                // ���׸��� �Ӽ�(���İ�) ����
                StartCoroutine(AnimateMaterialFloat(renderer.material,shaderValueRef,0f,shaderValueRate,shaderValueRefreshRate));

                Destroy(ghostObject,1f);

                yield return new WaitForSeconds(trailRefreshRate);
            }
            isTrailActive=false;
        }

        // ���׸��� �Ӽ�(���İ�) ����
        IEnumerator AnimateMaterialFloat(Material mat, string valueRef,float goal, float rate, float refreshRate)
        {
            float valueToAnimate = mat.GetFloat(valueRef);

            while(valueToAnimate > goal)
            {
                valueToAnimate -= rate;
                mat.SetFloat(valueRef, valueToAnimate);

                yield return new WaitForSeconds(refreshRate);

            }

        }







    }
}