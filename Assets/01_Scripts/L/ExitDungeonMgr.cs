using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class ExitDengeonMgr : MonoBehaviourPunCallbacks
{
    public Button exitBtn;

    private void Awake()
    {
        // Exit 버튼 이벤트 연결
        exitBtn.onClick.AddListener(() => OnClickExitDuneonBtn());
    }
    public void OnClickExitDuneonBtn()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.JoinRoom("Room_Home");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);

        PhotonNetwork.CreateRoom("Room_Home");
    }
}
