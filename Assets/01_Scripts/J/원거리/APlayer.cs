using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class APlayer : MonoBehaviour
{

    [Header("Shop")]
    private GameObject nearObject;
    public int coin;

    [Header("Move")]
    public float speed;
    public float moveSpeed = 8f;
    public float turn;
    public bool desh;
    float deshCool;
    float curDeshCool = 8f;
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
    public APlayerWeapons weapons;
    private PlayableDirector PD;
    public TimelineAsset[] Ta;
    Boss boss;
    public ArchorTPS tps;
    public StateManager stateManager;
    MeshRenderTail meshRenderTail;
   

    [Header("CamBat")]
    public bool isAttack;
    public bool isAttack1;
    public bool isAttack2;
    public bool isAttack3;
    public float fireDelay;
    public bool isFireReady;
    public bool isDeath;
    public bool downing;

    public GameObject rifle;
    public GameObject rGun;
    public GameObject lGun;

    public GameObject skillQ;
    public GameObject skillE;
    public GameObject fskillR;
    public GameObject bskillR;
    public GameObject skillLoding;

    public GameObject rmuzzle;
    public GameObject lmuzzle;

    [Header("Trans")]
    public Transform lTargetPlayer;
    public Transform rTargetPlayer;
    public Transform eskillLoding;
    public Transform eskillActive;
    public Transform rifleFtarget;
    public Transform rifleBtarget; 

    bool qisReady;
    bool eisReady;
    bool risReady;

    public float qskillcool;
    public float eskillcool;
    public float rskillcool;

    public float curQskillcool = 12f;
    public float curEskillcool = 8f;
    public float curRskillcool = 15f;

    [SerializeField] private float rotCamXAxisSpeed = 500f;
    [SerializeField] private float rotCamYAxisSpeed = 3f;
    internal string NickName;

    void Start()
    {
        weapons = GetComponentInChildren<APlayerWeapons>();
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        PD = GetComponent<PlayableDirector>();
        if (boss != null)
        {
            boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>();
        }
        tps = GetComponentInParent<ArchorTPS>();
        stateManager = GetComponent<StateManager>();
       
    }

    //"��������"
    void Interation()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt) && nearObject != null && nearObject.tag == "Shop")
        {
            Shop shop = nearObject.GetComponent<Shop>();
            if (shop != null)
            {
                //shop.Enter(this);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(!isDeath)
        {
            GetinPut();
            Attack();
            SkillOn();
            Death();
            Deshs();
            Interation();
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
        deshCool += Time.deltaTime;
        if(deshCool >= curDeshCool)
        {
            desh = true;
            deshCool = curDeshCool;
        }
        if (desh)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger("isDesh");
                //deshCool = 0;
                isDeshInvincible = true;
            }
        }
    }

    
    IEnumerator LeffectDelay()
    {
        
        lmuzzle.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        lmuzzle.SetActive(false);
    }
    IEnumerator ReffectDelay()
    {
        
        rmuzzle.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        rmuzzle.SetActive(false);
    }

    void Attack()
    {
        fireDelay += Time.deltaTime;
        isFireReady = weapons.rate < fireDelay;

        if (isAttack && isFireReady)
        {
            
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
        if(stateManager.hp <= 0)
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
        qskillcool += Time.deltaTime; 

        if (qskillcool >= curQskillcool)
        {
            qskillcool = curQskillcool;
            qisReady = true;
        }
            
        eskillcool += Time.deltaTime;

        if (eskillcool >= curEskillcool)
        {
            eskillcool = curEskillcool;
            eisReady = true;
        }
        rskillcool += Time.deltaTime;

        if (rskillcool >= curRskillcool)
        {
            rskillcool = curRskillcool;
            risReady = true;
        }

        if(qisReady)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                animator.SetTrigger("SkillQ");
                qskillcool = 0;
                qisReady = false;
            }
        }

        if(eisReady)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                animator.SetTrigger("SkillE");
                eskillcool = 0;
                eisReady = false;
            }
        }
        
        if(risReady)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                animator.SetTrigger("SkillR");
                rskillcool = 0;
                risReady = false;
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

    //Player ���� ���� 
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Shop")
            nearObject = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Shop")
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
    
    void Skill_Q()
    {
        GameObject obj;
        obj = Instantiate(skillQ, lTargetPlayer.position, lTargetPlayer.rotation);
        obj.GetComponent<WeaponsAttribute>().sm = transform.GetComponent<StateManager>();
        Destroy(obj, 1.3f);
    }
    void Skill_E()
    {
        GameObject obj;

        obj = Instantiate(skillE, eskillLoding.position, eskillLoding.rotation);
        //obj.GetComponent<WeaponsAttribute>().sm = transform.GetComponent<StateManager>();
        Destroy(obj, 2f);
    }

    void Skill_R()
    {
        GameObject obj;
        GameObject objs;

        obj = Instantiate(fskillR, rifleFtarget.position, rifleFtarget.rotation);
        objs = Instantiate(bskillR, rifleBtarget.position, rifleBtarget.rotation);
        obj.GetComponent<WeaponsAttribute>().sm = transform.GetComponent<StateManager>();
        Destroy(obj, 1.3f);
        Destroy(objs, 1.3f);

    }

    void ActiveRifle()
    {
        rifle.SetActive(true);
    }
    void HidingRifle()
    {
        rifle.SetActive(false);
    }

    void HandGunActive()
    {
        lGun.SetActive(true);
        rGun.SetActive(true);
    }
    void HidingHandGun()
    {
        lGun.SetActive(false);
        rGun.SetActive(false) ;
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
