using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using UnityEngine.SceneManagement;
using System;

public class DungeonMgr : MonoBehaviour
{
    public Text discription;

    public int dungeonSort; // ���� ���� ����(�̱�1, ī����2, ���̵�3)
    public int dungeonNum;

    public string single = "Single";
    public string chaos = "Chaos";
    public string raid = "Raid";

    public GameObject singlePanel;
    public GameObject chaosPanel;
    public GameObject raidPanel;

    public GameObject singleContent;
    public GameObject chaosContent;
    public GameObject raidContent;

    public TextAsset txtFile; //Jsonfile
    public GameObject singleCell; // Prefab
    public GameObject chaosCell;
    public GameObject raidCell;


    private void Awake()
    {
        singleContent = GameObject.Find("SingleContent");
        chaosContent = GameObject.Find("ChaosContent");
        raidContent = GameObject.Find("RaidContent");

        singlePanel = GameObject.Find("SinglePanel");
        chaosPanel = GameObject.Find("ChaosPanel");
        raidPanel = GameObject.Find("RaidPanel");

        discription = GameObject.Find("DungeonDescription").GetComponent<Text>();

        var jsonitemFIle = Resources.Load<TextAsset>("Json/DungeonList");
        txtFile = jsonitemFIle;
    }

    private void Start()
    {
        BackDungeon();

        var DunGeonListFile = Resources.Load<TextAsset>("Json/DungeonList");
        txtFile = DunGeonListFile;


        string json = txtFile.text;
        var jsonData = JSON.Parse(json);

        for (int i = 1; i < jsonData["Single"].Count + 1; i++)
        {
            InstSingleDunGeon(i);
        }
        for (int i = 1; i < jsonData["Chaos"].Count + 1; i++)
        {
            InstChaosDunGeon(i); 
        }
        for (int i = 1; i < jsonData["Raid"].Count + 1; i++)
        {
            InstRaidDunGeon(i);
        }
    }

    public void InstSingleDunGeon(int n)
    {
        string json = txtFile.text;
        var jsonData = JSON.Parse(json);

        int idx = n - 1; // �Ű�����

        GameObject character = Instantiate(singleCell); // ����ž�

        character.transform.name = jsonData["Single"][idx]["dungeonName"]; // ������Ʈ�� ����

        character.GetComponent<CellsInfo_Single>().dungeonIdxNum = (int)(jsonData["Single"][idx]["num"]);
        character.GetComponent<CellsInfo_Single>().dungeonName = (jsonData["Single"][idx]["dungeonName"]);
        character.GetComponent<CellsInfo_Single>().discription = (jsonData["Single"][idx]["Discription"]);
        character.GetComponent<CellsInfo_Single>().difficult = (jsonData["Single"][idx]["difficulty"]);

        character.transform.SetParent(singleContent.transform);
    }

    public void InstChaosDunGeon(int n)
    {
        string json = txtFile.text;
        var jsonData = JSON.Parse(json);

        int idx = n - 1; // �Ű�����

        GameObject character = Instantiate(chaosCell);

        character.transform.name = jsonData["Chaos"][idx]["dungeonName"];

        character.GetComponent<CellsInfo_Chaos>().dungeonIdxNum = (int)(jsonData["Chaos"][idx]["num"]);
        character.GetComponent<CellsInfo_Chaos>().dungeonName = (jsonData["Chaos"][idx]["dungeonName"]);
        character.GetComponent<CellsInfo_Chaos>().discription = jsonData["Chaos"][idx]["Discription"];
        character.GetComponent<CellsInfo_Chaos>().difficult = (jsonData["Chaos"][idx]["difficulty"]);

        character.transform.SetParent(chaosContent.transform);
    }
    public void InstRaidDunGeon(int n)
    {
        string json = txtFile.text;
        var jsonData = JSON.Parse(json);

        int idx = n - 1; // �Ű�����

        GameObject character = Instantiate(raidCell);

        character.transform.name = jsonData["Raid"][idx]["dungeonName"];

        character.GetComponent<CellsInfo_Raid>().dungeonIdxNum = (int)(jsonData["Raid"][idx]["num"]);
        character.GetComponent<CellsInfo_Raid>().dungeonName = (jsonData["Raid"][idx]["dungeonName"]);
        character.GetComponent<CellsInfo_Raid>().discription = jsonData["Raid"][idx]["Discription"];
        character.GetComponent<CellsInfo_Raid>().difficult = (jsonData["Raid"][idx]["difficulty"]);

        character.transform.SetParent(raidContent.transform);
    }




    public void OnSinglePanel()
    {
        singlePanel.SetActive(true);
        chaosPanel.SetActive(false);
        raidPanel.SetActive(false);
        dungeonSort = 1;
        string single = "ȥ�ڿͽ�? �� �̱��̾�.";
        discription.text = single;
    }

    public void OnChaosPanel()
    {
        singlePanel.SetActive(false);
        chaosPanel.SetActive(true);
        raidPanel.SetActive(false);
        dungeonSort = 2;
        string chaos = "ī���� ������ �����ϼ���.";
        discription.text = chaos;
    }

    public void OnRaidPanel()
    {
        singlePanel.SetActive(false);
        chaosPanel.SetActive(false);
        raidPanel.SetActive(true);
        dungeonSort = 3;
        string raid = "���̵� ������ �����ϼ���.";
        discription.text = raid;
    }

    public void BackDungeon()
    {
        singlePanel.SetActive(false);
        chaosPanel.SetActive(false);
        raidPanel.SetActive(false);
        string main = "������ ������.";
        discription.text = main;
    }
    public void MoveDungeon()
    {
        if (dungeonSort == 1)
            SceneManager.LoadScene(single);
        else if (dungeonSort == 2)
            SceneManager.LoadScene(chaos);
        else if (dungeonSort == 3)
            SceneManager.LoadScene(raid);
    }

}