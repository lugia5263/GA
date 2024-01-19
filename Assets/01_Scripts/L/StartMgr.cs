using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class StartMgr : MonoBehaviourPunCallbacks
{
    public GameObject panel;

    public GameObject roomName;
    public GameObject connectInfo;
    public GameObject msgList;
    public GameObject exitBtn;

    private void Start()
    {
        panel.SetActive(true);
        roomName.SetActive(false);
        connectInfo.SetActive(false);
        msgList.SetActive(false);
        exitBtn.SetActive(false);
    }
    public void OnClickStart()
    {
        Debug.Log("start ´­¸²");
        panel.SetActive(false);
        roomName.SetActive(true);
        connectInfo.SetActive(true);
        msgList.SetActive(true);
        exitBtn.SetActive(true);
    }
}
