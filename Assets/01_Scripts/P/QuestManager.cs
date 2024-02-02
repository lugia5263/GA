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
    public GameObject jsonObject; //�Ƚᵵ ��
    public QuestPopUpManager qPopup;
    public RewardMgr rewardMgr;
    public InventoryManager inventoryMgr;

    // ����Ʈ �˾��� �̱������� �ٸ� ���� ���ٰ� �´�. �׷��Ƿ� ����Ʈ NPC�� ��ȣ�ۿ��� �� �� 
    // ���� �˾��� �ִ� ī��Ʈ�� ����Ʈ �Ŵ��� ��ũ��Ʈ�� �ִ� ����, �ִ� ī��Ʈ ���� ��������Ѵ�
    // �׸��� ��ȣ�ۿ��Ҷ� ����Ʈ�� �Ϸ�Ǿ��� �� bool���� True���� Ȯ���ؼ�
    // ����Ʈ�Ϸ� ��ư�� SetActive = true �� �Ѵ�.
    // npc Ŀ���ϰ� �Ұ�. 

    public GameObject questCanvas;
    public Text questNameTxt;
    public Text goalNameTxt;
    public Image questRewards;
    public GameObject descriptionPanel;

    public int acceptIdx;

    [Header("����Ʈ�˾�")]
    public GameObject questPopUpPanel;
    public Text questGoalTxt;
    public int questCurCount;
    //public int questMaxCount;

    [Header("����Ʈ ������ ǥ��")]
    public Text rewardExp;
    public Text rewardMat;
    public Text rewardGold;

    [Header("����Ʈ ������ư")]
<<<<<<< HEAD
=======
    public QuestPopUpManager QuestPopUpManager;
>>>>>>> DEV

    public GameObject acceptBtn;
    public GameObject ingBtn;
    public GameObject completedBtn;
<<<<<<< HEAD
    public GameObject questEndBtn;

    public bool isQuestIng;
    public bool isCompleted;
    public bool isEnd;
=======
>>>>>>> DEV

    //Player enterPlayer;


    public void CurQuestCheck()
    {
        if (questCurCount == 0)
        {
            return;
        }
        else
        {
            if (acceptIdx == qPopup.curQuestIndex)
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
<<<<<<< HEAD
        rewardMgr = GameObject.Find("RewardMgr").GetComponent<RewardMgr>();
        inventoryMgr = GameObject.Find("InventoryMgr").GetComponent<InventoryManager>();
        ingBtn = GameObject.Find("QuestIngBtn");
        completedBtn = GameObject.Find("CompletedBtn");
        questEndBtn = GameObject.Find("QuestEndBtn");
=======

        ingBtn = GameObject.Find("QuestIngBtn");

>>>>>>> DEV


    }
    private void Start()
    {
<<<<<<< HEAD
        ingBtn.SetActive(false);
        completedBtn.SetActive(false);
        questEndBtn.SetActive(false);
=======
        //ingBtn.SetActive(false);
        completedBtn.SetActive(false);
>>>>>>> DEV

        descriptionPanel.SetActive(false);
    }




    public void InstQuest(int n)
    {
        string json = txtFile.text;
        var jsonData = JSON.Parse(json); //var�� �ǹ�: Unity���� ������ �ٰ����´�.

        int item = n - 1; //�Ű�����

        //GameObject character = Instantiate(jsonObject);

        if (n >= acceptIdx || acceptIdx != 0)
        {
            questNameTxt.text = (jsonData["Quest"][item]["QuestName"]);
            goalNameTxt.text = (jsonData["Quest"][item]["Goal"]);
            rewardExp.text = (jsonData["Quest"][item]["Reward1"]);
            rewardMat.text = (jsonData["Quest"][item]["Reward2"]);
            rewardGold.text = (jsonData["Quest"][item]["Reward3"]);
            questCurCount = n;
            acceptIdx = n;
        }



<<<<<<< HEAD
=======
        questNameTxt.text = (jsonData["Quest"][item]["QuestName"]);
        goalNameTxt.text = (jsonData["Quest"][item]["Goal"]);
        rewardExp.text = (jsonData["Quest"][item]["Reward1"]);
        rewardMat.text = (jsonData["Quest"][item]["Reward2"]);
        rewardGold.text = (jsonData["Quest"][item]["Reward3"]);
        acceptIdx = n;
>>>>>>> DEV

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
        ReceiveQuest(acceptIdx);
<<<<<<< HEAD
=======


    }
    public void ReceiveQuest(int n)
    {
>>>>>>> DEV


    }
    public void ReceiveQuest(int n) // ������ư ������ ��
    {
        n = acceptIdx;
        string json = txtFile.text;
        var jsonData = JSON.Parse(json); //var�� �ǹ�: Unity���� ������ �ٰ����´�.
        int item = n - 1;
<<<<<<< HEAD

        qPopup.curCount = 0;

        ingBtn.SetActive(false);
        completedBtn.SetActive(false);
        questEndBtn.SetActive(false);

        qPopup.maxCount = (int)(jsonData["Quest"][item]["Count"]);
        qPopup.questCountTxt.text = $"({qPopup.curCount} / {(jsonData["Quest"][item]["Count"])})";
        qPopup.curQuestIndex = (int)(jsonData["Quest"][item]["QuestNum"]);
        isCompleted = false;

=======
>>>>>>> DEV

        questGoalTxt.text = (jsonData["Quest"][item]["Goal"]);
        //qPopup.questCountTxt.text = $"({questCurCount} / {(jsonData["Quest"][item]["Count"])})";

        qPopup.curQuestIndex = (int)(jsonData["Quest"][item]["QuestNum"]);
        //rewardExp.text = (jsonData["Quest"][item]["Reward1"]);
        //rewardMat.text = (jsonData["Quest"][item]["Reward2"]);
        //rewardGold.text = (jsonData["Quest"][item]["Reward3"]);

<<<<<<< HEAD
        isQuestIng = true;
        qPopup.InitCurQuest();
        questPopUpPanel.SetActive(true);
=======
        acceptBtn.SetActive(false);
>>>>>>> DEV
        ingBtn.SetActive(true);

    }

<<<<<<< HEAD
    public void QuestCompleted() // �Ϸ�
    {
        qPopup.InitCurQuest();

        //########���� �˾�#####################################
        //���ɹ��� �� �ִ� �˾� on�� ���� json�� �ִ� �����Լ��� ACCEPTiDX �Ű�������
        rewardMgr.Reward100exp3EABtn();

    }

    public void QuestSendBtn()
    {

        string json = txtFile.text;
        var jsonData = JSON.Parse(json); //var�� �ǹ�: Unity���� ������ �ٰ����´�.
        //�Ϸ� ��ư�� �̰� �ֱ� (������ �ڵ�)
        inventoryMgr.SendInventory();

        //������ �ֱ�

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
        //1����Ʈ�� �г� ��������


    } // ���� �ޱ� ��ư

    public void QuestRewading()
    {
        //��ư���� �޾Ƽ� �Ϸ�� ���ɹޱ�
    }


    public void QuestCheck()
    {
        if (qPopup.curCount >= qPopup.maxCount)
        {
            completedBtn.SetActive(true);
        }
    }

=======
    public void CompletedBtn()
    {
        GetComponent<QuestPopUpManager>().InitCurQuest();
        //if (curCount >= maxCount) { 
        //}
        transform.Find("IngBtn").gameObject.SetActive(false);
        transform.Find("CompletedBtn").gameObject.SetActive(true);
    }

    //public void CompleteButton()
    //{

    //}
>>>>>>> DEV
}

