using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON; //########################댕겨와야행

public class TrophyMgr : MonoBehaviour
{

    public TextAsset txtFile; //Jsonfile
    public GameObject jsonObject; //Prefab (Json char 달린)

    public GameObject achievementContent;

    private void Start()
    {
        var jsonitemFile = Resources.Load<TextAsset>("Json/TrophyTable");
        txtFile = jsonitemFile;
        jsonObject = Resources.Load<GameObject>("Prefabs/Achievement");
        achievementContent = GameObject.Find("AchievementContent");


        string json = txtFile.text;
        var jsonData = JSON.Parse(json);

        for (int i = 1; i < jsonData["Achievement"].Count; i++) // 대박 쩔었다 강사님 ㄳㄳ 교수님 ㄳㄳ
        {
            InstachievementContent(i);
        }


    }

    public void InstachievementContent(int n)
    {

        string json = txtFile.text;
        var jsonData = JSON.Parse(json);


        int item = n-1; // 매개변수


        GameObject character = Instantiate(jsonObject); // 만들거야

        character.transform.name = jsonData["Achievement"][item]["GoalName"].ToString(); // 오브젝트명 정의

        character.GetComponent<TrophyJsonData>().trophyName = (jsonData["Achievement"][item]["TrophyName"]);
        character.GetComponent<TrophyJsonData>().goalName = jsonData["Achievement"][item]["GoalName"];
        character.GetComponent<TrophyJsonData>().goal = (int)(jsonData["Achievement"][item]["Goal"]);
      
        character.GetComponent<TrophyJsonData>().rewardItem = (int)(jsonData["Achievement"][item]["RewardItem"]);
        character.GetComponent<TrophyJsonData>().rewardCount = (int)(jsonData["Achievement"][item]["RewardCount"]);

        character.GetComponent<TrophyJsonData>().styleName = (jsonData["Achievement"][item]["StyleName"]);


        character.transform.SetParent(achievementContent.transform);

    } //업적 만들기




}
