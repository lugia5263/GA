using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using SimpleJSON;

//###### Quest List, Quest Description UI ��� ######

public class QuestManager : MonoBehaviour
{
    public DataMgrDontDestroy dataMgrDontDestroy;
    public UIMgr uIMgr;
    public TextAsset txtFile; //Jsonfile
    public QuestPopUpManager questPopUpManager;

    // ����Ʈ �˾��� �̱������� �ٸ� ���� ���ٰ� �´�. �׷��Ƿ� ����Ʈ NPC�� ��ȣ�ۿ��� �� �� 
    // ���� �˾��� �ִ� ī��Ʈ�� ����Ʈ �Ŵ��� ��ũ��Ʈ�� �ִ� ����, �ִ� ī��Ʈ ���� ��������Ѵ�
    // �׸��� ��ȣ�ۿ��Ҷ� ����Ʈ�� �Ϸ�Ǿ��� �� bool���� True���� Ȯ���ؼ�
    // ����Ʈ�Ϸ� ��ư�� SetActive = true �� �Ѵ�.
    // npc Ŀ���ϰ� �Ұ�. 

    public GameObject questCanvas;
    public GameObject descriptionPanel;

    public Text questNameTxt;
    public Text goalNameTxt;
    public Image questRewards;

    [Header("����Ʈ�˾�")]
    public GameObject questPopUpPanel;
    public Text questGoalTxt;
    public int questCurCnt;
    public int questMaxCnt;
    public int questIdx;

    [Header("����Ʈ ������ ǥ��")]
    public Text rewardExp;
    public Text rewardMat;
    public Text rewardGold;

    [Header("����Ʈ ������ư")]
    public QuestPopUpManager QuestPopUpManager;

    public GameObject acceptBtn;
    public GameObject ingBtn;
    public GameObject completedBtn;

    //Player enterPlayer;

    public void Enter(Player player)
    {
        //enterPlayer = player;
        //uiGroup.anchoredPosition = Vector3.zero;
    }
    private void Awake()
    {
        Debug.Log("Start: Trying to find Buttons");

        questNameTxt = GameObject.Find("questNameTxt").GetComponent<Text>();
        goalNameTxt = GameObject.Find("goalNameTxt").GetComponent<Text>();
        questRewards = GameObject.Find("QuestRewards").GetComponent<Image>();
        questPopUpPanel = GameObject.Find("QuestPanel");
        questGoalTxt = GameObject.Find("GoalTxt").GetComponent<Text>();
        questPopUpManager = GameObject.Find("QuestPopUp").GetComponent<QuestPopUpManager>();

        ingBtn = GameObject.Find("QuestIngBtn");



    }
    private void Start()
    {
        //ingBtn.SetActive(false);
        completedBtn.SetActive(false);

        descriptionPanel.SetActive(false);

        questIdx = dataMgrDontDestroy.questIdx;
    }

    public void InstQuest(int n)
    {
        string json = txtFile.text;
        var jsonData = JSON.Parse(json); //var�� �ǹ�: Unity���� ������ �ٰ����´�.

        int item = n - 1; //�Ű�����

        questNameTxt.text = (jsonData["Quest"][item]["QuestName"]);
        goalNameTxt.text = (jsonData["Quest"][item]["Goal"]);
        rewardExp.text = (jsonData["Quest"][item]["Reward1"]);
        rewardMat.text = (jsonData["Quest"][item]["Reward2"]);
        rewardGold.text = (jsonData["Quest"][item]["Reward3"]);
        questIdx = n;

        #region
        //character.transform.name = (jsonData["��Ʈ1"][n]["QuestName"]);
        //character.GetComponent<QuestData>().charname = (jsonData["��Ʈ1"][n]["QuestName"]);
        //character.GetComponent<QuestData>().atk = (int)(jsonData["��Ʈ1"][n]["Count"]);
        ////character.GetComponent<QuestData>().count++; //QuestData�� ī��Ʈ ����

        //character.tag = "Player"; //prefab�� �±׸� �ްž�.

        //character.transform.SetParent(questCanvas.transform); //���� questCanvas�� �θ�� �ΰ� �����ϰ� Prefab�� �¾.
        #endregion
    }

    public void AcceptBtn()
    {
        ReceiveQuest(questIdx);
        uIMgr.UpdateQuestPopUpInfo(questPopUpManager.questGoalTxt.text, questPopUpManager.questCountTxt.text);
    }

    public void ReceiveQuest(int n)
    {

        string json = txtFile.text;
        var jsonData = JSON.Parse(json);
        int item = n - 1;

        questGoalTxt.text = (jsonData["Quest"][item]["Goal"]);
        questPopUpManager.questMaxCnt = (int)(jsonData["Quest"][item]["Count"]);
        questPopUpManager.questCountTxt.text = $"({questPopUpManager.questCurCnt} / {questPopUpManager.questMaxCnt})";
        questPopUpManager.questIdx = (int)(jsonData["Quest"][item]["QuestNum"]);
        dataMgrDontDestroy.curquestidx = n;

        acceptBtn.SetActive(false);
        ingBtn.SetActive(true);
    }

    public void CompletedBtn()
    {
        GetComponent<QuestPopUpManager>().InitCurQuest();
        if (questCurCnt >= questMaxCnt)
        {
<<<<<<< HEAD

=======
            
>>>>>>> upstream/DEV
        }
        transform.Find("IngBtn").gameObject.SetActive(false);
        transform.Find("CompletedBtn").gameObject.SetActive(true);
    }

    //public void CompleteButton()
    //{

    //}
}