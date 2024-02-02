using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class RaidDungeonManager : MonoBehaviourPunCallbacks
{
    public SpawnScipt spawnMgr;

    public TMP_Text roomName;
    public TMP_Text connectInfo;
    public TMP_Text msgList;

    public Button exitBtn;

    private void Awake()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        // ���� ���� ���� �� ǥ��
        SetRoomInfo();
        // Exit ��ư �̺�Ʈ ����
        //exitBtn.onClick.AddListener(() => OnClickExitBtn());

        Debug.Log("Ŭ���̾�Ʈ ����: " + PhotonNetwork.NetworkClientState + ", InRoom : "+PhotonNetwork.InRoom+ ", InRoom : "+PhotonNetwork.InLobby);
        spawnMgr = GameObject.Find("SpawnMgr").GetComponent<SpawnScipt>();
    }

    private void Start()
    {
        spawnMgr.CreatePlayer();
    }

    // Exit ��ư�� OnClick�� ������ �Լ�
    public void OnClickExitBtn()
    {
        RoomEnterManager.dungeonType = "None";
        PhotonNetwork.LeaveRoom();
    }

    #region ���� �ݹ��Լ�
    // ���� ������ ���� �� ȣ��Ǵ� �ݹ� �Լ�
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master!");
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}");
        PhotonNetwork.JoinLobby();
    }

    // ���� �뿡�� �������� �� ȣ��Ǵ� �ݹ��Լ�
    public override void OnLeftRoom()
    {
        Debug.Log("�� ������ �Ϸ�.");
        PhotonNetwork.JoinLobby();
        //SceneManager.LoadScene("Lobby");
    }

    // �κ� �����Ϸ� ������ �ݹ��Լ�
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("�κ� ���� �Ϸ�.");
        PhotonNetwork.JoinRoom("Room_Home");
        Debug.Log("Room_Home�� Join ����");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        Debug.Log("Room_Home�� Join ����");

        // ���� �Ӽ� ����
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 20;     // �뿡 ������ �� �ִ� �ִ� ������ ��
        ro.IsOpen = true;       // ���� ���� ����
        ro.IsVisible = true;    // �κ񿡼� �� ��Ͽ� �����ų ����

        PhotonNetwork.CreateRoom("Room_Home", ro);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Room_Home�� Join ����");

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Town");
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        Debug.Log("���Ӳ���. ������ ����..");
        PhotonNetwork.ConnectUsingSettings();
    }
    #endregion

    #region RoomInfo
    // �� ���� ������ ���
    void SetRoomInfo()
    {
        Room room = PhotonNetwork.CurrentRoom;
        roomName.text = room.Name;
        connectInfo.text = $"({room.PlayerCount}/{room.MaxPlayers})";
    }

    // ������ ���ο� ��Ʈ��ũ ������ �����߶� ȣ��Ǵ� �ݹ��Լ�
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        SetRoomInfo();
        string msg = $"\n<color=#00ff00>{newPlayer.NickName}</color> is joined room";
        msgList.text += msg;
    }

    // �뿡�� ��Ʈ��ũ ������ �����߶� ȣ��Ǵ� �ݹ��Լ�
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        SetRoomInfo();
        string msg = $"\n<color=#ff0000>{otherPlayer.NickName}</color> is left room";
        msgList.text += msg;
    }
    #endregion
}
