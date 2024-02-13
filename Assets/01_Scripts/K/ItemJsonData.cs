using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemJsonData : MonoBehaviour
{
    //������ ���� �� ������Ʈ�� ���� ������
    [Header("����")]
    public int count = 0;

    [Header("����")]
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
