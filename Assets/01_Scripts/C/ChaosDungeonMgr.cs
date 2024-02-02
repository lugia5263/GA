using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosDungeonMgr : MonoBehaviour
{
    public Transform[] spawnPoint;
    public GameObject[] bossPrefab;
    public GameObject bossMakeEffect;
    public void InstBoss1()
    {
        Instantiate(bossPrefab[1], spawnPoint[1]);
    }
    public void InstBoss2()
    {
        Instantiate(bossPrefab[2], spawnPoint[2]);
    }
    public void InstBoss3()
    {
        Instantiate(bossPrefab[3], spawnPoint[3]);
    }
    public void InstBoss4()
    {
        StartCoroutine(MakeChaosBoss());
    }

    IEnumerator MakeChaosBoss()
    {
        Instantiate(bossMakeEffect, spawnPoint[4]);
        yield return new WaitForSeconds(1.5f);
        Instantiate(bossPrefab[4], spawnPoint[4]);

    }

}
