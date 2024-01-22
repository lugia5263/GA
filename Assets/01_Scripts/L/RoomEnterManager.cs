using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class RoomEnterManager : MonoBehaviourPunCallbacks
{
    public GameObject dungeonPanel;

    public void OnSoloDungeon1ButtonClick()
    {
        JoinDungeon("Dungeon1_", 1); // 솔로 던전, 최대 1명 입장 가능
    }

    public void OnSoloDungeon2ButtonClick()
    {
        JoinDungeon("Dungeon2_", 1); // 솔로 던전, 최대 1명 입장 가능
    }

    public void OnRaidDungeonButtonClick()
    {
        JoinDungeon("RaidDungeon", 4); // 멀티 던전, 최대 4명 입장 가능
    }

    public void JoinDungeon(string dungeonType, int maxPlayers)
    {
        Debug.Log("던전의 타입 : " + dungeonType);
        Debug.Log("maxPlayers : " + maxPlayers);

        RoomOptions roomOptions = new RoomOptions
        {
            MaxPlayers = (byte)maxPlayers,
            IsVisible = true, // 방을 리스트에 보이게 할 것인지 여부
            IsOpen = true,    // 방이 열려 있는지 여부
            CustomRoomProperties = new Hashtable { { "DungeonType", dungeonType } },
            CustomRoomPropertiesForLobby = new string[] { "DungeonType" }
        };

        // 방에 조인하거나 생성
        PhotonNetwork.JoinOrCreateRoom(dungeonType, roomOptions, null);
    }

    // userid 필요하면 사용
    public void SetUserId()
    {
        // 유저 이름 가져오기
        string userId = PlayerPrefs.GetString("USER_ID");
        // 접속 유저의 닉네임 등록
        PhotonNetwork.NickName = userId;
    }

    #region 포톤 콜백 함수
    public override void OnJoinedRoom()
    {
        dungeonPanel.SetActive(false);

        // 현재 방의 던전 타입을 가져옴
        if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("DungeonType", out object dungeonTypeObj))
        {
            string dungeonType = dungeonTypeObj.ToString();
            Debug.Log("던전의 이름은 : " + dungeonType);
            // 방장인 경우
            if (PhotonNetwork.IsMasterClient)
            {
                // 해당 던전 타입에 맞는 씬을 로드
                PhotonNetwork.LoadLevel(dungeonType);
            }
        }
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);

        //CreateDungeon();
    }
    #endregion

    // 던전입장 UI panel키는 버튼누르는 함수
    public void OnClickEnterDungeonBtn()
    {
        dungeonPanel.SetActive(true);
        PhotonNetwork.LeaveRoom();
        Debug.Log("방을 나갑니다..");
    }
}
