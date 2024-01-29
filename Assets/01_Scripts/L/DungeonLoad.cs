using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DungeonLoad : MonoBehaviourPunCallbacks
{
    public string dungeonType;

    void Start()
    {
        dungeonType = RoomEnterManager.dungeonType;
        Debug.Log("DungeonLoad.dungeonType : " + dungeonType);
        StartCoroutine(EnterDungeon());
    }

    IEnumerator EnterDungeon()
    {


        yield return null;
    }
}
