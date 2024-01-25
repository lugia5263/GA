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
    public Image coolTImeFillE;
    public Image coolTImeFillR;

    public Image qskillIcon;
    public Image eskillIcon;
    public Image rskillIcon;


    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        
        if(player != null)
        {
            coolTImeFillQ = GameObject.Find("CoolTImeFillQ").GetComponent<Image>();
            coolTImeFillE = GameObject.Find("CoolTimeFillE").GetComponent<Image>();
            coolTImeFillR = GameObject.Find("CoolTimeFillR").GetComponent<Image>();

            qskillIcon = GameObject.Find("SkillIconQ").GetComponent<Image>();
            qskillIcon = GameObject.Find("SkillIconE").GetComponent<Image>();
            qskillIcon = GameObject.Find("SkillIconR").GetComponent<Image>();
        }
    }

    void Update()
    {
        if(player != null)
        {
            coolTImeFillQ.fillAmount = player.qskillcool / player.curQskillcool;
            coolTImeFillE.fillAmount = player.eskillcool / player.curEskillcool;
            coolTImeFillR.fillAmount = player.rskillcool / player.curRskillcool;

        }
        else
        {
            Debug.Log("플레이어 못찾음");
        }
    }
}
