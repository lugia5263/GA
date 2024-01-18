using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentObject : MonoBehaviour
{
    Transform playerTransform;
    Vector3 Offset; // 카메라와 플레이어 사이의 거리 변수
    public bool IsTransparent { get; private set; } = false;
    MeshRenderer[] renderers;
    WaitForSeconds delay = new WaitForSeconds(0.001f);
    WaitForSeconds resetDelay = new WaitForSeconds(0.005f);
    const float THRESHOLD_ALPHA = 0.25f;
    const float THRESHOLD_MAX_TIMER = 0.5f;
   
    bool isReseting = false;
    float timer = 0f;
    Coroutine timeCheckCoroutine;
    Coroutine resetCoroutine;
    Coroutine becomeTransparentCoroutine;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BecomeTransparent()
    {
        if (IsTransparent)
        {
            timer = 0f;
            return;
        }

        if (resetCoroutine != null && isReseting)
        {
            isReseting = false;
            IsTransparent = false;
            StopCoroutine(resetCoroutine);
        }

        SetMaterialTransparent();
        IsTransparent = true;
        becomeTransparentCoroutine = StartCoroutine(BecomeTransparentCoroutine());
    }

    void SetMaterialRenderingMode(Material material, float mode, int renderQueue)
    {
        material.SetFloat("_Mode", mode);
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = renderQueue;
    }

    void SetMaterialTransparent()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            foreach (Material material in renderers[i].materials)
            {
                SetMaterialRenderingMode(material, 3f, 3000);
            }
        }
    }

    void SetMaterialOpaque()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            foreach (Material material in renderers[i].materials)
            {
                SetMaterialRenderingMode(material, 0f, -1);
            }
        }
    }
    public void ResetOriginalTransparent()
    {
        SetMaterialOpaque();
        resetCoroutine = StartCoroutine(ResetOriginalTransparentCoroutine());
    }
    IEnumerator BecomeTransparentCoroutine()
    {
        while (true)
        {
            bool isComplete = true;
            for (int i = 0; i < renderers.Length; i++)
            {
                if (renderers[i].material.color.a > THRESHOLD_ALPHA)
                    isComplete = false;

                Color color = renderers[i].material.color;
                color.a -= Time.deltaTime;
                renderers[i].material.color = color;
            }
            if (isComplete)
            {
                CheckTimer();
                break;
            }
            yield return delay;
        }
    }
    IEnumerator ResetOriginalTransparentCoroutine()
    {
        IsTransparent = false;
        while (true)
        {
            bool isComplete = true;
            for (int i = 0; i < renderers.Length; i++)
            {
                if (renderers[i].material.color.a < 1f)
                    isComplete = false;

                Color color = renderers[i].material.color;
                color.a += Time.deltaTime;
                renderers[i].material.color = color;
            }
            if (isComplete)
            {
                isReseting = false;
                break;
            }
            yield return resetDelay;
        }
    }
    public void CheckTimer()
    {
        if (timeCheckCoroutine != null)
            StopCoroutine(timeCheckCoroutine);
        timeCheckCoroutine = StartCoroutine(CheckTimerCouroutine());
    }

    IEnumerator CheckTimerCouroutine()
    {
        timer = 0f;
        while (true)
        {
            timer += Time.deltaTime;
            if (timer > THRESHOLD_MAX_TIMER)
            {
                isReseting = true;
                ResetOriginalTransparent();
                break;
            }
            yield return null;
        }
    }
}

