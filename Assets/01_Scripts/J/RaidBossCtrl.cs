using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using Cinemachine;

public class RaidBossCtrl : MonoBehaviourPunCallbacks,IPunObservable
{
    public enum RAIDBOSS
    {
        IDLE = 0,
        MOVE,
        ATTACK,
        BREAK,
        DOWN,
        DIE,
        PAGE1,
        PAGE2,
        PAGE3,
    }
    float gravity = -9.8f;
    public float detectionRadius = 10f;
    public string playerTag = "Player";
    private Transform currentTarget;
    private Vector3 currPos;
    private Quaternion currRot;
    public BoxCollider neM1area;
    public GameObject[] prefabs;
    public GameObject nem2Area;
    public GameObject safeZoneBall;
    public float patternTime;
    public float nem2PatternTime;
    [Header("Move")]
    public float speed = 2.5f;
    public float rotSpeed = 5f;
    public float range = 8f;
    public float attakRange = 3f;
    public Transform targetPlayer;
    public RAIDBOSS raidBoss;
    public bool isActivating;

    [Header("Component")]
    public CharacterController characterController;
    public Animator anim;
    public StateManager stateManager;
    public VisualEffect healthUp;
    public Player player;
    public PhotonView pv;
    public PhotonAnimatorView pav;
    testGameMgr testgameMgr;
    public Slider hud;
    [Header("AttackPattern")]
    public float p1;
    public float p2;
    public float p3;
    public float p4;
    public float p5;
    public float dieNowPattern;
    public float healthUpingTime;
    public float healUseingTime;
    public float p1check;
    public float p2check;
    public float p3check;
    public float p4check;
    public float p5check;
    public float dieNowPatternCheck;
    public float healthUpingTimeCheck;
    public float healUseingTimeCheck;

    public bool p1Ready;
    public bool p2Ready;
    public bool p3Ready;
    public bool p4Ready;
    public bool p5Ready;
    public bool down;
    public bool attacking;
    public GameObject dieNowPatternEffect;
    public GameObject DownPattern;
    public float breakTime;
    public float breakCheck;
    public GameObject[] allDownPattern;
    private List<GameObject> gameObjects = new List<GameObject>();
    public GameObject[] nearbyPlayer;
    public BoxCollider allDownArea;
    public float page1;
    public float page1check;
    public bool breakOn;
    public bool die;
    public bool healthUpCheck;

    [Header("Ettect")]
    public BoxCollider weapons;
    public GameObject p5PatternE;
    public GameObject downPatterE;
    public GameObject p2EttectE;
    public GameObject p1EttectE;
    public GameObject healEttetE;
    public RaidGroundOner groundOner;
    void Start()
    {
        pv = GetComponent<PhotonView>();
        if (PhotonNetwork.IsConnected)
        {
            raidBoss = RAIDBOSS.IDLE;
            characterController = GetComponent<CharacterController>();
            anim = GetComponent<Animator>();
            stateManager = GetComponent<StateManager>();
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            pav = GetComponent<PhotonAnimatorView>();
        }
    }
  
