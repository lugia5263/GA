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
    public bool mouseOnOff;

    Button exitBtn;

    private void Start()
    {
        dataMgrDontDestroy = DataMgrDontDestroy.Instance;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // 마을의 Exit 버튼을 누르면 지금까지의 정보를 저장하고 캐릭터 선택씬으로 이동.
    public void OnExitClick()
    {
        dataMgrDontDestroy.SaveData();
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("CHSelect");
    }

    public void OnButtonClick()
    {
        // 현재 플레이어의 PhotonView 가져오기
        PhotonView photonView = PhotonView.Get(this);

        // 현재 플레이어의 PhotonView가 isMine인지 확인
        if (photonView.IsMine)
        {
            // PhotonView가 isMine일 때 처리할 작업 수행
            Debug.Log("This player is the owner of the PhotonView.");
        }
        else
        {
            // PhotonView가 isMine이 아닐 때 처리할 작업 수행
            Debug.Log("This player is not the owner of the PhotonView.");
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!mouseOnOff)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                mouseOnOff = true;
            }
            else
           {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                mouseOnOff = false;
            }
        }
    }
}
        

