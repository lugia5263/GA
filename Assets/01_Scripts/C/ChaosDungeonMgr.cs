using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using static MageMiddleBoss;


public class ChaosDungeonMgr : MonoBehaviourPunCallbacks
{
    public DataMgrDontDestroy dataMgrDontDestroy;

    public RewardMgr rewardMgr;

    public int bossKilled;

    public int dungeonSortIdx;               //1�� �̱�, 2�� ī����, 3�� ���̵�
    public int dungeonNumIdx;                //���� �� ü�°� ���ݷ� ������.

    public bool isBattle;

    public GameObject reset;                //�������� ���µǴ� �÷��̾�
    public GameObject[] door;
    public GameObject[] bossPrefab;         //[0]�� ��ĭ
    public GameObject[] mobPrefab;          //[0]�� ��ĭ
    public Transform[] mobSpawnPoint;       //[0]�� ��ĭ
    public Transform[] spawnPoint;          //[0]�� �÷��̾� ���� ��ġ
    public GameObject spawnPointObject;

    public GameObject midBossEffect;
    public GameObject endBossEffect;
    public GameObject chaosEndClearEffect;
    public GameObject clearPanel;
    


    private void Start()
    {
            dataMgrDontDestroy = DataMgrDontDestroy.Instance;
        dungeonNumIdx = DataMgrDontDestroy.Instance.DungeonNumIdx;
    }

    #region 1���� ��ȯ
    public void InstBoss1()
    {
        StartCoroutine(MakeBoss1());
        StartCoroutine(MakeMob1());
        StartCoroutine(Door());
    }
    IEnumerator MakeBoss1()
    {
        Instantiate(midBossEffect, spawnPoint[1]); // ����Ʈ ����
        GameObject bossnem1 = Instantiate(bossPrefab[1], spawnPoint[1]); // ���� ����
        yield return new WaitForSeconds(0.5f);
        bossnem1.GetComponent<StateManager>().maxhp *= dungeonNumIdx;
        bossnem1.GetComponent<StateManager>().hp *= dungeonNumIdx;
        bossnem1.GetComponent<StateManager>().attackPower += (dungeonNumIdx * 30);
    }
    IEnumerator MakeMob1()
    {
        GameObject mob1 = Instantiate(mobPrefab[1], mobSpawnPoint[1]);
        yield return new WaitForSeconds(0.5f);
        StateManager[] stateManagers = mob1.GetComponentsInChildren<StateManager>();
        foreach (StateManager stateManager in stateManagers)
        {
            stateManager.maxhp *= dungeonNumIdx;
            stateManager.hp *= dungeonNumIdx;
            stateManager.attackPower += (dungeonNumIdx * 30);
        }

        //mob1.GetComponentInChildren<StateManager>().maxhp *= dungeonNumIdx;
        //mob1.GetComponentInChildren<StateManager>().hp *= dungeonNumIdx;
        //mob1.GetComponentInChildren<StateManager>().attackPower += (dungeonNumIdx * 30);
    }
    #endregion

    #region 2���� ��ȯ
    public void InstBoss2()
    {
        StartCoroutine(MakeBoss2());
        StartCoroutine(MakeMob2());
        StartCoroutine(Door());
    }
    IEnumerator MakeBoss2()
    {
        Instantiate(midBossEffect, spawnPoint[2]); // ����Ʈ ����
        GameObject bossnem1 = Instantiate(bossPrefab[2], spawnPoint[2]); // ���� ����
        yield return new WaitForSeconds(0.5f);
        bossnem1.GetComponent<StateManager>().maxhp *= dungeonNumIdx;
        bossnem1.GetComponent<StateManager>().hp *= dungeonNumIdx;
        bossnem1.GetComponent<StateManager>().attackPower += (dungeonNumIdx * 30);
    }
    IEnumerator MakeMob2()
    {
        GameObject mob1 = Instantiate(mobPrefab[2], mobSpawnPoint[2]);
        yield return new WaitForSeconds(0.5f);
        StateManager[] stateManagers = mob1.GetComponentsInChildren<StateManager>();
        foreach (StateManager stateManager in stateManagers)
        {
            stateManager.maxhp *= dungeonNumIdx;
            stateManager.hp *= dungeonNumIdx;
            stateManager.attackPower += (dungeonNumIdx * 30);
        }
    }
    #endregion

