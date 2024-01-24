using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DunGeonInfo : MonoBehaviour
{
    public string dunGeonName;
    public string difficult;
    public string discription;

    public Image bossFace;
    public Text dunGeonNameTxt;
    public Text difficultTxt;

    private void Start()
    {
        dunGeonNameTxt = gameObject.transform.GetChild(2).GetComponent<Text>();
        difficultTxt = gameObject.transform.GetChild(3).GetComponent<Text>();
        dunGeonNameTxt.text = dunGeonName;
        difficultTxt.text = difficult;
    }
}
