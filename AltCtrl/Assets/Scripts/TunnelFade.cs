using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelFade : MonoBehaviour
{
    private Renderer objectRenderer;

    private void OnEnable()
    {
        Debug.Log("onEnable");
        if(objectRenderer == null)
        {
            objectRenderer = GetComponent<Renderer>();
        }
        StartCoroutine(FadeIn());
    }

    public IEnumerator FadeIn()
    {
        float timeElapsed = 0;
        float alpha = 0;

        while (timeElapsed < 3f)
        {
            alpha = Mathf.Lerp(0, 1, timeElapsed / 3f);

            timeElapsed += Time.deltaTime;

            Color C = objectRenderer.material.color;
            objectRenderer.material.SetFloat("_Metallic", alpha * 2);
            C.a = alpha;
            objectRenderer.material.color = C;

            yield return null;
        }
    }

    public IEnumerator FadeOut()
    {
        float timeElapsed = 0;
        float alpha = 0;

        while (timeElapsed < 3f)
        {
            alpha = Mathf.Lerp(1, 0, timeElapsed / 3f);

            timeElapsed += Time.deltaTime;

            Color C = objectRenderer.material.color;
            objectRenderer.material.SetFloat("_Metallic", alpha / 2);
            C.a = alpha;
            objectRenderer.material.color = C;

            yield return null;
        }
    }

}
