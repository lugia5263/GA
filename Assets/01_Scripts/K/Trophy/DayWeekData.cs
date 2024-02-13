using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayWeekData : MonoBehaviour
{
    public ImageList imagelist;
    public InventoryManager inventoryMgr;

    //������ ���� �� ������Ʈ�� ���� ������

    [Header("����Ʈ �̸�, ����")]
    public string trophyName; //����â�� ����ٰ� �˾�
    public string goalName; //���� ����
    public int curGoal = 0;
    public int goal;
    public GameObject getEndBtn;


    [Header("����Ʈ ���Ǹ�����")] //��°�
    public int rewardItem; //��� ������, ������ ������ ���ڸ� �±׷� �޾Ƽ� ������ ȹ��
    public int rewardCount; //��� ������ ����

    [Header("��� Īȣ")]
    public string styleName;

    

    private void Start()
    {
        //inventoryMgr = GameObject.Find("InventoryMgr").GetComponent<InventoryManager>();
        //imagelist = GameObject.Find("ImageList").GetComponent<ImageList>();

        gameObject.transform.GetChild(2).GetComponent<Text>().text = trophyName;
        gameObject.transform.GetChild(3).GetComponent<Text>().text = goalName;
        //gameObject.transform.GetChild(4).GetComponent<Image>().sprite = imagelist.meterialsImage[rewardItem];
        gameObject.transform.GetChild(5).GetComponent<Text>().text = rewardCount.ToString();
        gameObject.transform.GetChild(6).GetComponent<Text>().text = $"{curGoal} / {goal}";
        getEndBtn = gameObject.transform.GetChild(8).gameObject;
        getEndBtn.SetActive(false);
    }

    public void InitAcheive()
    {
        if(curGoal >= goal)
        {
            gameObject.transform.GetChild(7).gameObject.SetActive(true);
        }
        gameObject.transform.GetChild(6).GetComponent<Text>().text = $"{curGoal} / {goal}";
        
    }


    public void GetRewardBtn() // ���� ���� ��ư
    {
        //����� â �˾�
        if(rewardItem == 2) //2���� ���
        {
            inventoryMgr.materials += rewardCount;
        }
        if (rewardItem == 3) //3���� ������
        {
            inventoryMgr.gold += rewardCount;
        }
        inventoryMgr.InitInventory();
        getEndBtn.SetActive(true);

    }
}

