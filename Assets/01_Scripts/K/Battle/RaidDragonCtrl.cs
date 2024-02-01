using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RaidBossCtrl;
using UnityEngine.VFX;
using Unity.VisualScripting;

public class RaidDragonCtrl : MonoBehaviour
{
    public enum RAIDDRAGON
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

    #region 변수

    [Header("Ready")]
    public bool isRaidStart;

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
    public Player player;

    [Header("AttackPattern")]
    public float p1;
    public float dieNowPattern;
    public bool down;
    public bool attacking;
    public GameObject dieNowPatternEffect;
    public float dieNowPatternCheck;
    public float breakTime;
    public float breakCheck;
    public bool breakOn;
    public bool die;

    [Header("Effect")]
    public GameObject[] skill = new GameObject[5];

    public Shader orignShader;
    public Shader hitShader;
    #endregion



    void Start()
    {
        raidBoss = RAIDBOSS.IDLE; //보스 상태 방치
        characterController = GetComponent<CharacterController>(); // 캐릭터 컨트롤러
        stateManager = GetComponent<StateManager>(); // 용 스텟 불러오기
        anim = GetComponent<Animator>(); // 애니메이터 불러오기

        targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();    
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        if (!die || isRaidStart)
        {
            GameObject closestargetPlayer = FindClosestPlayerWithTag("Player");
            if (closestargetPlayer != null)
            {

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
                        isActivating = true; // 공격할땐 스피드가 0 됌
                        speed = 0f;
                        anim.SetTrigger("ATTACK");

                        float dists = Vector3.Distance(targetPlayer.position, transform.position);
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
                        isActivating = true;
                        speed = 0f;
                        breakTime = 0f;
                        break;
                    case RAIDBOSS.DOWN:
                        isActivating = true;
                        down = true;
                        speed = 0f;
                        float dista = Vector3.Distance(targetPlayer.position, transform.position);
                        anim.SetTrigger("HIT");
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
                        isActivating = true;
                        speed = 0;
                        anim.SetTrigger("Die");
                        break;
                    case RAIDBOSS.PAGE1:
                        break;
                }
            }
        }
    }


    GameObject FindClosestPlayerWithTag(string tag)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag(tag);
        GameObject closestPlayer = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject player in players)
        {
            float distanceToPlayer = Vector3.Distance(currentPosition, player.transform.position);

            if (distanceToPlayer < closestDistance)
            {
                closestDistance = distanceToPlayer;
                closestPlayer = player;
            }
        }

        return closestPlayer;
    } // 가까운 플레이어 찾기

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

        if (Vector3.Distance(transform.position, targetPlayer.transform.position) < 1.5f)
        {
            raidBoss = RAIDBOSS.ATTACK;
        }
    }
    IEnumerator MaterialLight()
    {
        transform.GetChild(0).GetComponent<Renderer>().material.shader = hitShader;
        yield return new WaitForSeconds(0.2f);
        transform.GetChild(0).GetComponent<Renderer>().material.shader = orignShader;

    }


    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(MaterialLight());
        if (breakOn)
        {
            if (other.CompareTag("DownSkill"))
            {
                raidBoss = RAIDBOSS.DOWN;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        //if (other.gameObject.CompareTag("TimeSlow"))
        //{
        //    anim.speed = 0.15f;
        //    speed = 0.3f;
        //}
        //else
        //{
        //    speed = 3f;
        //    anim.speed = 1f;
        //}
    } // TimeSlow

}
