using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using SimpleJSON;
using Photon.Pun;

//###### Quest List, Quest Description UI 담당 ######

public class QuestManager : MonoBehaviour
{
    [Header("DonDestroy")]
    public DataMgrDontDestroy dataMgrDontDestroy;
    public TextAsset txtFile; //Jsonfile
    public int questIdx;
    // 퀘스트 팝업은 싱글톤으로 다른 씬을 갔다가 온다. 그러므로 퀘스트 NPC에 상호작용을 할 때 
    // 현재 팝업에 있는 카운트를 퀘스트 매니저 스크립트에 있는 현재, 최대 카운트 값을 보내줘야한다
    // 그리고 상호작용할때 퀘스트가 완료되었을 때 bool값이 True인지 확인해서
    // 퀘스트완료 버튼을 SetActive = true 로 한다.
    // npc 커밋하고 할것. 
    [Header("Component")]
    public QuestPopUpManager questPopUpManager;

    [Header("퀘스트 선택창")]
    public GameObject questPanel;

    [Header("퀘스트 설명창")]
    public GameObject descriptionPanel;
    public Text questNameTxt;
    public Text goalTxt;

    [Header("NPC 대화")]
    public DialogueTrigger dialogueTrigger; //대본
    public GameObject nextBtn; //대본 진행
    public GameObject nPCConversation;

    public Text textName;
    public Text textSentence;
    public GameObject conversationPanel;

    Queue<string> naming = new Queue<string>();
    Queue<string> sentence = new Queue<string>();

    public Text rewardExp;
    public Text rewardMat;
    public Text rewardGold;

    [Header("퀘스트 진행버튼")]
    public GameObject acceptBtn;
    public GameObject ingImg;
    public GameObject completedBtn;

    [Header("퀘스트 현재 진행도 창")]
    public GameObject questPopUpPanel;
    //public bool questPopUpPanelVisible;
    public Text questDescriptionGoalTxt; //이 텍스트에 questGoalTxt 의 문자가 들어감
    public string questGoalTxt; 
    public int questCurCnt;
    public int questMaxCnt;

    private void Awake()
    {
        dataMgrDontDestroy = DataMgrDontDestroy.Instance;
    }
    private void Start()
    {
        //ingBtn.SetActive(false);
        completedBtn.SetActive(false);
        ingImg.SetActive(false);
        descriptionPanel.SetActive(false);
        questPopUpPanel.SetActive(false);
        questIdx = dataMgrDontDestroy.QuestIdx;
        questGoalTxt = dataMgrDontDestroy.GoalTxt;
        questCurCnt = dataMgrDontDestroy.QuestCurCnt;
        questMaxCnt = dataMgrDontDestroy.QuestMaxCnt;
        nPCConversation.SetActive(false);
    }

    public void Update()
    {
        if(Input.GetMouseButtonDown(2))
        {
            Debug.Log("마우스 휠클릭 정상동작");
            nextBtn.GetComponent<TalkMgr>();
            Debug.Log("### TalkMgr 정상동작 ###");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PhotonView>().IsMine)
            {
                Debug.Log("충돌일어남");
                questPanel.SetActive(true);
                nPCConversation.SetActive(true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PhotonView>().IsMine)
            {
                // 강화창 껐으니까 플레이어의 정보에 반영
                questPanel.SetActive(false);
            }
        }
    }

    public void InstQuest(int n)
    {
        string json = txtFile.text;
        var jsonData = JSON.Parse(json); //var의 의미: Unity외의 파일을 다가져온다.

        int item = n - 1; //매개변수

        questIdx = n;
        questNameTxt.text = (jsonData["Quest"][item]["QuestName"]);
        goalTxt.text = (jsonData["Quest"][item]["Goal"]);
        rewardExp.text = (jsonData["Quest"][item]["Reward1"]);
        rewardMat.text = (jsonData["Quest"][item]["Reward2"]);
        rewardGold.text = (jsonData["Quest"][item]["Reward3"]);
        
        
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
        ReceiveQuest(questIdx);
        //uIMgr.UpdateQuestPopUpInfo(questPopUpManager.questGoalTxt.text, questPopUpManager.questCountTxt.text);
    }

    public void ReceiveQuest(int n)
    {

        string json = txtFile.text;
        var jsonData = JSON.Parse(json);
        int item = n - 1;

        goalTxt.text = (jsonData["Quest"][item]["Goal"]);
        questPopUpManager.questMaxCnt = (int)(jsonData["Quest"][item]["Count"]);
        questPopUpManager.questCountTxt.text = $"({questPopUpManager.questCurCnt} / {questPopUpManager.questMaxCnt})";
        questPopUpManager.questIdx = (int)(jsonData["Quest"][item]["QuestNum"]);
        dataMgrDontDestroy.questIdx = n;

        //questPopUpPanelVisible
        acceptBtn.SetActive(false);
        questPopUpPanel.SetActive(true);
        ingImg.SetActive(true);
    }

    public void CompletedBtn()
    {
        GetComponent<QuestPopUpManager>().InitCurQuest();
        if (questCurCnt >= questMaxCnt)
        {
            questIdx++;
            dataMgrDontDestroy.QuestIdx = questIdx;
        }
        transform.Find("IngImg").gameObject.SetActive(false);
        transform.Find("CompletedBtn").gameObject.SetActive(true);
    }

    //public void CompleteButton()
    //{

    //}
}