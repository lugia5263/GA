using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class SettingScript : MonoBehaviourPunCallbacks
{
    public DataMgrDontDestroy dataMgrDontDestroy;
    public PhotonView pv;
    public GameObject settingPanel;
    public bool panelActive;

    public Slider volumeSlider;

    public Image[] buttonImageList;
    FullScreenMode screenMode;

    void Start()
    {
        dataMgrDontDestroy = DataMgrDontDestroy.Instance;
        pv = GetComponent<PhotonView>();
        settingPanel.SetActive(false);
    }

    void Update()
    {
        if (pv.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!panelActive)
                {
                    settingPanel.SetActive(true);
                    panelActive = true;
                }
                else
                {
                    settingPanel.SetActive(false);
                    panelActive = false;
                }
            }
        }
    }

    public void OnClickExitBtn()
    {
        dataMgrDontDestroy.SaveData();
        if (dataMgrDontDestroy.DungeonSortIdx == 0)
        {
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene("CHSelect");
        }
        else
        {
            dataMgrDontDestroy.dungeonSortIdx = 0;
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene("DungeonLoadingScene");
        }
    }

    public void OnClickQuitBtn()
    {
        dataMgrDontDestroy.SaveData();
        Application.Quit();
    }

    public void VolumeSetting()
    {
        float volume = volumeSlider.value;
        if (pv.IsMine)
        {
            Debug.Log("Volume ����");
            AudioListener.volume = volume;
        }
    }

    #region �ػ�
    public void FullScreenBtn()
    {
        if (pv.IsMine)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            screenMode = FullScreenMode.FullScreenWindow;
            buttonImageList[3].enabled = true;
            buttonImageList[4].enabled = false;
        }
    }
    public void WindowScreenBtn()
    {
        if (pv.IsMine)
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
            screenMode = FullScreenMode.Windowed;
            buttonImageList[3].enabled = false;
            buttonImageList[4].enabled = true;
        }
    }
    public void OnClickBtn_1920x1080()
    {
        if (pv.IsMine)
        {
            buttonImageList[0].enabled = true;
            buttonImageList[1].enabled = false;
            buttonImageList[2].enabled = false;
            Screen.SetResolution(1920, 1080, screenMode);
        }
    }
    public void OnClickBtn_1600x900()
    {
        if (pv.IsMine)
        {
            buttonImageList[0].enabled = false;
            buttonImageList[1].enabled = true;
            buttonImageList[2].enabled = false;
            Screen.SetResolution(1600, 900, screenMode);
        }
    }
    public void OnClickBtn_1366x768()
    {
        if (pv.IsMine)
        {
            buttonImageList[0].enabled = false;
            buttonImageList[1].enabled = false;
            buttonImageList[2].enabled = true;
            Screen.SetResolution(1366, 768, screenMode);
        }
    }
    #endregion

    #region ���� �ݹ��Լ�
    public void MakeHome()
    {
        Debug.Log("MakeHome ����");

        // ���� �Ӽ� ����
        RoomOptions ro = new RoomOptions();
        ro.MaxPlayers = 20;     // �뿡 ������ �� �ִ� �ִ� ������ ��
        ro.IsOpen = true;       // ���� ���� ����
        ro.IsVisible = true;    // �κ񿡼� �� ��Ͽ� �����ų ����

        PhotonNetwork.CreateRoom("Room_Home", ro);
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("�� ��������");
        Debug.Log($"JoinRoom Failed {returnCode}:{message}");
        MakeHome();
    }
    public override void OnJoinedRoom()
    {
        Debug.Log($"PhotonNetwork.InRoom = {PhotonNetwork.InRoom}");
        Debug.Log($"Player Count = {PhotonNetwork.CurrentRoom.PlayerCount}");

        foreach (var player in PhotonNetwork.CurrentRoom.Players)
        {
            Debug.Log($"{player.Value.NickName} , {player.Value.ActorNumber}");
        }

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("MasterClient is LoadLevel ����");
            PhotonNetwork.LoadLevel("Town");
        }
    }
    #endregion
}
