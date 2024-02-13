using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using SimpleJSON;
using Photon.Pun;

//###### Quest List, Quest Description UI ��� ######

public class QuestManager : MonoBehaviour
{
    [Header("DonDestroy")]
    public DataMgrDontDestroy dataMgrDontDestroy;

    public TextAsset txtFile; //Jsonfile
    // ����Ʈ �˾��� �̱������� �ٸ� ���� ���ٰ� �´�. �׷��Ƿ� ����Ʈ NPC�� ��ȣ�ۿ��� �� �� 
    // ���� �˾��� �ִ� ī��Ʈ�� ����Ʈ �Ŵ��� ��ũ��Ʈ�� �ִ� ����, �ִ� ī��Ʈ ���� ��������Ѵ�
    // �׸��� ��ȣ�ۿ��Ҷ� ����Ʈ�� �Ϸ�Ǿ��� �� bool���� True���� Ȯ���ؼ�
    // ����Ʈ�Ϸ� ��ư�� SetActive = true �� �Ѵ�.
    // npc Ŀ���ϰ� �Ұ�. 
    [Header("Component")]
    public QuestPopUpManager questPopUpManager;

    [Header("����Ʈ ����â")]
    public GameObject questPanel;
    public GameObject quest_1;
    public GameObject quest_2;
    public GameObject quest_3;
    public GameObject quest_4;

    [Header("����Ʈ ����â")]
    public GameObject descriptionPanel;
    public Text questNameTxt;
    public Text goalTxt;

    [Header("NPC ��ȭ")]
    public DialogueTrigger dialogueTrigger; //�뺻
    public GameObject nextBtn; //�뺻 ����
    public GameObject nPCConversation;

    public Text textName;
    public Text textSentence;

    Queue<string> naming = new Queue<string>();
    Queue<string> sentence = new Queue<string>();

    [Header("����Ʈ �����ư")]
    public GameObject clearfowardQuestImg;
    public GameObject acceptBtn;
    public GameObject ingImg;
    public GameObject completedBtn;
    public GameObject endBtn;


    [Header("����Ʈ ���� ���൵ â")]
    public GameObject questPopUpPanel;
    public Text questDescriptionGoalTxt; //�� �ؽ�Ʈ�� questGoalTxt �� ���ڰ� ��
    public string questGoalTxt;
    public int questCurCnt;
    public int questMaxCnt;


    [Header("����Ʈ ����")]
    public int expPotionReward;
    public int materialReward;
    public int goldReward;

    public Text rewardExp;
    public Text rewardMat;
    public Text rewardGold;

    private void Awake()
    {
        dataMgrDontDestroy = DataMgrDontDestroy.Instance;
        questPopUpManager = GameObject.Find("QuestPopUp").GetComponent<QuestPopUpManager>();
        descriptionPanel.SetActive(false);
    }
    private void Start()
    {
        completedBtn.SetActive(false);
        ingImg.SetActive(false);

        questPanel.SetActive(false);
        
        questPopUpManager.questIdx = dataMgrDontDestroy.QuestIdx;
        questGoalTxt = dataMgrDontDestroy.GoalTxt;
        questCurCnt = dataMgrDontDestroy.QuestCurCnt;
        questMaxCnt = dataMgrDontDestroy.QuestMaxCnt;
        //nPCConversation.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PhotonView>().IsMine)
            {
                Debug.Log("�浹�Ͼ");
                questPanel.SetActive(true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PhotonView>().IsMine)
            {
                nPCConversation.SetActive(false);
                descriptionPanel.SetActive(false);
                questPanel.SetActive(false);
            }
        }
    }

    public void InstQuest(int n)
    {
        string json = txtFile.text;
        var jsonData = JSON.Parse(json); //var�� �ǹ�: Unity���� ������ �ٰ����´�.

        int item = n - 1; //�Ű�����

        questNameTxt.text = (jsonData["Quest"][item]["QuestName"]);
        goalTxt.text = (jsonData["Quest"][item]["Goal"]);
        rewardExp.text = (jsonData["Quest"][item]["RewardExp"]);
        rewardMat.text = (jsonData["Quest"][item]["RewardMat"]);
        rewardGold.text = (jsonData["Quest"][item]["RewardGold"]);
        Debug.Log("Json ������ �ҷ���");
    }

