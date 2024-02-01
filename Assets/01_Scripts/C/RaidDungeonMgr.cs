using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaidDungeonMgr : MonoBehaviour
{
    public Transform[] spawnPoint;
    public GameObject[] bossPrefab;
    public GameObject bossMakeEffect;

    public GameObject ground_1f;
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
        Debug.Log("ojnionio");
    }

    IEnumerator MakeChaosBoss()
    {
        Destroy(ground_1f);
        yield return new WaitForSeconds(1.5f);
        Instantiate(bossMakeEffect, spawnPoint[0]);
        yield return new WaitForSeconds(1.5f);
        Instantiate(bossPrefab[1], spawnPoint[0]);


    }




}
