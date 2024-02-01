using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviourPunCallbacks
{
    public SpawnScipt spawnMgr;

    public Button exitBtn;

    void Awake()
    {
        spawnMgr = GameObject.Find("SpawnMgr").GetComponent<SpawnScipt>();
    }

    void Start()
    {
        spawnMgr.StartCoroutine(spawnMgr.SpwanPlayer());
    }

    // Exit 버튼의 OnClick에 연결할 함수
    public void OnExitClick()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("LoginIntro");
    }

    #region 포톤 콜백함수
    // 포톤 룸에서 퇴장했을 때 호출되는 콜백함수
    public override void OnLeftRoom()
    {
        Debug.Log("방 나가기 완료.");
    }

    // 룸으로 새로운 네트워크 유저가 입장했때 호출되는 콜백함수
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)  
    {
        string msg = $"\n<color=#00ff00>{newPlayer.NickName}</color> is joined room";
    }

    // 룸에서 네트워크 유저가 퇴장했때 호출되는 콜백함수
    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        string msg = $"\n<color=#ff0000>{otherPlayer.NickName}</color> is left room";
    }
    #endregion
}
