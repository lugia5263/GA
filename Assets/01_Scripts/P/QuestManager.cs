using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using SimpleJSON;
public class QuestManager : MonoBehaviour
{
    public TextAsset txtFile; //Jsonfile
    public GameObject jsonObject; //안써도 됨

    public GameObject questCanvas;
    public Text questNameTxt;
    public Text goalNameTxt;
    public Text countTxt;
    public Image questRewards;
    public GameObject descriptionPanel;

    public int acceptIdx;

    [Header("퀘스트팝업")]
    public GameObject questPopUpPanel;
    public Text questGoalTxt;
    public Text questCountTxt;
    public int questCurCount;
    //public int questMaxCount;

    //[Header("퀘스트 보상목록 표시")]
    //public Image 


    //Player enterPlayer;

    public void Enter(Player player)
    {
        //enterPlayer = player;
        //uiGroup.anchoredPosition = Vector3.zero;
    }
    private void Awake()
    {
        questNameTxt = GameObject.Find("questNameTxt").GetComponent<Text>();
        goalNameTxt = GameObject.Find("goalNameTxt").GetComponent<Text>();
        countTxt = GameObject.Find("countTxt").GetComponent<Text>();
        questRewards = GameObject.Find("QuestRewards").GetComponent<Image>();
        questPopUpPanel = GameObject.Find("QuestPanel");
        questGoalTxt = GameObject.Find("GoalTxt").GetComponent<Text>();
        questCountTxt = GameObject.Find("CountTxt").GetComponent<Text>();
        descriptionPanel.SetActive(false);
    }
    void Start()
    {


    }


    public void InstQuest(int n)
    {
        string json = txtFile.text;
        var jsonData = JSON.Parse(json); //var의 의미: Unity외의 파일을 다가져온다.

        int item = n-1; //매개변수

        //GameObject character = Instantiate(jsonObject);


        questNameTxt.text = (jsonData["Quest"][item]["QuestName"]);
        goalNameTxt.text = (jsonData["Quest"][item]["Goal"]);
        countTxt.text = (jsonData["Quest"][item]["Count"]);
        acceptIdx = n;

        #region
        //character.transform.name = (jsonData["시트1"][n]["QuestName"]);
        //character.GetComponent<QuestData>().charname = (jsonData["시트1"][n]["QuestName"]);
        //character.GetComponent<QuestData>().atk = (int)(jsonData["시트1"][n]["Count"]);
        ////character.GetComponent<QuestData>().count++; //QuestData의 카운트 증가

        //character.tag = "Player"; //prefab에 태그를 달거야.

        //character.transform.SetParent(questCanvas.transform); //나는 questCanvas를 부모로 두고 응애하고 Prefab이 태어남.
        #endregion
    }

    public void AcceptQuestBtn()
    {
        ReceiveQuest(acceptIdx);
    }
    public void ReceiveQuest(int n)
    {
        Debug.Log("Tlqkf");
        string json = txtFile.text;
        var jsonData = JSON.Parse(json); //var의 의미: Unity외의 파일을 다가져온다.
        int item = n-1;

        questGoalTxt.text = (jsonData["Quest"][item]["Goal"]);
        questCountTxt.text = $"({questCurCount} / {(jsonData["Quest"][item]["Count"])})";
    }
}
