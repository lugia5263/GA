using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaidBossCtrl : MonoBehaviour
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

    [Header("AttackPattern")]
    public float p1;
    public float p2;
    public float p3;
    public float p4;
    public float p5;
    public float dieNowPattern;

    public float p1check;
    public float p2check;
    public float p3check;
    public float p4check;
    public float p5check;
    public float dieNowPatternCheck;

    public bool p1Ready;
    public bool p2Ready;
    public bool p3Ready;
    public bool p4Ready;
    public bool p5Ready;

    public GameObject dieNowPatternEffect;
    public GameObject DownPattern;
    public float breakTime;
    public float breakCheck;
    public bool breakOn;
    public bool die;
    void Start()
    {
        raidBoss = RAIDBOSS.IDLE;
        characterController = GetComponent<CharacterController>();
        targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        stateManager = GetComponent<StateManager>();
    }

    void Update()
    {
        if (!die)
        {
            BreakTime();
            PatternTimeCheck();
            Dieing();
            DieNowPatternt();
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
                    float dis = Vector3.Distance(targetPlayer.position, transform.position);
                    if (dis > 0.7f)
                    {
                        isActivating = false;
                    }

                    speed = 3f;
                    anim.SetTrigger("RUN");
                    MoveTowardsTarget();
                    float distan = Vector3.Distance(targetPlayer.position, transform.position);
                    if (distan > 18)
                    {
                        raidBoss = RAIDBOSS.IDLE;
                    }
                    break;
                case RAIDBOSS.ATTACK:
                    isActivating = true;
                    speed = 0f;
                    if (p1Ready)
                    {
                        anim.SetTrigger("Pattern1");
                        p1 = 0;
                        p1Ready = false;
                    }
                    if (p2Ready)
                    {
                        anim.SetTrigger("Pattern2");
                        p2 = 0;
                        p2Ready = false;
                    }
                    if (p3Ready)
                    {
                        anim.SetTrigger("Pattern3");
                        p3 = 0;
                        p3Ready = false;
                    }
                    if (p4Ready)
                    {
                        anim.SetTrigger("Pattern4");
                        p4 = 0;
                        p4Ready = false;
                    }
                    if (p5Ready)
                    {
                        anim.SetTrigger("Pattern5");
                        p5 = 0;
                        p5Ready = false;
                    }
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
                    anim.SetTrigger("Break");
                    speed = 0f;
                    breakTime = 0f;
                    StartCoroutine(breakTiming());
                    break;
                case RAIDBOSS.DOWN:
                    isActivating = true;
                    speed = 0f;
                    anim.SetTrigger("Down");
                    float dista = Vector3.Distance(targetPlayer.position, transform.position);
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
    void MoveTowardsTarget()
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

    private void OnTriggerEnter(Collider other)
    {
        if(breakOn)
        {
            if(other.CompareTag("DownSkill"))
            {
                raidBoss = RAIDBOSS.DOWN;
            }
        }
    }

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
    void BreakTime()
    {
        breakTime += Time.deltaTime;
        if(breakTime >= breakCheck)
        {
            raidBoss = RAIDBOSS.BREAK;
            breakOn = true;
            breakTime = breakCheck;
        }
    }
    IEnumerator breakTiming()
    {
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
        DownPattern.SetActive(true);
        yield return new WaitForSeconds(2f);
        DownPattern.SetActive(false);
    }

    void Dieing()
    {
        if(stateManager.hp <= 0)
        {
            die = true;
            raidBoss = RAIDBOSS.DIE;
        }
    }

    void DieNowPatternt()
    {
        dieNowPattern += Time.deltaTime;
        if(dieNowPattern >= dieNowPatternCheck)
        {
            Vector3 Pos = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z - 1f);
            GameObject obj;
            obj = Instantiate(dieNowPatternEffect, Pos, transform.rotation);
            Destroy(obj, 3f);
            dieNowPattern = 0;
            anim.SetTrigger("Rolling");
        }
    }
}
