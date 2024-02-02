using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine.SceneManagement;

public class RoomEnterManager : MonoBehaviourPunCallbacks
{
    public GameObject dungeonPanel;

    public static string dungeonType;

    //�ַδ����� roomleave�ϰ� ���Ŵ����� ������
    public void OnSoloDungeon1ButtonClick()
    {
        dungeonType = "singleDungeon";
        Debug.Log("������ Ÿ�� : " + dungeonType);
        LeaveVillige();
    }

    public void OnSoloDungeon2ButtonClick()
    {
        dungeonType = "chaosDungeon";
        Debug.Log("������ Ÿ�� : " + dungeonType);
        LeaveVillige();
    }

    public void OnRaidDungeonButtonClick()
    {
        dungeonType = "raidDungeon";
        Debug.Log("������ Ÿ�� : " + dungeonType);
        LeaveVillige();
    }

    public void LeaveVillige()
    {
        PhotonNetwork.LeaveRoom(); // ���� ���� �����ϴ�.
        StartCoroutine(LoadLoadingScene());
    }

    IEnumerator LoadLoadingScene()
    {
        yield return new WaitForSeconds(1.0f); // �ε� ������

        SceneManager.LoadScene("DungeonLoadingScene"); // �ε� ������ ��ȯ
    }

    // ���⿡ �ݹ��Լ� ����ɵ�
    #region ���� �ݹ� �Լ�
    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        Debug.Log("�涰���� �Ϸ�");
        Debug.Log("Lobby�� ���� �õ��մϴ�");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        Debug.Log("Lobby�� ���� �Ϸ�");
        SceneManager.LoadScene("DungeonLoadingScene");
    }
    //public override void OnJoinedRoom()
    //{
    //    // ���� ���� ���� Ÿ���� ������
    //    if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("DungeonType", out object dungeonTypeObj))
    //    {
    //        string dungeonType = dungeonTypeObj.ToString();
    //        Debug.Log("������ �̸��� : " + dungeonType);
    //        // ������ ���
    //        if (PhotonNetwork.IsMasterClient)
    //        {
    //            // �ش� ���� Ÿ�Կ� �´� ���� �ε�
    //            PhotonNetwork.LoadLevel(dungeonType);
    //        }
    //    }
    //}

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
    }
    #endregion

    // �������� UI panelŰ�� ��ư������ �Լ�
    public void OnClickEnterDungeonBtn()
    {
        dungeonPanel.SetActive(true);
    }
    //�������� UI panel���� ��ư������ �Լ�
    public void OnClickCancelBtn()
    {
        dungeonPanel.SetActive(false);
    }
}
