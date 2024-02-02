using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestPopUpManager : MonoBehaviour
{
    public DataMgrDontDestroy dataMgr;
    
    public int questIndex; //���� ����Ʈ ��ȣ
    public Text questCountTxt;

    public string goal;
    public int curCnt;
    public int maxCnt;

    public int questMaxCnt { get; set; }
    public int questCurCnt { get; set; }
    public int questIdx { get; set; }

    public bool isCompleted;

    private void Start()
    {
        dataMgr = DataMgrDontDestroy.instance;

        questCountTxt = GameObject.Find("QCountTxt").GetComponent<Text>();
        questIndex = dataMgr.QuestIdx;
        goal = dataMgr.GoalTxt;
        curCnt = dataMgr.QuestCurCnt;
        maxCnt = dataMgr.QuestMaxCnt;
    }


    //public enum QuestCondition
    //{
    //    Talk = 1,
    //    NormalMonKill,
    //    BossKill,
    //    GotGold,
    //    Buy
    //}

    //private QuestCondition qcondition;

    public void QuestIndexUp(int n) //����Ʈ �޼��� ����
    {
        //qcondition = (QuestCondition)n; // ���� �̳Ѱ��� 1, 2 �� ���ž�
        if (questIndex == n)
        { 
            curCnt++;
            dataMgr.QuestCurCnt = curCnt; // ������ �Ŵ����� ���� ����Ʈ �޼��� ������Ʈ
        }

        InitCurQuest();
    }

    public void InitCurQuest() //�۾� �ʱ�ȭ
    {
        questCountTxt.text = $"({curCnt} /  {maxCnt} )";

        if(curCnt >= maxCnt)
        {
            questCountTxt.color = Color.yellow;
            isCompleted = true;
        }
        else
        {
            questCountTxt.color = Color.white;
        }
    }


}



