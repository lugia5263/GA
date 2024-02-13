using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
using System;
using JetBrains.Annotations;

public class DungeonLoadManager : MonoBehaviourPunCallbacks
{
    public DataMgrDontDestroy dataMgrDontDestroy;

    public int roomCnt = 0;

    public Text roomName;
    public Text connectInfo;
    public Text msgList;
    public Image bgImage;
    public Sprite[] bgImages;


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

        roomName.text = $"{CurDunGeonInfoMaker()}  {CurDunGeonLevelMaker()}";
        if (dataMgrDontDestroy.DungeonSortIdx == 0)
            roomName.text = "������ ���ư��� ���Դϴ� . . .";
 

    }

    // �����ΰ��������  �������� ������ dataMgrDontDestroy.dungeonSortIdx�� 0���� �ϰ��� �ε����� ����ȴ�.
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
                    PhotonNetwork.JoinRoom("Room_Home");
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
                CreateHome();
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
                        PhotonNetwork.LoadLevel("Raid");
                        PhotonNetwork.AutomaticallySyncScene = true;
                    }
                    break;
                default:
                    if (PhotonNetwork.IsMasterClient)
                    {
                        PhotonNetwork.LoadLevel("Town");
                        PhotonNetwork.AutomaticallySyncScene = true;
                    }
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
        ro.MaxPlayers = 4;     // �뿡 ������ �� �ִ� �ִ� ������ ��
        ro.IsOpen = true;       // ���� ���� ����
        ro.IsVisible = true;    // �κ񿡼� �� ��Ͽ� �����ų ����

        PhotonNetwork.CreateRoom($"Room_Raid_{Cnt}", ro);
    }

    public void CreateHome()
    {
        Debug.Log("CreateHome ����");

        // ���� �Ӽ� ����
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 20;     // �뿡 ������ �� �ִ� �ִ� ������ ��
        ro.IsOpen = true;       // ���� ���� ����
        ro.IsVisible = true;    // �κ񿡼� �� ��Ͽ� �����ų ����

        PhotonNetwork.CreateRoom("Room_Home");
    }

    // �� ���� ������ ���
    void SetRoomInfo()
    {
        Room room = PhotonNetwork.CurrentRoom;
        //roomName.text = room.Name;


        connectInfo.text = $"({room.PlayerCount}/{room.MaxPlayers})";
    }


    public string CurDunGeonInfoMaker()
    {
        string sortDungeon = "";

        dataMgrDontDestroy = DataMgrDontDestroy.Instance;
        if (dataMgrDontDestroy.DungeonSortIdx == 1)
        {
            sortDungeon = "Single Dungeon";
            bgImage.sprite = bgImages[1];
            roomName.color = Color.yellow;
        }
        else if (dataMgrDontDestroy.DungeonSortIdx == 2)
        {
            sortDungeon = "Chaos Dungeon";
            bgImage.sprite = bgImages[2];
            roomName.color = Color.cyan;
        }
        else if (dataMgrDontDestroy.DungeonSortIdx == 3)
        {
            sortDungeon = "Raid";
            bgImage.sprite = bgImages[3];
            roomName.color = Color.red;
        }

        return sortDungeon;




    }
    public string CurDunGeonLevelMaker()
    {
        string dungeonLV = "";

        dataMgrDontDestroy = DataMgrDontDestroy.Instance;

        dungeonLV = dataMgrDontDestroy.DungeonNumIdx.ToString();

        return dungeonLV;
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
}
