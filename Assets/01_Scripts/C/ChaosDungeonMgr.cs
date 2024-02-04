using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosDungeonMgr : MonoBehaviour
{
    public GameObject obstacle;
    public GameObject ground1;
    public GameObject ground2;
    public GameObject ground3;
    public GameObject ground4;

    public GameObject reset;


    public GameObject[] bossPrefab; //bossPrefab[0]Àº ºóÄ­
    public Transform[] spawnPoint;

    public GameObject midBossEffect;
    public GameObject endBossEffect;

    public void Start()
    {
        obstacle.SetActive(false);
    }

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
        Instantiate(midBossEffect, spawnPoint[1]);
        Instantiate(bossPrefab[1], spawnPoint[1]);
        obstacle.SetActive(true);
    }
    public void InstBoss2()
    {
        Instantiate(midBossEffect, spawnPoint[2]);
        Instantiate(bossPrefab[2], spawnPoint[2]);
        obstacle.SetActive(true);
    }
    public void InstBoss3()
    {
        Instantiate(midBossEffect, spawnPoint[3]);
        Instantiate(bossPrefab[3], spawnPoint[3]);
        obstacle.SetActive(true);
    }
    public void InstBoss4()
    {
        StartCoroutine(InstChaosBoss());
        obstacle.SetActive(true);
    }

    IEnumerator InstChaosBoss()
    {
        Instantiate(endBossEffect, spawnPoint[4]);
        yield return new WaitForSeconds(1.5f);
        Instantiate(bossPrefab[4], spawnPoint[4]);
    }

    public void ClearMidBoss()
    {
        obstacle.SetActive(false);
    }

    public void ClearEndBoss()
    {
        //clearPanel.SetActive(true);
    }

    public void Update()
    {
        if (reset.transform.position.y < -7f)
            ResetPlayer();

    }

    public void ResetPlayer()
    {
        reset.transform.position = spawnPoint[0].position;
    }

}
