using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using Cinemachine;

public class bosstest2 : MonoBehaviour
{
    private Vector3 currPos;
    private Quaternion currRot;
    public GameObject boss2;
    PhotonView pv;
    void Start()
    {
        if (pv.IsMine)
        {
            StartCoroutine(bossneeds());
        }
    }


    IEnumerator bossneeds()
    {
        yield return new WaitForSeconds(10f);
        Instantiate(boss2, transform.position, transform.rotation);
        Destroy(gameObject, 3f);
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
    
}
