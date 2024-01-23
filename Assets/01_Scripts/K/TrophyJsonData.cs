using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrophyJsonData : MonoBehaviour
{
    //파일이 생성 후 오브젝트가 담을 정보들

    [Header("퀘스트 이름, 조건")]
    public string trophyName; //메인창에 얻었다고 팝업
    public string goalName;
    public int goal;

    [Header("퀘스트 조건만족시")] //얻는거
    public int rewardItem; //얻는 아이템, 아이템 얻으면 숫자를 태그로 받아서 아이템 획득
    public int rewardCount; //얻는 아이템 개수

    [Header("얻는 칭호")]
    public string styleName;



    private void Start()
    {
       // gameObject.transform.GetChild(0).GetComponent<Text>().text = count.ToString();
    }

}