    void FixedUpdate()
    {
        if (PhotonNetwork.IsConnected)
        {
            pv.TransferOwnership(PhotonNetwork.LocalPlayer);
            if (!die)
            {
                patternTime += Time.deltaTime;
                nem2PatternTime += Time.deltaTime;
                NearByPlayer();
                BreakTime();
                PatternTimeCheck();
                DieNowPatternt();
                NEM1();
                NemStart();
                Dieing();
                healthUpPattern();
                GroundOner();
                Nem2Start();


                switch (raidBoss)
                {
                    case RAIDBOSS.IDLE:
                        isActivating = false;
                        anim.SetTrigger("IDLE");
                        float dist = Vector3.Distance(targetPlayer.position, transform.position);
                        if (dist < range)
                        {
                            raidBoss = RAIDBOSS.MOVE;
                        }
                        else
                        {
                            raidBoss = RAIDBOSS.IDLE;
                        }
                        break;
                    case RAIDBOSS.MOVE:
                        StartCoroutine(MoveDelay());
                        if (down)
                            return;
                        if (attacking)
                            return;
                        float dis = Vector3.Distance(targetPlayer.position, transform.position);
                        if (dis > 1f)
                        {
                            isActivating = false;
                        }
                        if (dis < attakRange)
                        {
                            raidBoss = RAIDBOSS.ATTACK;
                        }
                        speed = 3f;
                        anim.SetTrigger("RUN");
                        MoveTowardsTarget(true);
                        float distan = Vector3.Distance(targetPlayer.position, transform.position);
                        if (distan > 18)
                        {
                            raidBoss = RAIDBOSS.IDLE;
                        }
                        break;
                    case RAIDBOSS.ATTACK:
                        isActivating = true;
                        speed = 0f;
                        float dists = Vector3.Distance(targetPlayer.position, transform.position);
                        if (p1Ready)
                        {
                            if (dists < attakRange)
                            {
                                anim.SetTrigger("Pattern1");
                                p1 = 0;
                                p1Ready = false;
                                attacking = true;
                            }
                        }
                        if (p2Ready)
                        {
                            if (dists < attakRange)
                            {
                                anim.SetTrigger("Pattern2");
                                p2 = 0;
                                p2Ready = false;
                                attacking = true;
                            }
                        }
                        if (p3Ready)
                        {
                            if (dists < attakRange)
                            {
                                anim.SetTrigger("Pattern3");
                                p3 = 0;
                                p3Ready = false;
                                attacking = true;
                            }
                        }
                        if (p4Ready)
                        {
                            if (dists < attakRange)
                            {
                                anim.SetTrigger("Pattern4");
                                p4 = 0;
                                p4Ready = false;
                                attacking = true;
                            }
                        }
                        if (p5Ready)
                        {
                            if (dists < attakRange)
                            {
                                anim.SetTrigger("Pattern5");
                                p5 = 0;
                                p5Ready = false;
                                attacking = true;
                            }
                        }
                        if (dists > attakRange)
                        {
                            raidBoss = RAIDBOSS.MOVE;
                        }
                        else
                        {
                            raidBoss = RAIDBOSS.ATTACK;
                        }
                        break;
                    case RAIDBOSS.BREAK:
                        //isActivating = true;
                        //speed = 0f;
                        //breakTime = 0f;
                        //StartCoroutine(breakTiming());
                        break;
                    case RAIDBOSS.DOWN:
                        isActivating = true;
                        down = true;
                        speed = 0f;
                        float dista = Vector3.Distance(targetPlayer.position, transform.position);
                        anim.SetTrigger("Down");
                        if (dista > attakRange)
                        {
                            raidBoss = RAIDBOSS.MOVE;
                        }
                        else
                        {
                            raidBoss = RAIDBOSS.ATTACK;
                        }
                        break;
                    case RAIDBOSS.DIE:

                        break;
                    case RAIDBOSS.PAGE1:

                        break;
                }
            }
        }
    }

