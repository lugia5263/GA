using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using SimpleJSON;
using static UnityEditor.Progress;
public class QuestManager : MonoBehaviour
{

    public TextAsset txtFile; //Jsonfile
    public GameObject jsonObject; //안써도 됨
    public QuestPopUpManager qPopup;
    public RewardMgr rewardMgr;
    public InventoryManager inventoryMgr;

    // 퀘스트 팝업은 싱글톤으로 다른 씬을 갔다가 온다. 그러므로 퀘스트 NPC에 상호작용을 할 때 
    // 현재 팝업에 있는 카운트를 퀘스트 매니저 스크립트에 있는 현재, 최대 카운트 값을 보내줘야한다
    // 그리고 상호작용할때 퀘스트가 완료되었을 때 bool값이 True인지 확인해서
    // 퀘스트완료 버튼을 SetActive = true 로 한다.
    // npc 커밋하고 할것. 

    public GameObject questCanvas;
    public Text questNameTxt;
    public Text goalNameTxt;
    public Image questRewards;
    public GameObject descriptionPanel;

    public int acceptIdx;

    [Header("퀘스트팝업")]
    public GameObject questPopUpPanel;
    public Text questGoalTxt;
    public int questCurCount;
    //public int questMaxCount;

    [Header("퀘스트 보상목록 표시")]
    public Text rewardExp;
    public Text rewardMat;
    public Text rewardGold;

    [Header("퀘스트 수락버튼")]

    public GameObject acceptBtn;
    public GameObject ingBtn;
    public GameObject completedBtn;
    public GameObject questEndBtn;

    public bool isQuestIng;
    public bool isCompleted;
    public bool isEnd;

    //Player enterPlayer;


    public void CurQuestCheck()
    {
        if(questCurCount == 0)
        {
            return;
        }
        else
        {
            if(acceptIdx == qPopup.curQuestIndex)
            {
                descriptionPanel.SetActive(true);
                if (qPopup.curCount >= qPopup.maxCount)
                {
                    completedBtn.SetActive(true);
                    isCompleted = true;
                }
                else
                {
                    return;
                }
                InstQuest(acceptIdx);
            }
            
        }

    }

    private void Awake()
    {
        Debug.Log("Start: Trying to find Buttons");

        questNameTxt = GameObject.Find("questNameTxt").GetComponent<Text>();
        goalNameTxt = GameObject.Find("goalNameTxt").GetComponent<Text>();
        questRewards = GameObject.Find("QuestRewards").GetComponent<Image>();
        questPopUpPanel = GameObject.Find("QuestPopUp");
        questGoalTxt = GameObject.Find("GoalTxt").GetComponent<Text>();
        qPopup = GameObject.Find("QuestPopUp").GetComponent<QuestPopUpManager>();
        rewardMgr = GameObject.Find("RewardMgr").GetComponent<RewardMgr>();
        inventoryMgr = GameObject.Find("InventoryMgr").GetComponent<InventoryManager>();
        ingBtn = GameObject.Find("QuestIngBtn");
        completedBtn = GameObject.Find("CompletedBtn");
        questEndBtn = GameObject.Find("QuestEndBtn");


    }
    private void Start()
    {
        questPopUpPanel.SetActive(false);

        ingBtn.SetActive(false);
        completedBtn.SetActive(false);
        questEndBtn.SetActive(false);
        
        descriptionPanel.SetActive(false);
    }


