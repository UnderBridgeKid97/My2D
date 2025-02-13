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
        [SerializeField]private float trailActiveTime = 2f;    // 잔상효과 유효시간
        [SerializeField]private float trailRefreshRate = 0.1f;  // 잔상들의 발생 간격
        [SerializeField]private float trailDestroyDeley = 1f;   // 1초 후 킬 - > 페이드아웃 

        private SpriteRenderer playerRenderer;
        public Material ghostMaterial;  // 잔상 마테리얼
        [SerializeField]private string shaderValueRef = "_Alpha";
        [SerializeField]private float shaderValueRate = 0.1f; //알파값 감소 비율
        [SerializeField] private float shaderValueRefreshRate = 0.05f; // 알파값 감소되는 시간 간격
        #endregion

        private void Awake()
        {
            playerRenderer = GetComponent<SpriteRenderer>();
        }

        // 2초동안 잔상효과 발생
        public void StartActiveTrail()
        {
            if(isTrailActive)
            {
                return;
            }

            isTrailActive = true;
            StartCoroutine(ActiveTrail(trailActiveTime));


        }
        //activeTime 동안 잔상효과 발생
        IEnumerator ActiveTrail(float activeTime)
        {
            while(activeTime >0)
            {
                activeTime -= Time.deltaTime;

                // 잔상효과만들기 - 현재위치에
                GameObject ghostObject = new GameObject(); // 하이라키창에 빈 오브젝트 만들기 
                ghostObject.transform.SetLocalPositionAndRotation(transform.position, transform.rotation);
                ghostObject.transform.localScale = transform.localScale;

                // SpriteRenderer 셋팅 
                SpriteRenderer renderer = ghostObject.AddComponent<SpriteRenderer>();
                renderer.sprite = playerRenderer.sprite;
                renderer.sortingLayerName = playerRenderer.sortingLayerName;
                renderer.sortingOrder = playerRenderer.sortingOrder - 1;
                renderer.material = ghostMaterial;

                // 마테리얼 속성(알파값) 감소
                StartCoroutine(AnimateMaterialFloat(renderer.material,shaderValueRef,0f,shaderValueRate,shaderValueRefreshRate));

                Destroy(ghostObject,1f);

                yield return new WaitForSeconds(trailRefreshRate);
            }
            isTrailActive=false;
        }

        // 마테리얼 속성(알파값) 감소
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