using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestPopUpManager : MonoBehaviour
{
    public DataMgrDontDestroy dataMgrDonDestroy;
    public Text questCountTxt;
    public Text goalTxt;
    public GameObject questPopUpPanel;

    public string goalInfo;
    public int questIdx;
    public int curCnt;
    public int maxCnt;

    public void UpdateQuestStatus()
    {
        questPopUpPanel.SetActive(true);
        goalInfo = dataMgrDonDestroy.GoalTxt;
        questIdx = dataMgrDonDestroy.QuestIdx;
        curCnt = dataMgrDonDestroy.QuestCurCnt;
        maxCnt = dataMgrDonDestroy.QuestMaxCnt;

        if (dataMgrDonDestroy.IsDoing == true && dataMgrDonDestroy.IsCompleted == false)
        {
            goalTxt.text = goalInfo;
            questCountTxt.text = $"({curCnt} / {maxCnt})";
            questCountTxt.color = Color.white;
        }
        else if (dataMgrDonDestroy.IsDoing == true && dataMgrDonDestroy.IsCompleted == true)
        {
            goalTxt.text = goalInfo;
            questCountTxt.text = $"({curCnt} / {maxCnt})";
            questCountTxt.color = Color.yellow;
        }
        else
        {
            questPopUpPanel.SetActive(false);
        }
    }
    private void Start()
    {
        dataMgrDonDestroy = DataMgrDontDestroy.Instance;
        goalInfo = dataMgrDonDestroy.GoalTxt;
        questIdx = dataMgrDonDestroy.QuestIdx;
        curCnt = dataMgrDonDestroy.QuestCurCnt;
        maxCnt = dataMgrDonDestroy.QuestMaxCnt;
    }
}