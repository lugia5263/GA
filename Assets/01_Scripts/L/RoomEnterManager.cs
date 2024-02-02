using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine.SceneManagement;

public class RoomEnterManager : MonoBehaviourPunCallbacks
{
    public DataMgrDontDestroy dataMgrDontDestroy;

    //솔로던전은 roomleave하고 씬매니저로 돌릴것

    public void LeaveVillige()
    {
        dataMgrDontDestroy = DataMgrDontDestroy.Instance;

        switch (dataMgrDontDestroy.DungeonSortIdx)
        {
            case 1: // 싱글던전
                PhotonNetwork.Disconnect();
                SceneManager.LoadScene("Dungeon_1"); // 테스트용 바꿔야함
                break;
            case 2: // 카오스던전
                PhotonNetwork.Disconnect();
                SceneManager.LoadScene("Dungeon_2"); // 테스트용 바꿔야함
                break;
            case 3:
                PhotonNetwork.LeaveRoom(); // 마을 룸을 떠납니다.
                StartCoroutine(LoadLoadingScene());
                break;
            default:
                break;
        }
    }

    IEnumerator LoadLoadingScene()
    {
        yield return new WaitForSeconds(1.0f); // 로딩 딜레이

        SceneManager.LoadScene("DungeonLoadingScene"); // 로딩 씬으로 전환
    }

    // 여기에 콜백함수 없어도될듯
    #region 포톤 콜백 함수
    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        Debug.Log("방떠나기 완료");
        Debug.Log("Lobby에 입장 시도합니다");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
    }
    #endregion
}
