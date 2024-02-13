using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using Cinemachine;

public class bosstest1 : MonoBehaviourPunCallbacks
{
    private Vector3 currPos;
    private Quaternion currRot;
    public GameObject boss1;
    PhotonView pv;
    RaidBossCtrl raidBoss;
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(bossneed());
        }
    }


    IEnumerator bossneed()
    {
        yield return new WaitForSeconds(5f);
        SpawnBossMonster();
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //����� ������ 
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }

        //Ŭ���� ����� �޴� 
        else
        {
            currPos = (Vector3)stream.ReceiveNext();
            currRot = (Quaternion)stream.ReceiveNext();
        }
    }
    public void Starts()
    {
        raidBoss.player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        raidBoss.targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void SpawnBossMonster()
    {
        // ���� ���͸� �����ϰ� ��� Ŭ���̾�Ʈ�� ����ȭ
        GameObject bossMonster = PhotonNetwork.Instantiate(boss1.name, transform.position, transform.rotation);
        //bossMonster.GetComponent<RaidBossCtrl>().Starts();
        photonView.RPC("SyncBossMonsterSpawn", RpcTarget.AllBuffered, bossMonster.GetComponent<PhotonView>().ViewID);
    }

    // RPC�� ���� ��� Ŭ���̾�Ʈ���� ���� ���� ������ �˸�
    [PunRPC]
    void SyncBossMonsterSpawn(int bossMonsterViewID)
    {
        PhotonView bossMonsterView = PhotonView.Find(bossMonsterViewID);
        if (bossMonsterView != null)
        {
            GameObject bossMonster = bossMonsterView.gameObject;
            // ���⿡�� ���� ���� ����� ���õ� �ʱ�ȭ �� ���� ���� ����
        }
    }
}
