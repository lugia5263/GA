using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class RoomEnterManager : MonoBehaviourPunCallbacks
{
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

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
    }
    #endregion
}
