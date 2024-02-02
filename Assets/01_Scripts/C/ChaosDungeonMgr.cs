using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosDungeonMgr : MonoBehaviour
{
    public GameObject ground1;
    public GameObject ground2;
    public GameObject ground3;
    public GameObject ground4;

    public GameObject[] bossPrefab;
    public Transform[] spawnPoint;

    //public GameObject bossMakeEffect;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && ground1)
            InstBoss1();
        if (other.CompareTag("Player") && ground2)
            InstBoss2();
        if (other.CompareTag("Player") && ground3)
            InstBoss3();
        if (other.CompareTag("Player") && ground4)
            InstBoss4();
    }

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
        //Instantiate(bossMakeEffect, spawnPoint[4]);
        yield return new WaitForSeconds(1.5f);
        Instantiate(bossPrefab[4], spawnPoint[4]);
    }

}
