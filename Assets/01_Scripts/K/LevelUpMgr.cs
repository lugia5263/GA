using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using Photon.Pun;

public class LevelUpMgr : MonoBehaviourPunCallbacks
{
    public DataMgrDontDestroy dataMgrDontDestroy;
    public TextAsset leveltxtFile; //Jsonfile
    public GameObject lvupPanel;
    public Slider expSlider;
    public int classNum; //0:����, 1:�ų�, 2:����
    public int playerlv;
    public float playerHp;
    public int playerExp;
    public int playerExpPotion;
    public float playerCriDamage;
    public int playerCriChance;
    public int expRequire;

    [Header("������ �г�")]
    public Text playerLvTxt;
    public Image expSlideImg;
    public Text expRequireTxt;
    public Text curExpPotionTxt;

    [Header("������ �г�")]
    public Text beforeHealth;
    public Text afterHealth;
    public Text beforeCriPer;
    public Text afterCriPer;
    public Text beforeCriDmg;
    public Text afterCriDmg;

    [Header("afterExp, ExpPotion(�ǹݿ��ȵ�)")]
    public int afterExp;
    public int afterExpPotionCnt;

    private void Awake()
    {
        var jsonitemFile = Resources.Load<TextAsset>("Json/LvupTable");
        leveltxtFile = jsonitemFile;

        dataMgrDontDestroy = DataMgrDontDestroy.Instance;
        lvupPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PhotonView>().IsMine)
            {
                Debug.Log("�浹�Ͼ");
                PlayerDataCheck();
                Debug.Log("������Ʈ�� ui�� Ŭ�����ѹ� : " + classNum);
                UpdateUiData(classNum);
                lvupPanel.SetActive(true);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PhotonView>().IsMine)
            {
                SyncDataMgr();
                StateManager stateManager = other.gameObject.GetComponent<StateManager>();
                stateManager.level = playerlv;
                stateManager.maxhp = playerHp;
                stateManager.hp = playerHp;
                stateManager.criChance = playerCriChance;
                stateManager.criDamage = playerCriDamage;
                stateManager.exp = playerExp;
                stateManager.userExpPotion = playerExpPotion;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PhotonView>().IsMine)
            {
                SyncDataMgr();
                lvupPanel.SetActive(false);
            }
        }
    }

    public void OnClickOneBtn()
    {
        if (afterExpPotionCnt == 0) // ������ 0�����
        {
            Debug.Log("������ �����մϴ�.");
        }
        else // ������ 0���� �ƴ϶��
        {
            if (afterExp == expRequire) // ����ġ�� �ִ�� ���־��ٸ�
            {
                Debug.Log("if�� ����ġ �ִ�� ����");
                Debug.Log("������ ����");
            }
            else // ����ġ�� �ִ�� ������������ else�� ����
            {
                afterExpPotionCnt -= 1;
                afterExp += 100;
                UpdateUiData(classNum);
            }
        }
    }
    public void OnClickMaxBtn()
    {
        if (afterExpPotionCnt == 0) // ������ 0�����
        {
            Debug.Log("������ �����մϴ�.");
        }
        else // ������ 0���� �ƴ϶��
        {
            if (afterExp == expRequire) // ����ġ�� �ִ�� ���־��ٸ�
            {
                Debug.Log("if�� ����ġ �ִ�� ����");
                Debug.Log("������ ����");
            }
            else // ����ġ�� �ִ�� ������������ else�� ����
            {
                int n = (expRequire - afterExp) / 100;
                Debug.Log("�ѹ��� ����� ���� : " + n);
                if (n > afterExpPotionCnt)
                {
                    afterExp += afterExpPotionCnt * 100;
                    afterExpPotionCnt = 0;
                }
                else
                {
                    afterExpPotionCnt -= n;
                    afterExp += n * 100;
                }
                UpdateUiData(classNum);
            }
        }
    }

    public void OnClickLevelUpBtn()
    {
        string json = leveltxtFile.text;
        var jsonData = JSON.Parse(json);

        if (afterExp==expRequire)
        {
            playerExpPotion = afterExpPotionCnt;
            playerExp = 0;
            afterExp = 0;
            playerlv++;

            playerHp = (jsonData[classNum][playerlv]["PlayerHp"]);
            playerCriDamage = (jsonData[classNum][playerlv]["CriDMG"]);
            playerCriChance = (jsonData[classNum][playerlv]["CriPer"]);
            expRequire = jsonData["ExpRequireTable"][playerlv]["needExp"];
            UpdateUiData(classNum);
        }
        else
        {
            playerExpPotion = afterExpPotionCnt;
            playerExp = afterExp;

            playerHp = (jsonData[classNum][playerlv]["PlayerHp"]);
            playerCriDamage = (jsonData[classNum][playerlv]["CriDMG"]);
            playerCriChance = (jsonData[classNum][playerlv]["CriPer"]);
            expRequire = jsonData["ExpRequireTable"][playerlv]["needExp"];
            UpdateUiData(classNum);
        }

        // DataMgrDontDestroy���� ������ �����ش�.
        dataMgrDontDestroy.Level = playerlv;
        dataMgrDontDestroy.Exp = playerExp;
        dataMgrDontDestroy.UserExpPotion = playerExpPotion;
        dataMgrDontDestroy.MaxHp = playerHp;
        dataMgrDontDestroy.CriDamage = playerCriDamage;
        dataMgrDontDestroy.CriChance = playerCriChance;
    }

    public void PlayerDataCheck()
    {
        playerlv = dataMgrDontDestroy.Level;
        playerExp = dataMgrDontDestroy.Exp;
        playerExpPotion = dataMgrDontDestroy.UserExpPotion;
        playerHp = dataMgrDontDestroy.MaxHp;
        playerCriDamage = dataMgrDontDestroy.CriDamage;
        playerCriChance = dataMgrDontDestroy.CriChance;
        classNum = dataMgrDontDestroy.ClassNum;

        afterExpPotionCnt = playerExpPotion;
        afterExp = playerExp;

        //playerExpPotion = afterExpPotionCnt;
        //playerExp = afterExp;

        string json = leveltxtFile.text;
        var jsonData = JSON.Parse(json);
        expRequire = jsonData["ExpRequireTable"][playerlv]["needExp"];
    }
    public void UpdateUiData(int classNumber)
    {
        Debug.Log("���� �÷��̾��� ����ġ�� : " + playerExp);
        Debug.Log("�������ϱ����� �ʿ��� ����ġ �� : " + expRequire);
        string json = leveltxtFile.text;
        var jsonData = JSON.Parse(json);

        playerLvTxt.text = playerlv.ToString();
        beforeHealth.text = playerHp.ToString();
        afterHealth.text = (jsonData[classNumber][playerlv+1]["PlayerHp"]);
        beforeCriDmg.text = playerCriDamage.ToString();
        afterCriDmg.text = (jsonData[classNumber][playerlv + 1]["CriDMG"]);
        beforeCriPer.text = playerCriChance.ToString();
        afterCriPer.text = (jsonData[classNumber][playerlv + 1]["CriPer"]);
        curExpPotionTxt.text = afterExpPotionCnt.ToString();
        expRequireTxt.text = $"{afterExp} / {expRequire}";
        expSlider.value = ((float)afterExp / expRequire);
    }

    public void SyncDataMgr()
    {
        dataMgrDontDestroy.Level = playerlv;
        dataMgrDontDestroy.Exp = playerExp;
        dataMgrDontDestroy.UserExpPotion = playerExpPotion;
        dataMgrDontDestroy.MaxHp = playerHp;
        dataMgrDontDestroy.CriDamage = playerCriDamage;
        dataMgrDontDestroy.CriChance = playerCriChance;
        dataMgrDontDestroy.ClassNum = classNum;
    }
}
