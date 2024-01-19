using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MakeCount : MonoBehaviour
{
    private ItemJsonData jsonChar;
    private Text countTxt;
    void Start()
    {
        jsonChar = gameObject.GetComponentInParent<ItemJsonData>();
        countTxt = gameObject.GetComponent<Text>();
        countTxt.text = jsonChar.count.ToString();
    }


}
