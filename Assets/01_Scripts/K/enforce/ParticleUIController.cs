using UnityEngine;
using UnityEngine.UI;

public class ParticleUIController : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public RawImage particleImage;

    void Start()
    {
        InitializeUIElement();
        SetParticlePositionToOrigin();
    }

    void InitializeUIElement()
    {
        if (particleImage == null)
        {
            particleImage = GetComponent<RawImage>();
            if (particleImage == null)
            {
                particleImage = gameObject.AddComponent<RawImage>();
            }
        }

        if (particleSystem == null)
        {
            particleSystem = gameObject.AddComponent<ParticleSystem>();
            // 파티클 시스템 설정 등 추가
        }

        particleImage.texture = particleSystem.GetComponent<ParticleSystemRenderer>().material.mainTexture;
    }

    void SetParticlePositionToOrigin()
    {
        RectTransform rectTransform = particleImage.rectTransform;
        rectTransform.anchoredPosition = Vector2.zero;
    }

}