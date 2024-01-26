using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SpawnScipt : MonoBehaviourPunCallbacks
{
    public GameObject[] characterPrefabs;
    readonly int curSlotNum = SelectSlot.slotNum;

    private void Start()
    {
        Debug.Log(curSlotNum);
        CreatePlayer();
    }
    public void CreatePlayer()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        
        if (PhotonNetwork.IsConnected)
        {
            Transform[] points = GameObject.Find("SpawnPointGroup").GetComponentsInChildren<Transform>();
            int idx = Random.Range(1, points.Length);

            //PhotonNetwork.Instantiate(characterPrefabs[(int)DataMgr.instance.currentCharacter].name, points[idx].position, points[idx].rotation, 0);
            PhotonNetwork.Instantiate(characterPrefabs[curSlotNum].name, points[idx].position, points[idx].rotation, 0);
        }
    }
}