    public void CompleteFirst(int n) //�����̳� �ƴϳ�, "���� �����ϰ� ������" ���
    {
        if (n == dataMgrDontDestroy.QuestIdx)
        {
            if (dataMgrDontDestroy.IsDoing == true)
            {
                if (dataMgrDontDestroy.IsCompleted == true)
                {
                    acceptBtn.SetActive(false);
                    clearfowardQuestImg.SetActive(false);
                    ingImg.SetActive(false);
                    completedBtn.SetActive(true);
                    endBtn.SetActive(false);
                }
                else
                {
                    acceptBtn.SetActive(false);
                    clearfowardQuestImg.SetActive(false);
                    ingImg.SetActive(true);
                    completedBtn.SetActive(false);
                    endBtn.SetActive(false);
                }
            }
            else
            {
                if (dataMgrDontDestroy.IsCompleted == true)
                {
                    acceptBtn.SetActive(false);
                    clearfowardQuestImg.SetActive(false);
                    ingImg.SetActive(false);
                    completedBtn.SetActive(true);
                    endBtn.SetActive(false);
                }
                else
                {
                    acceptBtn.SetActive(false);
                    clearfowardQuestImg.SetActive(false);
                    ingImg.SetActive(false);
                    completedBtn.SetActive(false);
                    endBtn.SetActive(true);
                }
            }
        }
        else if (n > dataMgrDontDestroy.QuestIdx)
        {
            if (n - dataMgrDontDestroy.QuestIdx == 1)
            {
                if(n==1 && dataMgrDontDestroy.QuestIdx == 0)
                {
                    acceptBtn.SetActive(true);
                    clearfowardQuestImg.SetActive(false);
                    ingImg.SetActive(false);
                    completedBtn.SetActive(false);
                    endBtn.SetActive(false);
                }
                else
                {
                    if (dataMgrDontDestroy.IsDoing == false)
                    {
                        acceptBtn.SetActive(true);
                        clearfowardQuestImg.SetActive(false);
                        ingImg.SetActive(false);
                        completedBtn.SetActive(false);
                        endBtn.SetActive(false);
                    }
                    else
                    {
                        acceptBtn.SetActive(false);
                        clearfowardQuestImg.SetActive(true);
                        ingImg.SetActive(false);
                        completedBtn.SetActive(false);
                        endBtn.SetActive(false);
                    }
                }
            }
            else
            {
                acceptBtn.SetActive(false);
                clearfowardQuestImg.SetActive(true);
                ingImg.SetActive(false);
                completedBtn.SetActive(false);
                endBtn.SetActive(false);
            }
        }
        else
        {
            acceptBtn.SetActive(false);
            clearfowardQuestImg.SetActive(false);
            ingImg.SetActive(false);
            completedBtn.SetActive(false);
            endBtn.SetActive(true);
        }
    }
    public void AcceptBtn()
    {
        dataMgrDontDestroy.QuestIdx++;
        dataMgrDontDestroy.QuestCurCnt = 0;
        dataMgrDontDestroy.IsDoing = true; // ���߿� �̱������� ������

        string json = txtFile.text;
        var jsonData = JSON.Parse(json);
        int item = dataMgrDontDestroy.QuestIdx - 1;

        if (jsonData["Quest"][item]["Goal"] != null)
        {
            dataMgrDontDestroy.GoalTxt = jsonData["Quest"][item]["Goal"];
        }
        else
        {
            // ���� ó�� �Ǵ� �⺻�� �Ҵ�
            dataMgrDontDestroy.GoalTxt = "Default Goal Text";
        }
        dataMgrDontDestroy.QuestMaxCnt = (int)(jsonData["Quest"][item]["Count"]);

        questPopUpManager.UpdateQuestStatus();

        acceptBtn.SetActive(false);
        ingImg.SetActive(true);
    }

    public void CompletedBtn()
    {
        QuestClearReward(dataMgrDontDestroy.QuestIdx);
        dataMgrDontDestroy.IsCompleted = false;
        dataMgrDontDestroy.IsDoing = false;
        questPopUpPanel.SetActive(false);
        endBtn.SetActive(true);
    }

    public void QuestClearReward(int n)
    {
        string json = txtFile.text;
        var jsonData = JSON.Parse(json);

        int item = n - 1; //�Ű�����

        expPotionReward = (jsonData["Quest"][item]["RewardExp"]);
        materialReward = (jsonData["Quest"][item]["RewardMat"]);
        goldReward = (jsonData["Quest"][item]["RewardGold"]);

        //�̶� ����Ʈ ���� ����
        dataMgrDontDestroy.UserExpPotion += expPotionReward;
        dataMgrDontDestroy.UserMaterial += materialReward;
        dataMgrDontDestroy.UserGold += goldReward;
    }
}