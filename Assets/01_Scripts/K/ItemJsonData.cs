using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemJsonData : MonoBehaviour
{
    //파일이 생성 후 오브젝트가 담을 정보들
    [Header("개수")]
    public int count = 0;

    [Header("정보")]
    public string charname;
    public string discription;
    public int atk;
    public int exp;
    public int rarity;

    private void Start()
    {
        gameObject.transform.GetChild(0).GetComponent<Text>().text = count.ToString();
    }
    //public int plusAtk;
}
