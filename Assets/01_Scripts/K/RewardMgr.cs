using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardMgr : MonoBehaviour
{
    public static RewardMgr reward;
    public GameObject rewardContent;

    private void Start()
    {
        //imagelist = GameObject.Find("ImageList").GetComponent<ImageList>();
        rewardContent = GameObject.Find("RewardContent");
    }

    public void clear()
    {
        InstExp(3, 100);
    }
            ///예제입니다. 이렇게 불러오세요!!!
    public void MakeItem(int itemIdx, int count) // n번째 아이템을 count개 얻음
    {
        InstMaterial(itemIdx, count);
    }
    public void MakeItemOne(int itemIdx) // n번째 아이템을 1개 얻음
    {
        InstMaterial(itemIdx, 1);
    }
    public void MakeItemRandomBtn()
    {
        int makeidx = Random.Range(1, 5);// 1에서 4까지 나옴
        InstMaterial(makeidx, 1);
    } // 랜덤으로 재료 1개 얻는 버튼
    public void SoloClearReward()
    {

    }
    public void ChaosClearReward()
    {

    }
    public void RaidClearReward()
    {

    }
    public void QuestClearReward()
    {

    }
    public void Reward100exp3EABtn()
    {
        //앞자리 3으로 통일할게요!!!!!
        InstExp(3, 100);
        InstMaterial(3, 200);
        InstGOld(100); //3
    }






    // 재료소환 함수
    public void InstMaterial(int n, int itemcount)
    {

        //character.GetComponent<Image>().sprite = imagelist.meterialsImage[n];

        //character.tag = "Material";
        //character.transform.SetParent(rewardContent.transform);

    } 


    //경험치 물약 소환 함수
    public void InstExp(int n, int itemcount) 
    {
        int item = n-1; // 매개변수

        //character.tag = "Exp";
        //character.transform.SetParent(rewardContent.transform);

    } 


    // 골드 소환 함수
    public void InstGOld(int itemcount)
    {

        //character.tag = "Gold";
        //character.transform.SetParent(rewardContent.transform);
    }



}