    void HUDctrl()
    {
        
    }
    void NearByPlayer()
    {
        nearbyPlayer = GameObject.FindGameObjectsWithTag("Player");
        targetPlayer = GetNearestPlayer(nearbyPlayer);
    }
    Transform GetNearestPlayer(GameObject[] players)
    {
        Transform nearestPlayer = null;
        float shortestDistance = float.MaxValue;

        foreach (var player in players)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer < shortestDistance)
            {
                shortestDistance = distanceToPlayer;
                nearestPlayer = player.transform;
            }
        }

        return nearestPlayer;
    }
    [PunRPC]
   public void GroundOner()
    {
        if(stateManager.hp <= stateManager.maxhp / 2 )
        {
            bool crush = true;
            if(crush)
            {
                pv.RPC("GroundCrushAllClient", RpcTarget.All);
                crush = false;
            }

        }
    }
    [PunRPC]
    void GroundCrushAllClient()
    {
        bool crush = true;
        if(crush)
        {
            groundOner.CrushOner();
            crush = false;
            if(groundOner.sound)
            {
                groundOner.sounds();
                groundOner.sound = false;
            }
        }
        
    }

  
    [PunRPC]
    void MoveTowardsTarget(bool stop)
    {
        if (isActivating)
            return;
        Vector3 directionToTarget = targetPlayer.transform.position - transform.position;
        directionToTarget.y = 0;
        Quaternion toRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotSpeed * Time.deltaTime);

        Vector3 moveDirection = directionToTarget.normalized;
        characterController.SimpleMove(moveDirection * speed);

        if (!characterController.isGrounded)
        {
            Vector3 gravityVector = Vector3.down * -gravity * Time.deltaTime;
            characterController.Move(gravityVector);
        }

        if (Vector3.Distance(transform.position, targetPlayer.transform.position) < 1.5f)
        {
            raidBoss = RAIDBOSS.ATTACK;
        }
    }
    IEnumerator MoveDelay()
    {
        yield return new WaitForSeconds(4.5f);
        down = false;
        yield return new WaitForSeconds(3f);
        attacking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(breakOn)
        {
            if(other.CompareTag("DownSkill"))
            {
                pv.RPC("DownAllClient", RpcTarget.All);
            }
        }
    }
    [PunRPC]
    void DownAllClient()
    {
        raidBoss = RAIDBOSS.DOWN;
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("TimeSlow"))
        {
            anim.speed = 0.15f;
            speed = 0.3f;
        }
        else
        {
            speed = 3f;
            anim.speed = 1f;
        }
    }
    [PunRPC]
    void PatternTimeCheck()
    {
        p1 += Time.deltaTime;
        if(p1 >= p1check)
        {
            p1 = p1check;
            p1Ready = true;
        }
        p2 += Time.deltaTime;
        if (p2 >= p2check)
        {
            p2 = p2check;
            p2Ready = true;
        }
        p3 += Time.deltaTime;
        if (p3 >= p3check)
        {
            p3 = p3check;
            p3Ready = true;
        }
        p4 += Time.deltaTime;
        if (p4 >= p4check)
        {
            p4 = p4check;
            p4Ready = true;
        }
        p5 += Time.deltaTime;
        if (p5 >= p5check)
        {
            p5 = p5check;
            p5Ready = true;
        }
    }

    [PunRPC]
    void BreakTime()
    {
        breakTime += Time.deltaTime;
        if(breakTime >= breakCheck)
        {
            pv.RPC("OnBreakAllClient", RpcTarget.All);
            breakOn = true;
            breakTime = breakCheck;
            isActivating = true;
            speed = 0f;
            breakTime = 0f;
            StartCoroutine(breakTiming());
        }
    }

    [PunRPC]
    void OnBreakAllClient()
    {
        anim.SetTrigger("Break");
    }

    [PunRPC]
    IEnumerator breakTiming()
    {
        DownPattern.SetActive(true);
        yield return new WaitForSeconds(2f);
        DownPattern.SetActive(false);
        yield return new WaitForSeconds(3f);
        float dista = Vector3.Distance(targetPlayer.position, transform.position);
        if (dista > attakRange)
        {
            raidBoss = RAIDBOSS.MOVE;
        }
        else
        {
            raidBoss = RAIDBOSS.ATTACK;
        }
        breakOn = false;
    }
    [PunRPC]
    void Dieing()
    {
        if(stateManager.hp <= 0)
        {
            pv.RPC("DieAllClient", RpcTarget.All);
        }
    }
    [PunRPC]
    void DieAllClient()
    {
        anim.SetTrigger("Die");
        raidBoss = RAIDBOSS.DIE;
        weapons.enabled = false;
        characterController.enabled = false;
        isActivating = true;
        speed = 0;
        die = true;
    }

    [PunRPC]
    void DieNowPatternt()
    {
        dieNowPattern += Time.deltaTime;
        if(dieNowPattern >= dieNowPatternCheck)
        {
            pv.RPC("DieNowAllClient", RpcTarget.All);
        }
    }
    [PunRPC]
    void DieNowAllClient()
    {
        Vector3 Pos = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z - 1f);
        GameObject obj;
        obj = Instantiate(dieNowPatternEffect, Pos, transform.rotation);
        Destroy(obj, 3.8f);
        dieNowPattern = 0;
        anim.SetTrigger("Rolling");
    }

    [PunRPC]
    void healthUpPattern()
    {
        healthUpingTime += Time.deltaTime;
        if (healthUpingTime >= healthUpingTimeCheck)
        {
            pv.RPC("HealthUPAllClient", RpcTarget.All);
        }
    }
    [PunRPC]
    void HealthUPAllClient()
    {
        anim.SetTrigger("HealthUp");
        healthUpCheck = true;
        if (healthUpCheck)
        {
            InvokeRepeating("EffectInvoke", 0, 2);
            stateManager.hp += 1000f;
            stateManager.attackPower += 50;
            healthUpingTime = 0;
            StartCoroutine(EffectDelay());
        } 
    }
    [PunRPC]
    void NEM1()
    {
        page1 += Time.deltaTime;
        if (page1 >= page1check)
        {
            pv.RPC("NemAllClient", RpcTarget.All);
        }
    }
    [PunRPC]
    void NemAllClient()
    {
        anim.SetTrigger("PAGE1");
        isActivating = true;
        InvokeRepeating("Spawn", 0.01f, 0.2f);
        StartCoroutine(Page1Start());
        page1 = 0;
    }
    [PunRPC]
    private void NemStart()
    {
        if (patternTime >= 60f)
        {
            Spawns();
            StartCoroutine(Nem1delay());
        }
    }
    [PunRPC]
    private void Nem2Start()
    {
        if (nem2PatternTime >= 120f)
        {
            Nem2();
            nem2PatternTime = 0;
        }
    }
    [PunRPC]
    void Spawn()
    {

        int selection = Random.Range(0, allDownPattern.Length);

        GameObject selectedPrefab = allDownPattern[selection];

        Vector3 spawnPos = GetRandomPosition();
        Quaternion spawnRot = Quaternion.Euler(0, transform.rotation.y + Random.Range(0, 360), 0);
        GameObject instance = Instantiate(selectedPrefab, spawnPos, spawnRot);
        gameObjects.Add(instance);

        raidBoss = RAIDBOSS.IDLE;
    }
    private Vector3 GetRandomPosition()
    {
        Vector3 basePosition = allDownArea.transform.position;

        Vector3 size = allDownArea.size;

        float posX = basePosition.x + Random.Range(-18, 18);
        float posY = basePosition.y + 0.1f;
        float posZ = basePosition.z + Random.Range(-18, 18);

        Vector3 spawnPos = new Vector3(posX, posY, posZ);

        return spawnPos;
    }
    IEnumerator Page1Start()
    {
        yield return new WaitForSeconds(5f);
        CancelInvoke("Spawn");
        float distans = Vector3.Distance(targetPlayer.position, transform.position);
        if (distans > attakRange)
        {
            raidBoss = RAIDBOSS.MOVE;
        }
        else
        {
            raidBoss = RAIDBOSS.ATTACK;
        }
    }


    [PunRPC]
    private Vector3 SaveBallPosition()
    {
        Vector3 basePosition = transform.position;

        Vector3 size = neM1area.size;

        float posX = basePosition.x + Random.Range(-size.x, size.x);
        float posY = basePosition.y + 1f;
        float posZ = basePosition.z + Random.Range(-size.z, size.z);

        Vector3 spawnPos = new Vector3(posX, posY, posZ);

        return spawnPos;
    }

    [PunRPC]
    void Spawns()
    {

        int selection = Random.Range(0, prefabs.Length);

        GameObject selectedPrefab = prefabs[selection];

        Vector3 spawnPos = GetRandomPosition();

        GameObject instance = Instantiate(selectedPrefab, spawnPos, Quaternion.Euler(0, 0, 0));
        gameObjects.Add(instance);

        raidBoss = RAIDBOSS.IDLE;
    }
    [PunRPC]
    void Nem2()
    {
        Vector3 spawnPos = SaveBallPosition();

        Instantiate(safeZoneBall, spawnPos, Quaternion.Euler(0, 0, 0));
        Instantiate(nem2Area, transform.position, transform.rotation);

    }
    IEnumerator Nem1delay()
    {
        yield return new WaitForSeconds(0.3f);
        patternTime = 0;
    }

    void EffectInvoke()
    {
        healthUp.Play();
    }
    IEnumerator EffectDelay()
    {
        yield return new WaitForSeconds(12f);
        stateManager.attackPower -= 50;
        CancelInvoke();
        healthUpCheck = false;
    }

    IEnumerator P5patternEttect()
    {
        p5PatternE.SetActive(true);
        yield return new WaitForSeconds(2f);
        p5PatternE.SetActive(false) ;
    }
    IEnumerator DownPatternEttect()
    {
        downPatterE.SetActive(true);
        yield return new WaitForSeconds(2.3f);
        downPatterE.SetActive(false);
    }
    IEnumerator P2EttectEttect()
    {
        p2EttectE.SetActive(true);
        yield return new WaitForSeconds(1.3f);
        p2EttectE.SetActive(false);
    }
    IEnumerator P1Ettect()
    {
        p1EttectE.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        p1EttectE.SetActive(false);
    }
    IEnumerator HealEttect()
    {
        healEttetE.SetActive(true);
        yield return new WaitForSeconds(5f);
        healEttetE.SetActive(false);
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //����� ������ 
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }

        //Ŭ���� ����� �޴� 
        else
        {
            currPos = (Vector3)stream.ReceiveNext();
            currRot = (Quaternion)stream.ReceiveNext();
        }
    }
}