    public void InstQuest(int n)
    {
        string json = txtFile.text;
        var jsonData = JSON.Parse(json); //var의 의미: Unity외의 파일을 다가져온다.

        int item = n - 1; //매개변수

        //GameObject character = Instantiate(jsonObject);

        if(n >= acceptIdx || acceptIdx !=0)
        {
            questNameTxt.text = (jsonData["Quest"][item]["QuestName"]);
            goalNameTxt.text = (jsonData["Quest"][item]["Goal"]);
            rewardExp.text = (jsonData["Quest"][item]["Reward1"]);
            rewardMat.text = (jsonData["Quest"][item]["Reward2"]);
            rewardGold.text = (jsonData["Quest"][item]["Reward3"]);
            questCurCount = n;
            acceptIdx = n;
        }




        #region
        //character.transform.name = (jsonData["시트1"][n]["QuestName"]);
        //character.GetComponent<QuestData>().charname = (jsonData["시트1"][n]["QuestName"]);
        //character.GetComponent<QuestData>().atk = (int)(jsonData["시트1"][n]["Count"]);
        ////character.GetComponent<QuestData>().count++; //QuestData의 카운트 증가

        //character.tag = "Player"; //prefab에 태그를 달거야.

        //character.transform.SetParent(questCanvas.transform); //나는 questCanvas를 부모로 두고 응애하고 Prefab이 태어남.
        #endregion
    }

    public void AcceptBtn()
    {
        ReceiveQuest(acceptIdx);


    }
    public void ReceiveQuest(int n) // 수락버튼 눌렀을 때
    {
        n = acceptIdx;
        string json = txtFile.text;
        var jsonData = JSON.Parse(json); //var의 의미: Unity외의 파일을 다가져온다.
        int item = n - 1;

        qPopup.curCount = 0;
        
        ingBtn.SetActive(false);
        completedBtn.SetActive(false);
        questEndBtn.SetActive(false);
        
        qPopup.maxCount = (int)(jsonData["Quest"][item]["Count"]);
        qPopup.questCountTxt.text = $"({qPopup.curCount} / {(jsonData["Quest"][item]["Count"])})";
        qPopup.curQuestIndex = (int)(jsonData["Quest"][item]["QuestNum"]);
        isCompleted = false;


        questGoalTxt.text = (jsonData["Quest"][item]["Goal"]);
        //qPopup.questCountTxt.text = $"({questCurCount} / {(jsonData["Quest"][item]["Count"])})";
 
        qPopup.curQuestIndex = (int)(jsonData["Quest"][item]["QuestNum"]);
        //rewardExp.text = (jsonData["Quest"][item]["Reward1"]);
        //rewardMat.text = (jsonData["Quest"][item]["Reward2"]);
        //rewardGold.text = (jsonData["Quest"][item]["Reward3"]);

        isQuestIng = true;
        qPopup.InitCurQuest();
        questPopUpPanel.SetActive(true);
        ingBtn.SetActive(true);

    }

    public void QuestCompleted() // 완료
    {
        qPopup.InitCurQuest();

        //########보상 팝업#####################################
        //수령받을 수 있는 팝업 on에 현재 json에 있는 보상함수에 ACCEPTiDX 매개변수로
        rewardMgr.Reward100exp3EABtn();

    }

    public void QuestSendBtn()
    {

        string json = txtFile.text;
        var jsonData = JSON.Parse(json); //var의 의미: Unity외의 파일을 다가져온다.
        //완료 버튼에 이거 넣기 (지금은 자동)
        inventoryMgr.SendInventory();

        //위에꺼 넣기

        ingBtn.SetActive(false);
        completedBtn.SetActive(false);
        questEndBtn.SetActive(false);

        qPopup.curQuestIndex++;
        acceptIdx++;
      
        questGoalTxt.text = (jsonData["Quest"][acceptIdx]["Goal"]);

        Debug.Log("Tlqkf");
        qPopup.InitCurQuest();

        questPopUpPanel.SetActive(false);
        isCompleted = true;
        isEnd = true;


        isQuestIng = false;


        descriptionPanel.SetActive(false);
        //1퀘스트꺼 패널 가려버려


    } // 보상 받기 버튼

    public void QuestRewading()
    {
        //버튼에다 달아서 완료로 수령받기
    }


    public void QuestCheck()
    {
        if(qPopup.curCount >= qPopup.maxCount)
        {
            completedBtn.SetActive(true);
        }
    }

  }

