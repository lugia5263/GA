using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniHUDBar : MonoBehaviour //∏ÛΩ∫≈Õ HUD 
{
    private StateManager stateMgr;
    private Canvas canvas;

    [SerializeField]
    public Image hpFillbar;

    void Awake()
    {
        canvas= gameObject.transform.GetComponentInChildren<Canvas>();
        hpFillbar = canvas.transform.GetChild(0).GetChild(1).GetComponent<Image>();
        stateMgr = this.GetComponent<StateManager>();
    }

    void Update()
    {
        float targetFillAmount = Mathf.InverseLerp(0, stateMgr.maxhp, stateMgr.hp);

        if (hpFillbar.fillAmount > targetFillAmount)
        {
            hpFillbar.fillAmount -= 3f * Time.deltaTime;
            hpFillbar.fillAmount = Mathf.Max(hpFillbar.fillAmount, targetFillAmount);
        }
    }
}
