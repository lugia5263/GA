using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DataMgr : MonoBehaviour
{
    private static DataMgr _instance;
    public static DataMgr Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DataMgr>();
            }
            return _instance;
        }
    }

    // 플레이어의 정보를 저장할 변수들

    public int level;
    public int exp;
    public float maxhp;
    public float hp;
    public int attackPower;
    public int criChance = 50; //in percentage
    public float criDamage = 120f;
    public int def; //방어력 안쓰지않나?
    public float gageTime; // 이건뭐지

    // 초기화 메서드
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // 다른 씬으로 이동해도 유지되도록 설정
        }
        else
        {
            Destroy(gameObject); // 중복된 인스턴스 파괴
        }
    }

    // 초기화 메서드
    void Start()
    {
        Initialize(); // 게임 시작 시 유저정보 초기화
    }

    // 게임 시작 시 기존에 있는 정보를 불러오기(curSlotNum의 정보 불러오기)
    public void Initialize()
    {
        ;
    }

    // 공격력
    public int AttackPower
    {
        get { return attackPower; }
        set { attackPower = value; }
    }

    // 레벨
    public int Level
    {
        get { return level; }
        set { level = value; }
    }

    // 경험치통
    public int Exp
    {
        get { return exp; }
        set { exp = value; }
    }

    // 최대HP
    public float MaxHp
    {
        get { return maxhp; }
        set { maxhp = value; }
    }

    // 현재HP?
    public float Hp
    {
        get { return hp; }
        set { hp = value; }
    }

    // 크리확률
    public int CriChance
    {
        get { return criChance; }
        set { criChance = value; }
    }

    // 크리데미지
    public float CriDamage
    {
        get { return criDamage; }
        set { criDamage = value; }
    }

    // 방어력
    public int Def
    {
        get { return def; }
        set { def = value; }
    }

    // 게이지타임?
    public float GageTime
    {
        get { return gageTime; }
        set { gageTime = value; }
    }

    public void LoadData() // 접속했을때, 저장
    {

    }
    public void SaveDate() // 접속종료할때 저장
    {

    }
}
