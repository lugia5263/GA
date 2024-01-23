using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniHUDBar : MonoBehaviour //∏ÛΩ∫≈Õ HUD 
{
    [SerializeField]
    private StateManager stateManager;

    [SerializeField]
    Image hpFillbar;

    private void Awake()
    {
        stateManager = this.GetComponent<StateManager>();
    }

    private void Update()
    {
        float targetFillAmount = Mathf.InverseLerp(0, stateManager.maxhp, stateManager.hp);

        if(hpFillbar.fillAmount > targetFillAmount )
        {
            hpFillbar.fillAmount -= 3f * Time.deltaTime;
            hpFillbar.fillAmount = Mathf.Max(hpFillbar.fillAmount, targetFillAmount);
        }
    }
}
