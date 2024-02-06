using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaidBossSpwan : MonoBehaviour
{
    public GameObject raidBoss;
    public Transform bossSpwanPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Instantiate(raidBoss, bossSpwanPos.position, bossSpwanPos.rotation);
            Destroy(gameObject);
        }
    }
}
