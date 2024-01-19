using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData : MonoBehaviour
{
    //파일이 생성 후 오브젝트가 담을 정보들
    [Header("개수")]
    public int count = 0;

    [Header("정보")]
    public string charname;
    public string discription;
    public int atk;
    public int exp;

    //public int plusAtk;
}
