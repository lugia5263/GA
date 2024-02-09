using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using Cinemachine;
public class SkillIconsManager : MonoBehaviour
{
   // public StateManager stateManager;
    public Player player;
    public PhotonView pv;
    public Image coolTImeFillQ; //아이콘에 필어마운트 가져올거
    public Image coolTImeFillE;
    public Image coolTImeFillR;
    public GameObject qskillIcon;
    public GameObject eskillIcon;
    public GameObject rskillIcon;

    void Awake()
    {
        if(pv.IsMine)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
    }

    private void Start()
    {
        if(pv.IsMine)
        {
            coolTImeFillQ.fillAmount = 0;
            coolTImeFillE.fillAmount = 0;
            coolTImeFillR.fillAmount = 0;
        }
    }

    void Update()
    {
        if (pv.IsMine)
        {
            qskillIcon.SetActive(true);
            eskillIcon.SetActive(true);
            rskillIcon.SetActive(true);
            if (!player.qisReady)
            {
                coolTImeFillQ.fillAmount = 1 - player.qskillcool / player.curQskillcool;
            }
            if (!player.eisReady)
            {
                coolTImeFillE.fillAmount = 1 - player.eskillcool / player.curEskillcool;
            }
            if (!player.risReady)
            {
                coolTImeFillR.fillAmount = 1 - player.rskillcool / player.curRskillcool;
            }
        }
    }
}
