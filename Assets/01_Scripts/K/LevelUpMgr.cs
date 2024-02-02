using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using Photon.Pun;

public class LevelUpMgr : MonoBehaviour
{

    public DataMgrDontDestroy dataMgrDontDestroy;
    public TextAsset leveltxtFile; //Jsonfile
    public InventoryManager inventoryMgr;
    public GameObject lvupPanel;

    [Header("레벨업 패널")]
    public Text playerLvTxt;
    public Image expSlideImg;
    public Text expChartTxt;
    public Text playerHaveExpTxt;

    [Header("증가값 패널")]
    public Text beforeHealth;
    public Text afterHealth;
    public Text beforeCriPer;
    public Text afterCriPer;
    public Text beforeCriDmg;
    public Text afterCriDmg;

    private void Awake()
    {
        var jsonitemFile = Resources.Load<TextAsset>("Json/LvupTable");
        leveltxtFile = jsonitemFile;
        dataMgrDontDestroy = DataMgrDontDestroy.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PhotonView>().IsMine)
            {

                lvupPanel.SetActive(true);
            }
        }
    }
    public void InitLevelInfo()
    {

    }




    public void PlayerCheck() // 현재 플레이어 가진거 불러옴, npc 누를때 호출
    {
        playerHaveExpTxt.text = inventoryMgr.expPotion.ToString();
    }

    public void LevelUpCheck() // 레벨업 처음에 세팅됌
    {
        InitCheckLevel(dataMgrDontDestroy.level);
        lvupPanel.SetActive(true);
    }

    public void OnLevelUpBtn()
    {
        string json = leveltxtFile.text;
        var jsonData = JSON.Parse(json);


        //여기 if문으로 class보고 따로 받음

        //여기에 json의 추가량만큼 더해줌

        InitCheckLevel(dataMgrDontDestroy.level); // 초기화해줌
    }

    public void InitCheckLevel(int lv)
    {
        string json = leveltxtFile.text;
        var jsonData = JSON.Parse(json);

        playerLvTxt.text = dataMgrDontDestroy.level.ToString();
        playerHaveExpTxt.text = inventoryMgr.expPotion.ToString();
        beforeHealth.text = dataMgrDontDestroy.MaxHp.ToString();

        //여기서 클래스에 따른 json파일 가져옴

        afterHealth.text = (jsonData["LvWarrior"][lv]["PlayerHp"]);
        beforeCriPer.text = dataMgrDontDestroy.criChance.ToString();
        //afeterCriPer.text = json 다음꺼
        beforeCriPer.text = dataMgrDontDestroy.criDamage.ToString();
        //afterCriDmg.Text = json 다음꺼



        inventoryMgr.InitInventory(); // 인벤토리를 초기화해줌
    }
}
