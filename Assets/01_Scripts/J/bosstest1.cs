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
        //통신을 보내는 
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }

        //클론이 통신을 받는 
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
        // 보스 몬스터를 생성하고 모든 클라이언트에 동기화
        GameObject bossMonster = PhotonNetwork.Instantiate(boss1.name, transform.position, transform.rotation);
        //bossMonster.GetComponent<RaidBossCtrl>().Starts();
        photonView.RPC("SyncBossMonsterSpawn", RpcTarget.AllBuffered, bossMonster.GetComponent<PhotonView>().ViewID);
    }

    // RPC를 통해 모든 클라이언트에게 보스 몬스터 생성을 알림
    [PunRPC]
    void SyncBossMonsterSpawn(int bossMonsterViewID)
    {
        PhotonView bossMonsterView = PhotonView.Find(bossMonsterViewID);
        if (bossMonsterView != null)
        {
            GameObject bossMonster = bossMonsterView.gameObject;
            // 여기에서 보스 몬스터 등장과 관련된 초기화 및 동작 설정 수행
        }
    }
}
