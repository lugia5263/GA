using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testGameMgr : MonoBehaviour
{
    RaidBossCtrl raidBoss;
    Tboss tboss;
    Player player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Starts()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        raidBoss.player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        raidBoss.targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        tboss.player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        tboss.targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
}
