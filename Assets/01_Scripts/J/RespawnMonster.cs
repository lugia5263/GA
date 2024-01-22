using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnMonster : MonoBehaviour
{
    public bool enableSpawn = false;
    public GameObject spawnFloor;
    public BoxCollider floorRange;
    public GameObject[] enemyPool;

    public int poolSize = 30;
    public float curTime;
    public float spawnTime = 15f;
    int spawnCut = 1;
    int maxSpawnCut;

    public GameObject monster;
    

    public GameObject[] floor;
    public GameObject[] enemies;

    public Vector3 originPos;
    private void Awake()
    {
        spawnFloor = this.gameObject;
        floorRange = spawnFloor.GetComponentInChildren<BoxCollider>();
    }
    void Start()
    {
        enemyPool = new GameObject[poolSize];
        for (int i = 0; i < enemyPool.Length; i++)
        {
            enemyPool[i] = Instantiate(monster);
            enemyPool[i].SetActive(false);
           
        }
        
        //InvokeRepeating("randomSpawn", 10, 20);
    }

    Vector3 RandomSpawnPosition()
    {
        Vector3 originPos = spawnFloor.transform.position;

        float rangeX = floorRange.bounds.size.x;
        float rangeZ = floorRange.bounds.size.z;

        rangeX = Random.Range((rangeX / 4) * -1, rangeX / 4);
        rangeZ = Random.Range((rangeZ / 4) * -1, rangeZ / 4);
        Vector3 RandomPos = new Vector3(rangeX, 0, rangeZ);
        Vector3 SpawnPos = originPos + RandomPos;
        return SpawnPos;
    }
    void randomSpawn()
    {
        if (enableSpawn)
        {

            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length < 100)
            {
                //Vector3 rot = new Vector3(0, Random.Range(-10, 10), 0);
                GameObject enemy = (GameObject)Instantiate(monster, RandomSpawnPosition(), Quaternion.identity);
               
            }

        }
    }

    void Update()
    {
        curTime += Time.deltaTime;
        if (curTime > spawnTime)
        {
            curTime = 0;
            for (int i = 0; i < enemyPool.Length; i++)
            {
                if (enemyPool[i].activeSelf == true)
                    continue;

                originPos = transform.position;
                float x = originPos.x -= Random.Range(-4f, 4f);
                float z = originPos.z -= Random.Range(-4f, 4f);
                enemyPool[i].transform.position = new Vector3(x, 0, z);
                enemyPool[i].SetActive(true);
                enemyPool[i].name = "ENEMY_" + spawnCut;
                ++spawnCut;
            }
        }

    }
}
