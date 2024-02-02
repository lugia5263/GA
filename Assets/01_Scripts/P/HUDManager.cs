using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUDManager : MonoBehaviour
{

    public StateManager stateManager;
    public Slider HpSlider;
    public Text HpText;


    public Image DHpBar;




    //���� �Լ��� �ǰ��������� �ҷ��´�!
    private void Awake()
    {
        stateManager = gameObject.GetComponent<StateManager>();
        InitHP();
        
    }


    public void InitHP()
    {
        DHpBar.fillAmount = 1;
        HpSlider.value = (stateManager.hp / stateManager.maxhp);
        HpText.text = ((int)stateManager.hp + "/" + (int)stateManager.maxhp).ToString();
    }

    public void InitHPBtn()
    {
        stateManager.hp -= 20f;
        HpSlider.value = (stateManager.hp / stateManager.maxhp);
        HpText.text = ((int)stateManager.hp + "/" + (int)stateManager.maxhp).ToString();
    }
    public void ChangeUserHUD()
    {
        HpSlider.value = (stateManager.hp / stateManager.maxhp);
        HpText.text = ((int)stateManager.hp + "/" + (int)stateManager.maxhp).ToString();

        if (stateManager.hp <= 0)
        {
            HpText.text = ("0" + "/" + (int)stateManager.maxhp).ToString();
        }
    }

    private void Update()
    {
        ChangeUserHUD();
        float targetFillAmount = Mathf.InverseLerp(0, stateManager.maxhp, stateManager.hp);

        if (DHpBar.fillAmount > targetFillAmount)
        {
            DHpBar.fillAmount -= 0.1f * Time.deltaTime;
            DHpBar.fillAmount = Mathf.Max(DHpBar.fillAmount, targetFillAmount);
        }
        if(DHpBar.fillAmount < targetFillAmount)
        {
            DHpBar.fillAmount += 0.1f * Time.deltaTime;
            DHpBar.fillAmount = Mathf.Max(DHpBar.fillAmount, targetFillAmount);
        }
    }
    public void ChangeMainHP()
    {

    }





}
