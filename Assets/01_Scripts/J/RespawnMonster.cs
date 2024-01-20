using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnMonster : MonoBehaviour
{
    public bool enableSpawn = false;
    public GameObject spawnFloor;
    public BoxCollider floorRange;
    public GameObject monster;
    public GameObject monster1;

    public GameObject[] floor;
    public GameObject[] enemies;
    private void Awake()
    {
        spawnFloor = this.gameObject;
        floorRange = spawnFloor.GetComponentInChildren<BoxCollider>();
        
    }
    void Start()
    {
        InvokeRepeating("randomSpawn", 1, 20);
    }

    Vector3 RandomSpawnPosition()
    {
        Vector3 originPos = spawnFloor.transform.position;

        floor = GameObject.FindGameObjectsWithTag("Ground");

        float rangeX = floorRange.bounds.size.x;
        float rangeZ = floorRange.bounds.size.z;

        rangeX = Random.Range((rangeX/2) * -1, rangeX/2);
        rangeZ = Random.Range((rangeZ / 2) * -1, rangeZ / 2);
        Vector3 RandomPos = new Vector3(rangeX,0, rangeZ);
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
                Vector3 rot = new Vector3(0, Random.Range(-10, 10), 0);
                GameObject enemy = (GameObject)Instantiate(monster, RandomSpawnPosition(), Quaternion.Euler(rot));
                GameObject enemy1 = (GameObject)Instantiate(monster1, RandomSpawnPosition(), Quaternion.Euler(rot));
            }

        }
    }
    
}
