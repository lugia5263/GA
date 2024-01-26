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

<<<<<<< Updated upstream
    public int dungeonSort; // 현재 던전 형식(싱글1, 카오스2, 레이드3)
    public int dungeonNum;

    public string single = "Single";
    public string chaos = "Chaos";
    public string raid = "Raid";
=======
    public int dunGeonSort; // 현재 던전 형식(싱글1, 카오스2, 레이드3)
>>>>>>> Stashed changes

    public GameObject singlePanel;
    public GameObject chaosPanel;
    public GameObject raidPanel;

    public GameObject singleContent;
    public GameObject chaosContent;
    public GameObject raidContent;

<<<<<<< Updated upstream
    public TextAsset txtFile; //Jsonfile
    public GameObject singleCell; // Prefab
    public GameObject chaosCell;
    public GameObject raidCell;
=======

    private TextAsset txtFile; //Jsonfile
    public GameObject jsonObject;
    public GameObject jsonObject2;
    public GameObject jsonObject3;
>>>>>>> Stashed changes

    private void Awake()
    {
        singleContent = GameObject.Find("SingleContent");
        chaosContent = GameObject.Find("ChaosContent");
        raidContent = GameObject.Find("RaidContent");

        singlePanel = GameObject.Find("SinglePanel");
        chaosPanel = GameObject.Find("ChaosPanel");
        raidPanel = GameObject.Find("RaidPanel");

<<<<<<< Updated upstream
        discription = GameObject.Find("DungeonDescription").GetComponent<Text>();

        var jsonitemFIle = Resources.Load<TextAsset>("Json/DungeonList");
        txtFile = jsonitemFIle;
=======
        discription = GameObject.Find("DunGeonDescription").GetComponent<Text>();
    }

    private void Start()
    {
        singlePanel.SetActive(false);
        chaosPanel.SetActive(false);
        raidPanel.SetActive(false);

        var DunGeonListFile = Resources.Load<TextAsset>("Json/DungeonList");
        txtFile = DunGeonListFile;
>>>>>>> Stashed changes

        string json = txtFile.text;
        var jsonData = JSON.Parse(json);

<<<<<<< Updated upstream
        for (int i = 1; i < jsonData["Single"].Count + 1; i++)
=======
        for (int i = 0; i < jsonData["Single"].Count; i++)
>>>>>>> Stashed changes
        {
            InstSingleDunGeon(i);
        }
        for (int i = 0; i < jsonData["Chaos"].Count; i++)
        {
            InstChaosDunGeon(i);
        }
<<<<<<< Updated upstream
        for (int i = 1; i < jsonData["Raid"].Count + 1; i++)
        {
            InstRaidDunGeon(i);
        }
    }

    private void Start()
    {
        BackDungeon();
    }

    public void InstSingleDunGeon(int n)
=======
        for (int i = 0; i < jsonData["Raid"].Count; i++)
        {
            InstRaidDunGeon(i);
        }
    }


    public void InstSingleDunGeon(int n)
    {
        string json = txtFile.text;
        var jsonData = JSON.Parse(json);

        int idx = n; // 매개변수

        GameObject character = Instantiate(jsonObject); // 만들거야

        character.transform.name = jsonData["Single"][idx]["dungeonName"]; // 오브젝트명 정의

        character.GetComponent<DunGeonInfo>().dunGeonName = (jsonData["Single"][idx]["dungeonName"]);
        character.GetComponent<DunGeonInfo>().discription = (jsonData["Single"][idx]["discription"]);
        character.GetComponent<DunGeonInfo>().difficult = (jsonData["Single"][idx]["difficulty"]);

        character.transform.SetParent(singleContent.transform);
    }

    public void InstChaosDunGeon(int n)
    {
        string json = txtFile.text;
        var jsonData = JSON.Parse(json);

        int idx = n; // 매개변수

        GameObject character = Instantiate(jsonObject2); // 만들거야

        character.transform.name = jsonData["Chaos"][idx]["dungeonName"]; // 오브젝트명 정의

        character.GetComponent<DunGeonInfo>().dunGeonName = (jsonData["Chaos"][idx]["dungeonName"]);
        character.GetComponent<DunGeonInfo>().discription = jsonData["Chaos"][idx]["discription"];
        character.GetComponent<DunGeonInfo>().difficult = (jsonData["Chaos"][idx]["difficulty"]);

        character.transform.SetParent(chaosContent.transform);
    }

    public void InstRaidDunGeon(int n)
