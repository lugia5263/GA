using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
public class QuestManager : MonoBehaviour
{
    public TextAsset txtFile; //Jsonfile
    public GameObject jsonObject; //안써도 됨

    public GameObject questCanvas;
    public Text questNameTxt;
    public Text goalNameTxt;
    public Text countTxt;
    public GameObject descriptionPanel;

    private void Awake()
    {
        questNameTxt = GameObject.Find("questNameTxt").GetComponent<Text>();
        goalNameTxt = GameObject.Find("goalNameTxt").GetComponent<Text>();
        countTxt = GameObject.Find("countTxt").GetComponent<Text>();
        descriptionPanel.SetActive(false);
    }
    void Start()
    {


    }


    public void InstQuest(int n)
    {
        string json = txtFile.text;
        var jsonData = JSON.Parse(json); //var의 의미: Unity외의 파일을 다가져온다.

        int item = n; //매개변수

        //GameObject character = Instantiate(jsonObject);


        questNameTxt.text = (jsonData["시트1"][n]["QuestName"]);
        goalNameTxt.text = (jsonData["시트1"][n]["Goal"]);
        countTxt.text = (jsonData["시트1"][n]["Count"]);


        #region
        //character.transform.name = (jsonData["시트1"][n]["QuestName"]);
        //character.GetComponent<QuestData>().charname = (jsonData["시트1"][n]["QuestName"]);
        //character.GetComponent<QuestData>().atk = (int)(jsonData["시트1"][n]["Count"]);
        ////character.GetComponent<QuestData>().count++; //QuestData의 카운트 증가

        //character.tag = "Player"; //prefab에 태그를 달거야.

        //character.transform.SetParent(questCanvas.transform); //나는 questCanvas를 부모로 두고 응애하고 Prefab이 태어남.
        #endregion
    }


    void Update()
    {
        
    }
}
