using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private GameObject nearObject;

    [Header("Move")]
    public float speed;
    public float moveSpeed = 8f;
    public float turn;
    public bool Desh;
    float DeshCool;
    float CurDeshCool = 8f;
    public bool isDeshInvincible;

    float hAxis;
    float vAxis;
    Vector3 moveVec;

    [Header("Component")]
    public CharacterController characterController;
    public Rigidbody rigid;
    public GameObject rigids;
    public Transform CameraArm;
    Animator animator;
    public TrailRenderer trailRenderer;
    public Weapons weapons;
    private PlayableDirector PD;
    public TimelineAsset[] Ta;
    Boss boss;
    TPScontroller tps;
    public StateManager stateManager;
    MeshRenderTail meshRenderTail;
   

    [Header("CamBat")]
    public bool isAttack;
    public bool isAttack1;
    public bool isAttack2;
    public bool isAttack3;
    float fireDelay;
    public bool isFireReady;
    public bool isDeath;
    public bool downing;

    public GameObject SkillOneEffect;
    public GameObject SkillQ;
    public GameObject SkillE;
    public GameObject SkillR;
    public GameObject SkillLoding;
    
    public Transform TargetPlayer;

    bool QisReady;
    bool EisReady;
    bool RisReady;

    public float Qskillcool;
    public float Eskillcool;
    public float Rskillcool;

    public float CurQskillcool = 12f;
    public float CurEskillcool = 8f;
    public float CurRskillcool = 15f;

    [SerializeField] private float rotCamXAxisSpeed = 500f;
    [SerializeField] private float rotCamYAxisSpeed = 3f;
    void Start()
    {
        weapons = GetComponentInChildren<Weapons>();
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        PD = GetComponent<PlayableDirector>();
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>();
        tps = GetComponentInParent<TPScontroller>();
        stateManager = GetComponent<StateManager>();
       
    }

    void Interation()
    {
        if(nearObject.tag == "Shop")
        {
            Shop shop = nearObject.GetComponent<Shop>();
            shop.Enter(this);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(isDeath != true)
        {
            GetinPut();
            Attack();
            SkillOn();
            Death();
            Deshs();
        }
        
    }

    void GetinPut()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        turn = Input.GetAxisRaw("Mouse X");
        isAttack = Input.GetButtonDown("Fire");
    }
    void Deshs()
    {
        DeshCool += Time.deltaTime;
        if(DeshCool >= CurDeshCool)
        {
            Desh = true;
            DeshCool = CurDeshCool;
        }
        if (Desh)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger("isDesh");
                DeshCool = 0;
                isDeshInvincible = true;
            }
        }
    }
    void Attack()
    {
        fireDelay += Time.deltaTime;
        isFireReady = weapons.rate < fireDelay;

        if (isAttack && isFireReady)
        {
            weapons.WeaponUse();
            animator.SetTrigger("Attack");
            fireDelay = 0;
        }
        if(isAttack1)
        {
            isAttack3 = false;
            if(Input.GetMouseButtonDown(1))
            {
                animator.SetTrigger("Smash1");
                isAttack1 = false;
            }
        }
        if(isAttack2)
        {
            isAttack1 = false;
            if(Input.GetMouseButtonDown(1))
            {
                animator.SetTrigger("Smash2");
                isAttack2 = false;
            }
        }
        if(isAttack3)
        {
            isAttack2 = false;
            if(Input.GetMouseButtonDown(1))
            {
                animator.SetTrigger("Smash3");
                isAttack3 = false;
            }
        }
    }


    public void Death()
    {

        if (stateManager.hp <= 0)
        {
            isDeath = true;
            characterController.enabled = false;
            StartCoroutine(DeathDelay());
        }

    }

    IEnumerator DeathDelay()
    {
        animator.SetTrigger("isDeath");
        yield return null;
    }
    void SkillOn()
    {
        Qskillcool += Time.deltaTime; 

        if (Qskillcool >= CurQskillcool)
        {
            Qskillcool = CurQskillcool;
            QisReady = true;
        }
            
        Eskillcool += Time.deltaTime;

        if (Eskillcool >= CurEskillcool)
        {
            Eskillcool = CurEskillcool;
            EisReady = true;
        }
        Rskillcool += Time.deltaTime;

        if (Rskillcool >= CurRskillcool)
        {
            Rskillcool = CurRskillcool;
            RisReady = true;
        }

        if(QisReady)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                animator.SetTrigger("SkillQ");
                Qskillcool = 0;
                QisReady = false;
            }
        }

        if(EisReady)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                animator.SetTrigger("SkillE");
                Eskillcool = 0;
                EisReady = false;
            }
        }
        
        if(RisReady)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                animator.SetTrigger("SkillR");
                Rskillcool = 0;
                RisReady = false;
            }
        }
    }
   

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("DownPattern"))
        {
            if (isDeshInvincible == true)
                return;

            animator.SetTrigger("Down");
        }

        if (other.CompareTag("SaveZone"))
        {
            isDeshInvincible = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Shop")
            nearObject = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Shop")
        {
            Shop shop = nearObject.GetComponent<Shop>();
            shop.Exit();
            nearObject = null;
        }
    }
    void Die()
    {
        isDeath = true;
    }
    void Downing()
    {
        downing = true;
    }

    void StandUp()
    {
        downing = false;
    }
    void SkillOneAttack()
    {
        GameObject obj;

        obj = Instantiate(SkillOneEffect, TargetPlayer.position, TargetPlayer.rotation);

        Destroy(obj, 2f);
    }
    void Skill_Q()
    {
        GameObject obj;

        obj = Instantiate(SkillQ, TargetPlayer.position, TargetPlayer.rotation);
        obj.GetComponent<WeaponsAttribute>().sm = transform.GetComponent<StateManager>();
        Destroy(obj, 2f);
    }
    void Skill_E()
    {
        GameObject obj;

        obj = Instantiate(SkillE, TargetPlayer.position, TargetPlayer.rotation);
        obj.GetComponent<WeaponsAttribute>().sm = transform.GetComponent<StateManager>();
        Destroy(obj, 2f);
    }

    void Skill_R()
    {
        GameObject obj;

        obj = Instantiate(SkillR, TargetPlayer.position, TargetPlayer.rotation);
        obj.GetComponent<WeaponsAttribute>().sm = transform.GetComponent<StateManager>();
        Destroy(obj, 2f);

    }

    void Skill_QLoding()
    {
        GameObject obj;

        obj = Instantiate(SkillLoding, TargetPlayer.position, TargetPlayer.rotation);

        Destroy(obj, 2f);
    }

    public void SlidingUse()
    {
        characterController.enabled = false;
    }

    public void SlidingEnd()
    {
        characterController.enabled = true;
    }
}
