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




    public void PlayerCheck() // ���� �÷��̾� ������ �ҷ���, npc ������ ȣ��
    {
        playerHaveExpTxt.text = inventoryMgr.expPotion.ToString();
    }

    public void LevelUpCheck() // ������ ó���� ���É�
    {
        InitCheckLevel(dataMgrDontDestroy.level);
        lvupPanel.SetActive(true);
    }

    public void OnLevelUpBtn()
    {
        string json = leveltxtFile.text;
        var jsonData = JSON.Parse(json);


        //���� if������ class���� ���� ����

        //���⿡ json�� �߰�����ŭ ������

        InitCheckLevel(dataMgrDontDestroy.level); // �ʱ�ȭ����
    }

    public void InitCheckLevel(int lv)
    {
        string json = leveltxtFile.text;
        var jsonData = JSON.Parse(json);

        playerLvTxt.text = dataMgrDontDestroy.level.ToString();
        playerHaveExpTxt.text = inventoryMgr.expPotion.ToString();
        beforeHealth.text = dataMgrDontDestroy.MaxHp.ToString();

        //���⼭ Ŭ������ ���� json���� ������

        afterHealth.text = (jsonData["LvWarrior"][lv]["PlayerHp"]);
        beforeCriPer.text = dataMgrDontDestroy.criChance.ToString();
        //afeterCriPer.text = json ������
        beforeCriPer.text = dataMgrDontDestroy.criDamage.ToString();
        //afterCriDmg.Text = json ������



        inventoryMgr.InitInventory(); // �κ��丮�� �ʱ�ȭ����
    }
}
