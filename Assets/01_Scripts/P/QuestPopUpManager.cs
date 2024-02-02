using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestPopUpManager : MonoBehaviour
{
    
    public int curQuestIndex; //���� ����Ʈ ��ȣ

    public Text questCountTxt;

    public string goal;
    public int curCount;
    public int maxCount;

    public bool isCompleted;

    private void Start()
    {
        questCountTxt = GameObject.Find("QCountTxt").GetComponent<Text>();
    }


    public enum QuestCondition
    {
        Talk = 1,
        NormalMonKill,
        BossKill,
        GotGold,
        Buy
    }

    private QuestCondition qcondition;

    public void QuestIndexUp(int n) //����Ʈ �޼��� ����
    {
        qcondition = (QuestCondition)n; // ���� �̳Ѱ��� 1, 2 �� ���ž�
        if (curQuestIndex == n)
        { curCount++;}

        InitCurQuest();
    }

    public void InitCurQuest() //�۾� �ʱ�ȭ
    {
        questCountTxt.text = $"({curCount} /  {maxCount} )";

        if(curCount >= maxCount)
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



