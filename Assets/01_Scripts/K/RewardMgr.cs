using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardMgr : MonoBehaviour
{
    public DataMgrDontDestroy dataMgrDontDestroy;

    public GameObject rewardPanel;
    public GameObject movePanel;
    public GameObject defeatPanel;
    public GameObject rewardContent;
    public GameObject itemPrefab;
    public Sprite[] imageList; //0���� material, 1���� expPotion, 2���� gold
    public int DgSortIdx;
    public int expPotionReward; 
    public int materialReward;

    public int goldReward;
    public int clearCut = 1;
    //����=(��ȭ ����) * ������ ��ȣ? 1-2, 1-3�� 2�� 3�� ��ȭ������ �����ִ� �̷���(�ϴ� �̷��� ������ �س��� �ʿ�� ����)
    //������ ������ ���� �����带 �����ϰ�, ���̵�������(�� �����ܰ��� �����̸�) �׸�ŭ �����忡 �����ش�.

    private void Start()
    {
        dataMgrDontDestroy = DataMgrDontDestroy.Instance;
        DgSortIdx = dataMgrDontDestroy.DungeonSortIdx;
        rewardPanel.SetActive(false);
        movePanel.SetActive(false);
    }

    private void Update()
    {
        playerDieful();
    }
    public void ShowReward()
    {
        rewardPanel.SetActive(true);
        movePanel.SetActive(true);
        switch (DgSortIdx)
        {
            case 1:
                SoloClearReward();
                break;
            case 2:
                ChaosClearReward();
                break;
            case 3:
                RaidClearReward();
                break;
            default:
                break;
        }
    }

    public void playerDieful()
    {
        if(dataMgrDontDestroy.playerDie)
        {
            movePanel.SetActive(true);  
            defeatPanel.SetActive(true);
        }
    }
        
    public void OnClickReceiveBtn()
    {
        clearCut = 0;
        dataMgrDontDestroy.UserExpPotion += expPotionReward;
        dataMgrDontDestroy.UserMaterial += materialReward;
        dataMgrDontDestroy.UserGold += goldReward;
        rewardPanel.SetActive(false);
    }

    #region Ŭ����� ����
    public void SoloClearReward()
    {
        expPotionReward = 3;
        materialReward = 100;

        expPotionReward *= dataMgrDontDestroy.dungeonNumIdx;
        materialReward *= dataMgrDontDestroy.dungeonNumIdx;
        InstExp(expPotionReward);
        InstMaterial(materialReward);
    }

    public void ChaosClearReward()
    {
        expPotionReward = 500;
        materialReward = 300;

        expPotionReward *= dataMgrDontDestroy.dungeonNumIdx;
        materialReward *= dataMgrDontDestroy.dungeonNumIdx;
        InstExp(expPotionReward);
        InstMaterial(materialReward);
    }

    public void RaidClearReward()
    {
        expPotionReward = 1000;
        goldReward = 15000;

        expPotionReward *= dataMgrDontDestroy.dungeonNumIdx;
        goldReward *= dataMgrDontDestroy.dungeonNumIdx;
        InstExp(expPotionReward);
        InstGold(goldReward);
    }
    #endregion

    #region ��ũ�Ѻ� Content�� ���� ���̰��ϱ�
    public void InstMaterial(int itemcount)
    {
        GameObject character = Instantiate(itemPrefab);

        character.transform.GetChild(0).GetComponent<Image>().sprite = imageList[0];
        character.GetComponentInChildren<Text>().text = itemcount.ToString();
        character.transform.SetParent(rewardContent.transform);
    }

    public void InstExp(int itemcount)
    {
        GameObject character = Instantiate(itemPrefab);

        character.transform.GetChild(0).GetComponent<Image>().sprite = imageList[1];
        character.GetComponentInChildren<Text>().text = itemcount.ToString();
        character.transform.SetParent(rewardContent.transform);
    }

    public void InstGold(int itemcount)
    {
        GameObject character = Instantiate(itemPrefab);

        character.transform.GetChild(0).GetComponent<Image>().sprite = imageList[2];
        character.GetComponentInChildren<Text>().text = itemcount.ToString();
        character.transform.SetParent(rewardContent.transform);
    }
    #endregion
}