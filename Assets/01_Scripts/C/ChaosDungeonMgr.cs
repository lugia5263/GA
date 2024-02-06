using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static MageMiddleBoss;


public class ChaosDungeonMgr : MonoBehaviour
{
    public DataMgrDontDestroy dataMgr;

    public int bossKilled;

    public int cDungeonStep;                //초기 던전 난이도

    public bool isBattle;

    public GameObject reset;                //떨어지면 리셋되는 플레이어
    public GameObject[] door;
    public GameObject[] bossPrefab;         //[0]은 빈칸
    public GameObject[] mobPrefab;          //[0]은 빈칸
    public Transform[] mobSpawnPoint;       //[0]은 빈칸
    public Transform[] spawnPoint;          //[0]은 플레이어 리셋 위치

    public GameObject midBossEffect;
    public GameObject endBossEffect;



    private void Start()
    {
        if(dataMgr != null)
        {
            cDungeonStep = DataMgrDontDestroy.Instance.DungeonNumIdx;
        }
    }

    #region 1보스 소환
    public void InstBoss1()
    {
        StartCoroutine(MakeBoss1());
        StartCoroutine(MakeMob1());
        StartCoroutine(Door());
    }
    IEnumerator MakeBoss1()
    {
        Instantiate(midBossEffect, spawnPoint[1]); // 이펙트 생성
        GameObject bossnem1 = Instantiate(bossPrefab[1], spawnPoint[1]); // 보스 생성
        yield return new WaitForSeconds(0.5f);
        bossnem1.GetComponent<StateManager>().maxhp *= cDungeonStep;
        bossnem1.GetComponent<StateManager>().hp *= cDungeonStep;
        bossnem1.GetComponent<StateManager>().attackPower += (cDungeonStep * 30);
    }
    IEnumerator MakeMob1()
    {
        GameObject mob1 = Instantiate(mobPrefab[1], mobSpawnPoint[1]);
        yield return new WaitForSeconds(0.5f);
        mob1.GetComponentInChildren<StateManager>().maxhp *= cDungeonStep;
        mob1.GetComponentInChildren<StateManager>().hp *= cDungeonStep;
        mob1.GetComponentInChildren<StateManager>().attackPower += (cDungeonStep * 30);
    }
    #endregion

    #region 2보스 소환
    public void InstBoss2()
    {
        StartCoroutine(MakeBoss2());
        StartCoroutine(MakeMob2());
        StartCoroutine(Door());
    }
    IEnumerator MakeBoss2()
    {
        Instantiate(midBossEffect, spawnPoint[2]); // 이펙트 생성
        GameObject bossnem1 = Instantiate(bossPrefab[2], spawnPoint[2]); // 보스 생성
        yield return new WaitForSeconds(0.5f);
        bossnem1.GetComponent<StateManager>().maxhp *= cDungeonStep;
        bossnem1.GetComponent<StateManager>().hp *= cDungeonStep;
        bossnem1.GetComponent<StateManager>().attackPower += (cDungeonStep * 30);
    }
    IEnumerator MakeMob2()
    {
        GameObject mob1 = Instantiate(mobPrefab[2], mobSpawnPoint[2]);
        yield return new WaitForSeconds(0.5f);
        mob1.GetComponentInChildren<StateManager>().maxhp *= cDungeonStep;
        mob1.GetComponentInChildren<StateManager>().hp *= cDungeonStep;
        mob1.GetComponentInChildren<StateManager>().attackPower += (cDungeonStep * 30);
    }
    #endregion

    #region 3보스 소환
    public void InstBoss3()
    {
        StartCoroutine(MakeBoss3());
        StartCoroutine(MakeMob3());
        StartCoroutine(Door());
    }
    IEnumerator MakeBoss3()
    {
        Instantiate(midBossEffect, spawnPoint[3]); // 이펙트 생성
        GameObject bossnem1 = Instantiate(bossPrefab[3], spawnPoint[3]); // 보스 생성
        yield return new WaitForSeconds(0.5f);
        bossnem1.GetComponent<StateManager>().maxhp *= cDungeonStep *3;
        bossnem1.GetComponent<StateManager>().hp *= cDungeonStep *3;
        bossnem1.GetComponent<StateManager>().attackPower += (cDungeonStep * 30);
    }
    IEnumerator MakeMob3()
    {
        GameObject mob1 = Instantiate(mobPrefab[3], mobSpawnPoint[3]);
        yield return new WaitForSeconds(0.5f);
        mob1.GetComponentInChildren<StateManager>().maxhp *= cDungeonStep;
        mob1.GetComponentInChildren<StateManager>().hp *= cDungeonStep;
        mob1.GetComponentInChildren<StateManager>().attackPower += (cDungeonStep * 30);
    }
    #endregion

    IEnumerator Door()
    {
        if (isBattle == false)
        {
            Jun_TweenRuntime[] gameObject1 = door[2].GetComponents<Jun_TweenRuntime>();
            Jun_TweenRuntime[] gameObject2 = door[3].GetComponents<Jun_TweenRuntime>();
            Jun_TweenRuntime[] gameObject3 = door[4].GetComponents<Jun_TweenRuntime>();
            Jun_TweenRuntime[] gameObject4 = door[5].GetComponents<Jun_TweenRuntime>();
            yield return new WaitForSeconds(0.5f);
            gameObject1[0].Play(); // 닫히기
            gameObject2[0].Play(); // 닫히기
            gameObject3[0].Play(); // 닫히기
            gameObject4[0].Play(); // 닫히기
            isBattle = true;
        }
        else
        {
            Jun_TweenRuntime[] gameObject1 = door[2].GetComponents<Jun_TweenRuntime>();
            Jun_TweenRuntime[] gameObject2 = door[3].GetComponents<Jun_TweenRuntime>();
            Jun_TweenRuntime[] gameObject3 = door[4].GetComponents<Jun_TweenRuntime>();
            Jun_TweenRuntime[] gameObject4 = door[5].GetComponents<Jun_TweenRuntime>();
            yield return new WaitForSeconds(0.5f);
            gameObject1[1].Play(); // 열려라 참깨
            gameObject2[1].Play(); // 열려라 참깨
            gameObject3[1].Play(); // 열려라 참깨
            gameObject4[1].Play(); // 열려라 참깨
            isBattle = false;
        }
    }



    public void ClearBoss1()
    {
        StartCoroutine(Door());
        bossKilled++;
    }

    public void ClearBoss2()
    {
        StartCoroutine(Door());
        bossKilled++;
    }

    public void ClearEndBoss()
    {
        //clearPanel.SetActive(true);
    }

    public void Update()
    {
        //if (reset.transform.position.y < -7f)
        //    ResetPlayer();

        if (bossPrefab[1].GetComponent<MageMiddleBoss>().die)
        {
            ClearBoss1();
        }
        if (bossPrefab[2].GetComponent<Tboss>().die)
        {
            ClearBoss2();
        }
        if (bossPrefab[3].GetComponent<NomalMonsterCtrl>().golemisDeath)
        {
            ClearEndBoss();
        }
    }

    public void ResetPlayer()
    {
        reset.transform.position = spawnPoint[0].position;
    }

}
