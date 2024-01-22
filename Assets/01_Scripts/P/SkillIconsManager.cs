using JetBrains.Annotations;

using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillIconsManager : MonoBehaviour
{
   // public StateManager stateManager;
    public Player player;

    public Image coolTImeFillQ; //아이콘에 필어마운트 가져올거
    //public Image coolTImeFillE;
    //public Image coolTImeFillR;

    public Image qskillIcon;
    //public Image eskillIcon;
    //public Image rskillIcon;


    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        coolTImeFillQ = GameObject.Find("CoolTImeFillQ").GetComponent<Image>();
        //coolTImeFillE = GameObject.Find("SkillIconE").GetComponent<Image>();
        //coolTImeFillR = GameObject.Find("SkillIconR").GetComponent <Image>();
        qskillIcon = GameObject.Find("SkillIconQ").GetComponent<Image>();
    }

    void Update()
    {
        if(player != null)
        {
            coolTImeFillQ.fillAmount = player.Qskillcool / player.CurQskillcool;
            //coolTImeFillE.fillAmount = player.CurEskillcool / player.Eskillcool;
            //coolTImeFillR.fillAmount = player.CurRskillcool / player.Rskillcool;

        }
        else
        {
            Debug.Log("플레이어 못찾음");
        }


    }
}
