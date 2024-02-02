using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class LevelUpMgr : MonoBehaviour
{
    public DataMgrDontDestroy dataMgrDontDestroy;
    public TextAsset leveltxtFile; //Jsonfile


    public StateManager stateMgr;
    public InventoryManager inventoryMgr;
    public GameObject lvupPanel;
    public int playerlv;
    public int classNum;
    public int playerExp;
    public int upMaxExp;

    [Header("������ �г�")]
    public Text playerLvTxt;
    public Image expSlideImg;
    public Text expChartTxt;
    public Text playerHaveExpTxt;

    [Header("������ �г�")]
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
        lvupPanel = GameObject.Find("LevelUpPanel");
        playerLvTxt = GameObject.Find("PlayerLevelInfo").GetComponent<Text>();
    }


    public void PlayerCheck() // ���� �÷��̾� ������ �ҷ���, npc ������ ȣ��
    {
        playerlv = dataMgrDontDestroy.level;
        playerHaveExpTxt.text = inventoryMgr.expPotion.ToString();
        
    }

    public void LevelUpCheck() // ������ ó���� ���É�
    {
        InitCheckLevel(playerlv,classNum);
        lvupPanel.SetActive(true);
    }

    public void OnLevelUpBtn()
    {
        string json = leveltxtFile.text;
        var jsonData = JSON.Parse(json);


        //���� if������ class���� ���� ����

        //���⿡ json�� �߰�����ŭ ������

        InitCheckLevel(playerlv, classNum); // �ʱ�ȭ����
    }

    public void InitCheckLevel(int lv, int classnum)
    {
        string json = leveltxtFile.text;
        var jsonData = JSON.Parse(json);

        playerLvTxt.text = dataMgrDontDestroy.level.ToString();
        playerHaveExpTxt.text = inventoryMgr.expPotion.ToString();
        beforeHealth.text = dataMgrDontDestroy.MaxHp.ToString();

        //���⼭ Ŭ������ ���� json���� ������

        afterHealth.text = (jsonData["LvWarrior"][lv]["PlayerHp"]);
        beforeCriPer.text = stateMgr.criChance.ToString();
        //afeterCriPer.text = json ������
        beforeCriPer.text = stateMgr.criDamage.ToString();
        //afterCriDmg.Text = json ������



        inventoryMgr.InitInventory(); // �κ��丮�� �ʱ�ȭ����
    }
}
