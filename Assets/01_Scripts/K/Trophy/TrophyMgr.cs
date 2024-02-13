using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON; //########################��ܿ;���

public class TrophyMgr : MonoBehaviour
{
    #region �̱���
    private static TrophyMgr instance;
    public static TrophyMgr Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<TrophyMgr>();
            }
            return instance;
        }
    }
    #endregion

    public TextAsset txtFile; //Jsonfile
    public GameObject jsonObjectD;
    public GameObject jsonObjectW;
    public GameObject jsonObject; //Prefab (Json char �޸�)

    public GameObject dailyContent;
    public GameObject weekContent;
    public GameObject achievementContent; // �̰� ���ҽ��������� �ȹ޾����� �׳� ������ //TODO:


    public GameObject trophyPanel; //TODO: ���� p�� ������ npc����� ����!!

    public GameObject dailyPanel;
    public GameObject weekPanel;
    public GameObject achievementPanel;
    public bool isTrophy; //TODO: npc����� ����!!

    private void Awake()
    {
        trophyPanel = GameObject.Find("TrophyCanvas"); //TODO: npc����� ����!!



        var jsonitemFile = Resources.Load<TextAsset>("Json/TrophyTable");
        txtFile = jsonitemFile;

        dailyContent = GameObject.Find("DailyContent");
        weekContent = GameObject.Find("WeekContent");
        achievementContent = GameObject.Find("AchievementContent");

        dailyPanel = GameObject.Find("DailyPanel");
        weekPanel = GameObject.Find("WeekPanel");
        achievementPanel = GameObject.Find("AchievementPanel");
        


        string json = txtFile.text;
        var jsonData = JSON.Parse(json);
        for (int i = 1; i < jsonData["DailyA"].Count + 1; i++)
        {
            InstDailyContent(i);
        }
        for (int i = 1; i < jsonData["WeekA"].Count + 1; i++)
        {
            InstWeekContent(i);
        }
        for (int i = 1; i < jsonData["Achievement"].Count + 1; i++)
        {
            InstachievementContent(i);
        }
    }
    private void Start()
    {


    }

    public void InstDailyContent(int n)
    {

        string json = txtFile.text;
        var jsonData = JSON.Parse(json);


        int item = n - 1; // �Ű�����


        GameObject character = Instantiate(jsonObjectD); // ����ž�

        character.transform.name = jsonData["DailyA"][item]["GoalName"]; // ������Ʈ�� ����

        character.GetComponent<DayQData>().trophyName = (jsonData["DailyA"][item]["TrophyName"]);
        character.GetComponent<DayQData>().goalName = jsonData["DailyA"][item]["GoalName"];
        character.GetComponent<DayQData>().goal = (int)(jsonData["DailyA"][item]["Goal"]);

        character.GetComponent<DayQData>().rewardItem = (int)(jsonData["DailyA"][item]["RewardItem"]);
        character.GetComponent<DayQData>().rewardCount = (int)(jsonData["DailyA"][item]["RewardCount"]);


        character.transform.SetParent(dailyContent.transform);

        character.transform.GetChild(7).gameObject.SetActive(false);
        } //��������Ʈ �����
    public void InstWeekContent(int n)
    {

        string json = txtFile.text;
        var jsonData = JSON.Parse(json);


        int item = n - 1; // �Ű�����


        GameObject character = Instantiate(jsonObjectW); // ����ž�

        character.transform.name = jsonData["WeekA"][item]["GoalName"]; // ������Ʈ�� ����

        character.GetComponent<WeekQData>().trophyName = (jsonData["WeekA"][item]["TrophyName"]);
        character.GetComponent<WeekQData>().goalName = jsonData["WeekA"][item]["GoalName"];
        character.GetComponent<WeekQData>().goal = (int)(jsonData["WeekA"][item]["Goal"]);

        character.GetComponent<WeekQData>().rewardItem = (int)(jsonData["WeekA"][item]["RewardItem"]);
        character.GetComponent<WeekQData>().rewardCount = (int)(jsonData["WeekA"][item]["RewardCount"]);


        character.transform.SetParent(weekContent.transform);

        character.transform.GetChild(7).gameObject.SetActive(false);

    } //�ְ�����Ʈ �����
    public void InstachievementContent(int n)
    {

        string json = txtFile.text;
        var jsonData = JSON.Parse(json);


        int item = n-1; // �Ű�����


        GameObject character = Instantiate(jsonObject); // ����ž�

        character.transform.name = jsonData["Achievement"][item]["GoalName"]; // ������Ʈ�� ����
        
        character.GetComponent<TrophyJsonData>().trophyName = (jsonData["Achievement"][item]["TrophyName"]);
        character.GetComponent<TrophyJsonData>().goalName = jsonData["Achievement"][item]["GoalName"];
        character.GetComponent<TrophyJsonData>().goal = (int)(jsonData["Achievement"][item]["Goal"]);
      
        character.GetComponent<TrophyJsonData>().rewardItem = (int)(jsonData["Achievement"][item]["RewardItem"]);
        character.GetComponent<TrophyJsonData>().rewardCount = (int)(jsonData["Achievement"][item]["RewardCount"]);

        character.GetComponent<TrophyJsonData>().styleName = (jsonData["Achievement"][item]["StyleName"]);


        character.transform.SetParent(achievementContent.transform);

        character.transform.GetChild(7).gameObject.SetActive(false);

    } //���� �����





    public enum ConditionCode
    {
        Lvup = 1, // �����޼�
        Enforced, //2 ��ȭ�õ�
        EnforceS, //3 ��ȭ����
        EnforceF, //4 ��ȭ����
        GoldSpend, //5 ���Ҹ�
        SingleMonDead, //6 �Ϲݸ��� óġ
        RaidBossDead, //7 ���̵�� ���
        HiddenMonDead, //8 ������� óġ
        NoDeadinRaid, //9 ���̵忡�� ������
        End
    }
    private ConditionCode conditionCode;


            //�̱��� ����� TrophyMgr.Instance.TrophyIndexUp(int n);���� ����!!!
    public void TrophyIndexUp(int n) // �̱������� �ƹ��뼭�� ���ܼ� �Ű������� ���� �ر� ���Ǵ���
    {
        conditionCode= (ConditionCode)n; 

        switch (conditionCode)
        {
            case ConditionCode.Lvup:
                achievementContent.transform.Find("�÷��̾� Lv.10 �޼��ϱ�").GetComponent<TrophyJsonData>().curGoal++;
                achievementContent.transform.Find("�÷��̾� Lv.10 �޼��ϱ�").GetComponent<TrophyJsonData>().InitAcheive();
                achievementContent.transform.Find("�÷��̾� Lv.40 �޼��ϱ�").GetComponent<TrophyJsonData>().curGoal++;
                achievementContent.transform.Find("�÷��̾� Lv.40 �޼��ϱ�").GetComponent<TrophyJsonData>().InitAcheive();
                break;
            case ConditionCode.Enforced:// ��ȭ ���������� int 2 �־ ȣ���ϱ�!!
                achievementContent.transform.Find("��ȭ 100���ϱ�").GetComponent<TrophyJsonData>().curGoal++;
                achievementContent.transform.Find("��ȭ 100���ϱ�").GetComponent<TrophyJsonData>().InitAcheive();
                break;
            case ConditionCode.EnforceS:
                break;
            case ConditionCode.EnforceF:
                break;
            case ConditionCode.GoldSpend:
                break;
            case ConditionCode.SingleMonDead:
                break;
            case ConditionCode.RaidBossDead:
                break;
            case ConditionCode.HiddenMonDead:
                break;
            case ConditionCode.NoDeadinRaid:
                break;
            case ConditionCode.End:
                break;
            default:
                break;
        }

    }


    private void Update() //TODO: ���߿� npc�� �ٲ�!!!!!! p ������ ���� ����
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!isTrophy)
            {
                trophyPanel.SetActive(true);
                isTrophy = true;
            }
            else
            {
                trophyPanel.SetActive(false);
                isTrophy = false;
            }
        }

    }




    public void DailyQBtn()
    {
        dailyPanel.SetActive(true);
        weekPanel.SetActive(false) ;
        achievementPanel.SetActive(false);
    }
    public void WeekQBtn()
    {
        dailyPanel.SetActive(false);
        weekPanel.SetActive(true);
        achievementPanel.SetActive(false);
    }
    public void AchieveBtn()
    {
        dailyPanel.SetActive(false);
        weekPanel.SetActive(false);
        achievementPanel.SetActive(true);
    }
    public void ChangeTrophy(int n) //1����, 2����, 3���� �Ⱦ�����
    {
        string json = txtFile.text;
        var jsonData = JSON.Parse(json);


        int item = n - 1; // �Ű�����
    } 
}
