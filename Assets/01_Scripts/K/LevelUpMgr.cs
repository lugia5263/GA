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

    public StateManager stateMgr;
    public GameObject lvupPanel;
    public int classNum; //0:전사, 1:거너, 2:법사
    public int playerlv;
    public float playerHp;
    public int playerExp;
    public int playerExpPotion;
    public float playerCriDamage;
    public int playerCriChance;
    public int expRequire;

    [Header("레벨업 패널")]
    public Text playerLvTxt;
    public Image expSlideImg;
    public Text expRequireTxt;
    public Text curExpPotionTxt;

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
                Debug.Log("충돌일어남");
                PlayerDataCheck();
                Debug.Log("업데이트할 ui의 클래스넘버 : " + classNum);
                UpdateUiData(classNum);
                lvupPanel.SetActive(true);
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
                // 레벨업창 껐으니까 플레이어의 정보에 반영
                lvupPanel.SetActive(false);
            }
        }
    }

    public void OnClickOneBtn()
    {
        Debug.Log("현재 플레이어의 경험치는 : " + playerExp);
        if (playerExpPotion == 0) // 포션이 0개라면
        {
            Debug.Log("포션이 부족합니다.");
        }
        else // 포션이 0개가 아니라면
        {
            if (playerExp == expRequire) // 경험치가 최대로 모여있었다면
            {
                Debug.Log("if문 경험치 최대로 모였음");
                Debug.Log("레벨업 가능");
            }
            else // 경험치가 최대로 모여있지않으니 else문 실행
            {
                playerExpPotion -= 1;
                playerExp += 100;
                UpdateUiData(classNum);
            }
        }
    }
    public void OnClickMaxBtn()
    {
        Debug.Log("현재 플레이어의 경험치는 : " + playerExp);
        if (playerExpPotion == 0) // 포션이 0개라면
        {
            Debug.Log("포션이 부족합니다.");
        }
        else // 포션이 0개가 아니라면
        {
            if (playerExp == expRequire) // 경험치가 최대로 모여있었다면
            {
                Debug.Log("if문 경험치 최대로 모였음");
                Debug.Log("레벨업 가능");
            }
            else // 경험치가 최대로 모여있지않으니 else문 실행
            {
                playerExpPotion -= 1;
                playerExp += 100;
                UpdateUiData(classNum);
            }
        }
    }

    public void OnClickLevelUpBtn()
    {
        string json = leveltxtFile.text;
        var jsonData = JSON.Parse(json);

        playerExp = 0;
        playerlv++;
        UpdateUiData(classNum);

        playerHp = (jsonData[classNum][playerlv]["PlayerHp"]);
        playerCriDamage = (jsonData[classNum][playerlv]["CriDMG"]);
        playerCriChance = (jsonData[classNum][playerlv]["CriPer"]);

        // DataMgrDontDestroy에도 정보를 보내준다.
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

        string json = leveltxtFile.text;
        var jsonData = JSON.Parse(json);

        expRequire = jsonData["ExpRequireTable"][playerlv + 1]["needExp"];
    }
    public void UpdateUiData(int classNumber)
    {
        Debug.Log("현재 플레이어의 경험치는 : " + playerExp);
        Debug.Log("레벨업하기위해 필요한 경험치 량 : " + expRequire);
        string json = leveltxtFile.text;
        var jsonData = JSON.Parse(json);

        playerLvTxt.text = playerlv.ToString();
        beforeHealth.text = playerHp.ToString();
        afterHealth.text = (jsonData[classNumber][playerlv+1]["PlayerHp"]);
        beforeCriDmg.text = playerCriDamage.ToString();
        afterCriDmg.text = (jsonData[classNumber][playerlv + 1]["CriDMG"]);
        beforeCriPer.text = playerCriChance.ToString();
        afterCriPer.text = (jsonData[classNumber][playerlv + 1]["CriPer"]);
        curExpPotionTxt.text = playerExpPotion.ToString();
        expRequireTxt.text = $"{playerExp} / {expRequire}";
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
