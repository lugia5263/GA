using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpMgr : MonoBehaviour
{
    public StateManager stateMgr;
    public InventoryManager inventoryMgr;
    public int playerLv;
    public int playerExp;
    public int upMaxExp;

    [Header("레벨업 패널")]
    public Text playerLvTxt;
    public Image expSlideImg;
    public Text expChartTxt;
    public Text playerHaveExpTxt;

    private void Awake()
    {
        playerLvTxt = GameObject.Find("PlayerLevelInfo").GetComponent<Text>();
    }
    //Collider에서 stateMgr받고 열기!!!


    public void PlayerCheck() // 현재 가진거 체크
    {
        playerLvTxt.text = stateMgr.level.ToString();
        playerHaveExpTxt.text = inventoryMgr.expPotion.ToString();
        LevelUpCheck(playerLv);
    }

    public void LevelUpCheck(int lv) // 레벨업에 관한거
    {
        


        //여기에 패널 열기 함수();
    }

    public void OnLevelUpBtn()
    {

        InitCheckLevel();
    }

    public void InitCheckLevel()
    {
        inventoryMgr.playerLv.text = inventoryMgr.playerLv.ToString();
        inventoryMgr.expTxt.text = inventoryMgr.expPotion.ToString();
        playerLvTxt.text = stateMgr.level.ToString();
        playerHaveExpTxt.text = inventoryMgr.expPotion.ToString();
    }
}
