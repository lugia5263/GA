using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    public enum BOSSSTATE
    {
        IDLE = 0,
        MOVE,
        ATTACK,
        DOWN,
        DIE,
        NEM1,
        NEM2,
        NEM3
    }

    [Header("Com")]
    public BOSSSTATE BossState;
    public Animator BossAnim;
    public Transform TargetPlayer;
    public NavMeshAgent NvAgent;
    public CharacterController characterController;
    public Rigidbody rigidbody;
    public BoxCollider HitBox;
    public BoxCollider NeM1area;
    private List<GameObject> gameObjects = new List<GameObject>();
    public GameObject[] prefabs;
    public Player player;
    StateManager stateManager;
    public GameObject Nem2Area;
    public GameObject SafeZoneBall;

    [Header("Stet")]
    public float Speed;
    public float Range;
    
    public float AttackRange;
    public int BossDamage;

    public bool isDeath;

    [Header("Tech")]
    public bool attacking;
    public float patternTime;
    public float Nem2PatternTime;

    void Start()
    {
        BossAnim = GetComponent<Animator>();
        NvAgent = GetComponent<NavMeshAgent>();
        TargetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();
        BossState = BOSSSTATE.IDLE;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        stateManager = GetComponent<StateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        patternTime += Time.deltaTime;
        Nem2PatternTime += Time.deltaTime;
        NemStart();
        Nem2Start();
        if (isDeath != true)
        {
            Die();

            switch (BossState)
            {
                case BOSSSTATE.IDLE:
                    BossAnim.SetInteger("BOSSSTATE", 0);
                    NvAgent.speed = 0;

                    float dist = Vector3.Distance(TargetPlayer.position, transform.position);
                    if (dist < Range)
                    {
                        BossState = BOSSSTATE.MOVE;
                    }
                    else
                    {
                        BossState = BOSSSTATE.IDLE;
                    }


                    break;
                case BOSSSTATE.MOVE:
                    BossAnim.SetInteger("BOSSSTATE", 1);
                    NvAgent.speed = Speed;

                    float distan = Vector3.Distance(TargetPlayer.position, transform.position);
                    if (distan < AttackRange)
                    {
                        BossState = BOSSSTATE.ATTACK;
                    }
                    else
                    {
                        NvAgent.SetDestination(TargetPlayer.position + new Vector3(0, 0, 2.5f));
                    }
                    if (distan > Range)
                    {
                        BossState = BOSSSTATE.IDLE;
                    }


                    break;
                case BOSSSTATE.ATTACK:
                    BossAnim.SetInteger("BOSSSTATE", 2);
                    NvAgent.speed = 0;
                    attacking = true;
                    if (attacking == true)
                    {
                        NvAgent.speed = 0;
                    }
                    else
                    {
                        NvAgent.speed = Speed;

                    }
                    float dists = Vector3.Distance(TargetPlayer.position, transform.position);
                    if (dists > AttackRange)
                    {
                        attacking = false;
                        BossState = BOSSSTATE.MOVE;
                    }
                    else
                    {
                        BossState = BOSSSTATE.ATTACK;
                    }


                    break;
                case BOSSSTATE.DOWN:
                    BossAnim.SetInteger("BOSSSTATE", 3);
                    NvAgent.speed = 0;
                    attacking = true;
                    if (attacking == true)
                    {
                        NvAgent.speed = 0;
                    }
                    else
                    {
                        NvAgent.speed = Speed;

                    }
                    StartCoroutine(StandUp());

                    break;
                case BOSSSTATE.DIE:


                    break;

                case BOSSSTATE.NEM1:
                    BossAnim.SetInteger("BOSSSTATE", 5);
                    if (patternTime > Random.Range(30, 39))
                    {
                        StartCoroutine(Nem1delay());
                        Spawn();
                    }
                    break;
                case BOSSSTATE.NEM2:
                    BossAnim.SetInteger("BOSSSTATE", 5);
                    
                    Nem2();
                    
                    break;
                default:
                    break;
            }
        }
    }

    public void Die()
    {
        if(stateManager.hp <=0)
        {
            NvAgent.speed = 0;
            isDeath = true;
            StartCoroutine(DeathDelay());
        }
    }
    IEnumerator DeathDelay()
    {
        BossAnim.SetTrigger("Die");
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("DownSkill"))
        {
            NvAgent.speed = 0;
            BossState = BOSSSTATE.DOWN;
        }

       
    }

    IEnumerator StandUp() 
    {
        yield return new WaitForSeconds(3f);
        float distans = Vector3.Distance(TargetPlayer.position, transform.position);
        if (distans > AttackRange)
        {
            attacking = false;
            BossState = BOSSSTATE.MOVE;
        }
        else
        {
            BossState = BOSSSTATE.ATTACK;
        }
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 basePosition = transform.position;

        Vector3 size = NeM1area.size;

        float posX = basePosition.x + Random.Range(-size.x, size.x);
        float posY = basePosition.y + Random.Range(0,size.y);
        float posZ = basePosition.z + Random.Range(-size.z, size.z);

        Vector3 spawnPos = new Vector3(posX, posY, posZ);

        return spawnPos;
    }
    private void NemStart()
    {
        if (patternTime >= 30)
        {
            BossState = BOSSSTATE.NEM1;
        }
    }
    void Nem2Start()
    {
        if (Nem2PatternTime >= 120f)
        {
            Nem2();
            Nem2PatternTime = 0;
        }
    }
    void Spawn()
    {

        int selection = Random.Range(0, prefabs.Length);

        GameObject selectedPrefab = prefabs[selection];

        Vector3 spawnPos = GetRandomPosition();

        GameObject instance = Instantiate(selectedPrefab, spawnPos, Quaternion.Euler(0, 0, 0));
        gameObjects.Add(instance);

        BossState = BOSSSTATE.IDLE;
    }

    private Vector3 SaveBallPosition()
    {
        Vector3 basePosition = transform.position;

        Vector3 size = NeM1area.size;

        float posX = basePosition.x + Random.Range(-size.x, size.x);
        float posY = basePosition.y + 1f;
        float posZ = basePosition.z + Random.Range(-size.z, size.z);

        Vector3 spawnPos = new Vector3(posX, posY, posZ);

        return spawnPos;
    }
    void Nem2()
    {
        Vector3 spawnPos = SaveBallPosition();

        Instantiate(SafeZoneBall, spawnPos, Quaternion.Euler(0, 0, 0));
        Instantiate(Nem2Area, transform.position, transform.rotation);
       
    }
    IEnumerator Nem1delay()
    {
        yield return new WaitForSeconds(4.5f);
        patternTime = 0;
    }
}
