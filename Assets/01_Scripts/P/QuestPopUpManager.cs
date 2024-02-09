using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestPopUpManager : MonoBehaviour
{
    public DataMgrDontDestroy dataMgr;
    public int questIndex; //���� ����Ʈ ��ȣ
    public Text questCountTxt;
    public Text goalTxt;

    public string goalInfo;
    public int curCnt;
    public int maxCnt;

    public int questMaxCnt { get; set; }
    public int questCurCnt { get; set; }
    public int questIdx { get; set; }

    public bool isCompleted;

    public void UpdateQuestStatus(string goals, int curCnts, int maxCnts)
    {
        goalInfo = goals;
        curCnt = curCnts;
        maxCnt = maxCnts;

        goalTxt.text = goalInfo;
        questCurCnt = curCnt;
        questMaxCnt = maxCnt;
        questCountTxt.text = $"({curCnt} / {maxCnt})";
    }
    private void Start()
    {
        dataMgr = DataMgrDontDestroy.Instance;

        questCountTxt = GameObject.Find("QCountTxt").GetComponent<Text>();
        questIndex = dataMgr.questIdx;
        goalInfo = dataMgr.GoalTxt;
        curCnt = dataMgr.QuestCurCnt;
        maxCnt = dataMgr.QuestMaxCnt;
    }

    //private QuestCondition qcondition;

    public void QuestIndexUp(int n) //����Ʈ �޼��� ����
    {
        //qcondition = (QuestCondition)n; // ���� �̳Ѱ��� 1, 2 �� ���ž�
        if (dataMgr.questIdx == n)
        {
            curCnt++;
            dataMgr.QuestCurCnt = curCnt; // ������ �Ŵ����� ���� ����Ʈ �޼��� ������Ʈ
        }
        InitCurQuest();
    }

    public void InitCurQuest() //�۾� �ʱ�ȭ
    {
        questCountTxt.text = $"({curCnt} /  {maxCnt} )";

        if (curCnt >= maxCnt)
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