    #region 3���� ��ȯ
    public void InstBoss3()
    {
        StartCoroutine(MakeBoss3());
        StartCoroutine(MakeMob3());
        StartCoroutine(Door());
    }
    IEnumerator MakeBoss3()
    {
        Instantiate(midBossEffect, spawnPoint[3]); // ����Ʈ ����
        GameObject bossnem1 = Instantiate(bossPrefab[3], spawnPoint[3]); // ���� ����
        yield return new WaitForSeconds(0.5f);
        bossnem1.GetComponent<StateManager>().maxhp *= dungeonNumIdx * 3;
        bossnem1.GetComponent<StateManager>().hp *= dungeonNumIdx * 3;
        bossnem1.GetComponent<StateManager>().attackPower += (dungeonNumIdx * 30);
    }
    IEnumerator MakeMob3()
    {
        GameObject mob1 = Instantiate(mobPrefab[3], mobSpawnPoint[3]);
        yield return new WaitForSeconds(0.5f);
        StateManager[] stateManagers = mob1.GetComponentsInChildren<StateManager>();
        foreach (StateManager stateManager in stateManagers)
        {
            stateManager.maxhp *= dungeonNumIdx;
            stateManager.hp *= dungeonNumIdx;
            stateManager.attackPower += (dungeonNumIdx * 30);
        }
    }
    #endregion

    IEnumerator Door()
    {
        if (isBattle == false)
        {
            Jun_TweenRuntime[] gameObject3 = door[2].GetComponents<Jun_TweenRuntime>();
            Jun_TweenRuntime[] gameObject4 = door[3].GetComponents<Jun_TweenRuntime>();
            Jun_TweenRuntime[] gameObject5 = door[4].GetComponents<Jun_TweenRuntime>();
            Jun_TweenRuntime[] gameObject6 = door[5].GetComponents<Jun_TweenRuntime>();
            yield return new WaitForSeconds(0.5f);
            
            gameObject3[0].Play(); // ������
            gameObject4[0].Play(); // ������
            gameObject5[0].Play(); // ������
            gameObject6[0].Play(); // ������
            isBattle = true;
        }
        else
        {
            Jun_TweenRuntime[] gameObject3 = door[2].GetComponents<Jun_TweenRuntime>();
            Jun_TweenRuntime[] gameObject4 = door[3].GetComponents<Jun_TweenRuntime>();
            Jun_TweenRuntime[] gameObject5 = door[4].GetComponents<Jun_TweenRuntime>();
            Jun_TweenRuntime[] gameObject6 = door[5].GetComponents<Jun_TweenRuntime>();
            yield return new WaitForSeconds(0.5f);

            gameObject3[1].Play(); // ������ ����
            gameObject4[1].Play(); // ������ ����
            gameObject5[1].Play(); // ������ ����
            gameObject6[1].Play(); // ������ ����
            isBattle = false;
        }
    }



    public void ClearMidBoss()
    {
        StartCoroutine(Door());
    }
    public void ClearEndBoss()
    {
        rewardMgr.ShowReward();

        spawnPointObject.SetActive(false);
        Jun_TweenRuntime[] gameObject = clearPanel.GetComponents<Jun_TweenRuntime>();
        gameObject[0].Play();
    }

    public void Receive()
    {

    }
    public void ResetPlayer()
    {
        reset.transform.position = spawnPoint[0].position;
    }

    public void MoveTown()
    {
        dataMgrDontDestroy.DungeonSortIdx = 0;
        dataMgrDontDestroy.playerDie = false;

        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("DungeonLoadingScene");
    }

    
    public void Update()
    {

    }

    
  
}
