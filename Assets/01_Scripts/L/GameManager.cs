using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviourPunCallbacks
{
    public DataMgrDontDestroy dataMgrDontDestroy;

    public Button exitBtn;

    private void Start()
    {
        dataMgrDontDestroy = DataMgrDontDestroy.Instance;
    }

    // 마을의 Exit 버튼을 누르면 지금까지의 정보를 저장하고 캐릭터 선택씬으로 이동.
    public void OnExitClick()
    {
        dataMgrDontDestroy.SaveDate();
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("CHSelect");
    }
}
