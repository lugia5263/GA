using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaidDungeonMgr : MonoBehaviour
{
    public Transform[] spawnPoint;
    public GameObject bossPrefab;
    public GameObject bossMakeEffect;
    public GameObject bossPhaseEffect;

    public GameObject ground_1f;


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(bossPrefab, spawnPoint[0]);
        }
    }

    public void BossPhase()
    {
        StartCoroutine(AngryBoss());
    }

    IEnumerator AngryBoss()
    {
        Destroy(ground_1f);
        yield return new WaitForSeconds(1.5f);
        Instantiate(bossPhaseEffect, spawnPoint[1]);
    }

    public void Panel()
    {

    }
}