>>>>>>> Stashed changes
    {
        string json = txtFile.text;
        var jsonData = JSON.Parse(json);

<<<<<<< Updated upstream
        int idx = n-1; // 매개변수

        GameObject character = Instantiate(singleCell);

        character.transform.name = jsonData["Single"][idx]["dungeonName"];

        character.GetComponent<CellsInfo_Single>().dungeonIdxNum = (int)(jsonData["Single"][idx]["num"]);
        character.GetComponent<CellsInfo_Single>().dungeonName = (jsonData["Single"][idx]["dungeonName"]);
        character.GetComponent<CellsInfo_Single>().discription = jsonData["Single"][idx]["Discription"];
        character.GetComponent<CellsInfo_Single>().difficult = (jsonData["Single"][idx]["difficulty"]);

        character.transform.SetParent(singleContent.transform);
    }
    public void InstChaosDunGeon(int n)
    {
        string json = txtFile.text;
        var jsonData = JSON.Parse(json);

        int idx = n-1; // 매개변수

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

        int idx = n-1; // 매개변수

        GameObject character = Instantiate(raidCell);

        character.transform.name = jsonData["Raid"][idx]["dungeonName"];

        character.GetComponent<CellsInfo_Raid>().dungeonIdxNum = (int)(jsonData["Raid"][idx]["num"]);
        character.GetComponent<CellsInfo_Raid>().dungeonName = (jsonData["Raid"][idx]["dungeonName"]);
        character.GetComponent<CellsInfo_Raid>().discription = jsonData["Raid"][idx]["Discription"];
        character.GetComponent<CellsInfo_Raid>().difficult = (jsonData["Raid"][idx]["difficulty"]);

        character.transform.SetParent(raidContent.transform);
    }
=======
        int idx = n; // 매개변수

        GameObject character = Instantiate(jsonObject3); // 만들거야

        character.transform.name = jsonData["Raid"][idx]["dungeonName"]; // 오브젝트명 정의

        character.GetComponent<DunGeonInfo>().dunGeonName = (jsonData["Raid"][idx]["dungeonName"]);
        character.GetComponent<DunGeonInfo>().discription = jsonData["Raid"][idx]["discription"];
        character.GetComponent<DunGeonInfo>().difficult = (jsonData["Raid"][idx]["difficulty"]);

        character.transform.SetParent(raidContent.transform);
    }
>>>>>>> Stashed changes

    public void OnSinglePanel()
    {
        singlePanel.SetActive(true);
        chaosPanel.SetActive(false);
        raidPanel.SetActive(false);
<<<<<<< Updated upstream
        dungeonSort = 1;
        string single = "혼자와써? 응 싱글이야.";
        discription.text = single;
=======
        dunGeonSort = 1;
        //discription.text = 
>>>>>>> Stashed changes
    }

    public void OnChaosPanel()
    {
        singlePanel.SetActive(false);
        chaosPanel.SetActive(true);
        raidPanel.SetActive(false);
<<<<<<< Updated upstream
        dungeonSort = 2;
        string chaos = "카오스 던전을 선택하세요.";
        discription.text = chaos;
=======
        dunGeonSort = 2;
>>>>>>> Stashed changes
    }

    public void OnRaidPanel()
    {
        singlePanel.SetActive(false);
        chaosPanel.SetActive(false);
        raidPanel.SetActive(true);
<<<<<<< Updated upstream
        dungeonSort = 3;
        string raid = "레이드 던전을 선택하세요.";
        discription.text = raid;
=======
        dunGeonSort = 3;
>>>>>>> Stashed changes
    }

    public void BackDungeon()
    {
        singlePanel.SetActive(false);
        chaosPanel.SetActive(false);
        raidPanel.SetActive(false);
        string main = "던전을 선택해.";
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