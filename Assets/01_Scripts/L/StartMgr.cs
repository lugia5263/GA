using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class StartMgr : MonoBehaviourPunCallbacks
{
    public GameObject selectPanel;

    public GameObject roomName;
    public GameObject connectInfo;
    public GameObject msgList;
    public GameObject exitBtn;

    public GameObject chatBox;

    private void Start()
    {
        selectPanel.SetActive(true);
        roomName.SetActive(false);
        connectInfo.SetActive(false);
        msgList.SetActive(false);
        exitBtn.SetActive(false);
        chatBox.SetActive(false);
    }
    public void OnClickStart()
    {
        Debug.Log("start ´­¸²");
        selectPanel.SetActive(false);
        roomName.SetActive(true);
        connectInfo.SetActive(true);
        msgList.SetActive(true);
        exitBtn.SetActive(true);
        chatBox.SetActive(true);
    }
}
