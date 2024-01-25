using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    [Header("Shop")]
    private GameObject nearObject;
    public int coin;

    [Header("Move")]
    public float speed;
    public float moveSpeed = 8f;
    public float turn;
    public bool Desh;
    float DeshCool;
    float CurDeshCool = 8f;
    public bool isDeshInvincible;

    float chargingTime;
    float curchargingTime = 3f;
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
    public TPScontroller tps;
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

    [Header("SkillEffect")]
    public GameObject[] Skill;

    [Header("Shot or Active Point")]
    public Transform[] Point;

    [Header("Guns or Object")]
    public GameObject[] ob;

    [Header("Skill CoolTime")]
    public Image[] skillIcon;

    public bool skillUse;
    public bool qisReady;
    public bool eisReady;
    public bool risReady;

    public float qskillcool;
    public float eskillcool;
    public float rskillcool;

    public float curQskillcool;
    public float curEskillcool;
    public float curRskillcool;

    [SerializeField] private float rotCamXAxisSpeed = 500f;
    [SerializeField] private float rotCamYAxisSpeed = 3f;
    internal string NickName;

    void Awake()
    {
        isFireReady = true;
        weapons = GetComponentInChildren<Weapons>();
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        if (boss != null)
        {
            boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>();
        }
        tps = GetComponentInParent<TPScontroller>();
        stateManager = GetComponent<StateManager>();
        //skillIcon[0] = GameObject.Find("CoolTimeBGQ").GetComponent<Image>();
        //skillIcon[1] = GameObject.Find("CoolTimeBGE").GetComponent<Image>();
        //skillIcon[2] = GameObject.Find("CoolTimeBGR").GetComponent<Image>();
    }

    private void Start()
    {
        skillIcon[0].fillAmount = 0;
        skillIcon[1].fillAmount = 0;
        skillIcon[2].fillAmount = 0;
    }

    //"��������"
    void Interation()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt) && nearObject != null && nearObject.tag == "Shop")
        {
            Shop shop = nearObject.GetComponent<Shop>();
            if (shop != null)
            {
                shop.Enter(this);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!isDeath)
        {
            GetinPut();
            Attack();
            SkillOn();
            Death();
            Deshs();
            Interation();
            //SkillCoolTime();
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
        if (DeshCool >= CurDeshCool)
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

    void SkillCoolTime()
    {
        if (!qisReady)
        {
            skillIcon[0].fillAmount = 1 - qskillcool /curQskillcool;
        }
        if (!eisReady)
        {
            skillIcon[1].fillAmount = 1 - eskillcool / curEskillcool;
        }
        if (!risReady)
        {
            skillIcon[2].fillAmount = 1 - rskillcool / curRskillcool;
        }
    }


    void Attack()
    {
        //chargingTime += Time.deltaTime;
        fireDelay += Time.deltaTime;
        isFireReady = weapons.rate < fireDelay;

        if (isAttack && isFireReady)
        {
            weapons.WeaponUse();
            animator.SetTrigger("Attack");
            fireDelay = 0;
        }
        if (isAttack1)
        {
            isAttack3 = false;
            if (Input.GetMouseButtonDown(1))
            {
                animator.SetTrigger("Smash1");
                isAttack1 = false;
            }
        }
        if (isAttack2)
        {
            isAttack1 = false;
            if (Input.GetMouseButtonDown(1))
            {
                animator.SetTrigger("Smash2");
                isAttack2 = false;
            }
        }
        if (isAttack3)
        {
            isAttack2 = false;
            if (Input.GetMouseButtonDown(1))
            {
                animator.SetTrigger("Smash3");
                isAttack3 = false;
                //if(Input.GetMouseButton(1))
                //{
                    //if(chargingTime >= curchargingTime)
                   // {
                       // animator.SetTrigger("Charging");
                       // isAttack3 = false;
                   // }
               // } 차징 스킬 구현중
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

        if (qisReady)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                animator.SetTrigger("SkillQ");
                qskillcool = 0;
                qisReady = false;
            }
        }

        if (eisReady)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                animator.SetTrigger("SkillE");
                eskillcool = 0;
                eisReady = false;
            }
        }

        if (risReady)
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
        if (other.CompareTag("DownPattern"))
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
    void SkillUsing()
    {
        skillUse = true;
    }
    void SkillClose()
    {
        skillUse = false;
    }

   
    void SwordSkill_Q()
    {
        GameObject obj;

        obj = Instantiate(Skill[0], Point[0].position, Point[0].rotation);
        obj.GetComponent<WeaponsAttribute>().sm = transform.GetComponent<StateManager>();
        Destroy(obj, 2f);
    }
    void SwordSkill_E()
    {
        GameObject obj;

        obj = Instantiate(Skill[1], Point[0].position, Point[0].rotation);
        obj.GetComponent<WeaponsAttribute>().sm = transform.GetComponent<StateManager>();
        Destroy(obj, 2f);
    }

    void SwordSkill_R()
    {
        GameObject obj;

        obj = Instantiate(Skill[2], Point[0].position, Point[0].rotation);
        obj.GetComponent<WeaponsAttribute>().sm = transform.GetComponent<StateManager>();
        Destroy(obj, 2f);
    }

    void A_LfireAttack()
    {
        Instantiate(Skill[4], Point[5].transform.position, Point[5].transform.rotation);
    }
      void A_RfireAttack()
    {
        Instantiate(Skill[4], Point[5].transform.position, Point[5].transform.rotation);
    }

    void A_SkillQ()
    {
        GameObject obj;

        obj = Instantiate(Skill[0], Point[0].position, Point[0].rotation);
        obj.GetComponent<WeaponsAttribute>().sm = transform.GetComponent<StateManager>();
        Destroy(obj, 1.7f);
    }
    void A_SkillE()
    {
        ob[3].SetActive(true);
        StartCoroutine(eskillDelay());
    }
    void A_SkillEclose()
    {
        ob[3].SetActive(false);
    }
    IEnumerator eskillDelay()
    {
        yield return new WaitForSeconds(1.8f);
        GameObject obj;
        obj = Instantiate(Skill[1], Point[2].position, Point[2].rotation);
        obj.GetComponent<WeaponsAttribute>().sm = transform.GetComponent<StateManager>();
        Destroy(obj, 2f);
    }
    void A_SkillR()
    {
        GameObject obj;
        GameObject objs;

        obj = Instantiate(Skill[2], Point[3].position, Point[3].rotation);
        objs = Instantiate(Skill[3], Point[4].position, Point[4].rotation);
        obj.GetComponent<WeaponsAttribute>().sm = transform.GetComponent<StateManager>();
        Destroy(obj, 1.3f);
        Destroy(objs, 1.3f);
    }

    IEnumerator M_SkillQ()
    {
        Skill[0].SetActive(true);
        yield return new WaitForSeconds(4f);
        Skill[0].SetActive(false);
    }

    void M_SkillE()
    {

    }

    void M_SkillR()
    {

    }

    void ActiveRifle()
    {
        ob[2].SetActive(true);
    }
    void HidingRifle()
    {
        ob[2].SetActive(false);
    }

    void HandGunActive()
    {
        ob[1].SetActive(true);
        ob[0].SetActive(true);
    }
    void HidingHandGun()
    {
        ob[1].SetActive(false);
        ob[0].SetActive(false);
    }

    IEnumerator LeffectDelay()
    {

        ob[4].SetActive(true);
        Instantiate(Skill[4], Point[5].transform.position, Point[5].transform.rotation);
        yield return new WaitForSeconds(0.3f);
        ob[4].SetActive(false);
    }
    IEnumerator ReffectDelay()
    {

        ob[5].SetActive(true);
        Instantiate(Skill[4], Point[5].transform.position, Point[5].transform.rotation);
        yield return new WaitForSeconds(0.3f);
        ob[5].SetActive(false);
    }


  
}
