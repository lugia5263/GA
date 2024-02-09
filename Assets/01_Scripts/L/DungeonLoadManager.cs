using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;

public class DungeonLoadManager : MonoBehaviourPunCallbacks
{
    public DataMgrDontDestroy dataMgrDontDestroy;

    public int roomCnt = 0;

    public TMP_Text roomName;
    public TMP_Text connectInfo;
    public TMP_Text msgList;

    private void Awake()
    {
        dataMgrDontDestroy = DataMgrDontDestroy.Instance;

        if (!PhotonNetwork.IsConnected)
        {
            Debug.Log("���� �����Ʈ��ũ ������ �ƴϱ⿡ �����մϴ�.");
            PhotonNetwork.ConnectUsingSettings();
        }
        if (!PhotonNetwork.InLobby)
        {
            Debug.Log("���� �κ� ���⿡ �κ� �����մϴ�.");
            PhotonNetwork.JoinLobby();
        }
    }

    IEnumerator LoadLevelRaidDungeon()
    {
        yield return null;

        PhotonNetwork.LoadLevel("RaidDungeon");
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    IEnumerator EnterDungeonRoom()
    {
        if (PhotonNetwork.InLobby)
        {
            switch (dataMgrDontDestroy.dungeonSortIdx)
            {
                case 1:
                    PhotonNetwork.JoinRoom($"Room_Single_{roomCnt}");
                    break;
                case 2:
                    PhotonNetwork.JoinRoom($"Room_Chaos_{roomCnt}");
                    break;
                case 3:
                    PhotonNetwork.JoinRoom($"Room_Raid_{roomCnt}");
                    break;
                default:
                    break;
            }
        }
        else
        {
            Debug.Log("EnterDungeonRoom�� if���� ����������");
        }

        yield return null;
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("���� inLobby : " + PhotonNetwork.InLobby);
        base.OnJoinRoomFailed(returnCode, message);
        Debug.Log("���� �����մϴ�");
        switch (dataMgrDontDestroy.dungeonSortIdx)
         {
            case 1:
                CreateSingleRoom(roomCnt);
                break;
            case 2:
                CreateChaosRoom(roomCnt);
                break;
            case 3:
                CreateRaidRoom(roomCnt);
                break;
            default:
                break;
            }
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Room���� ����");
        SetRoomInfo();

        if (PhotonNetwork.InRoom)
        {
            switch (dataMgrDontDestroy.dungeonSortIdx)
            {
                case 1:
                    if (PhotonNetwork.IsMasterClient)
                    {
                        PhotonNetwork.LoadLevel("SingleDungeon");
                    }
                    break;
                case 2:
                    if (PhotonNetwork.IsMasterClient)
                    {
                        PhotonNetwork.LoadLevel("ChaosD");
                    }
                    break;
                case 3:
                    if (PhotonNetwork.IsMasterClient)
                    {
                        PhotonNetwork.AutomaticallySyncScene = true;
                    }
                    break;
                default:
                    break;
            }
        }
        else
        {
            Debug.Log("OnJoinedRoom �ݹ��Լ��� if���� ����������");
        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        roomCnt++;
        StartCoroutine(EnterDungeonRoom());
    }

    public void CreateSingleRoom(int Cnt)
    {
        Debug.Log("CreateRaidRoom ����");

        // ���� �Ӽ� ����
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 1;     // �뿡 ������ �� �ִ� �ִ� ������ ��
        ro.IsOpen = true;       // ���� ���� ����
        ro.IsVisible = false;    // �κ񿡼� �� ��Ͽ� �����ų ����

        PhotonNetwork.CreateRoom($"Room_Single_{Cnt}", ro);
    }

    public void CreateChaosRoom(int Cnt)
    {
        Debug.Log("CreateRaidRoom ����");

        // ���� �Ӽ� ����
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 1;     // �뿡 ������ �� �ִ� �ִ� ������ ��
        ro.IsOpen = true;       // ���� ���� ����
        ro.IsVisible = false;    // �κ񿡼� �� ��Ͽ� �����ų ����

        PhotonNetwork.CreateRoom($"Room_Chaos_{Cnt}", ro);
    }

    public void CreateRaidRoom(int Cnt)
    {
        Debug.Log("CreateRaidRoom ����");

        // ���� �Ӽ� ����
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 3;     // �뿡 ������ �� �ִ� �ִ� ������ ��
        ro.IsOpen = true;       // ���� ���� ����
        ro.IsVisible = true;    // �κ񿡼� �� ��Ͽ� �����ų ����

        PhotonNetwork.CreateRoom($"Room_Raid_{Cnt}", ro);
    }

    // �� ���� ������ ���
    void SetRoomInfo()
    {
        Room room = PhotonNetwork.CurrentRoom;
        roomName.text = room.Name;
        connectInfo.text = $"({room.PlayerCount}/{room.MaxPlayers})";
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master!");
        Debug.Log($"PhotonNetwork.InLobby = {PhotonNetwork.InLobby}");
        PhotonNetwork.JoinLobby();
    }

    // �κ� �����Ϸ� ������ �ݹ��Լ�
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("�κ� ���� �Ϸ�.");
        Debug.Log("���� inLobby : "+PhotonNetwork.InLobby);
        StartCoroutine(EnterDungeonRoom());
    }

    // ������ ���ο� ��Ʈ��ũ ������ �����߶� ȣ��Ǵ� �ݹ��Լ�
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        SetRoomInfo();
        string msg = $"\n<color=#00ff00>{newPlayer.NickName}</color> is joined room";
        msgList.text += msg;

        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            Debug.Log("CurPlayerCount==MaxPlayerCount �Դϴ�.");
            if (PhotonNetwork.IsMasterClient)
            {
                Debug.Log("������ Ŭ���̾�Ʈ �Դϴ�. �ڷ�ƾ ������.");
                StartCoroutine(LoadLevelRaidDungeon());
            }
            else
            {
                Debug.Log("������ Ŭ���̾�Ʈ�� �ƴմϴ�. �ڷ�ƾ�������.");
            }
        }
    }

    // �뿡�� ��Ʈ��ũ ������ �����߶� ȣ��Ǵ� �ݹ��Լ�
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        SetRoomInfo();
        string msg = $"\n<color=#ff0000>{otherPlayer.NickName}</color> is left room";
        msgList.text += msg;
    }
}
