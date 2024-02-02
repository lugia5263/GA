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

    // �÷��̾��� ������ ������ ������

    public int level;
    public int exp;
    public float maxhp;
    public float hp;
    public int attackPower;
    public int criChance = 50; //in percentage
    public float criDamage = 120f;
    public int def; //���� �Ⱦ����ʳ�?
    public float gageTime; // �̰ǹ���

    // �ʱ�ȭ �޼���
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // �ٸ� ������ �̵��ص� �����ǵ��� ����
        }
        else
        {
            Destroy(gameObject); // �ߺ��� �ν��Ͻ� �ı�
        }
    }

    // �ʱ�ȭ �޼���
    void Start()
    {
        Initialize(); // ���� ���� �� �������� �ʱ�ȭ
    }

    // ���� ���� �� ������ �ִ� ������ �ҷ�����(curSlotNum�� ���� �ҷ�����)
    public void Initialize()
    {
        ;
    }

    // ���ݷ�
    public int AttackPower
    {
        get { return attackPower; }
        set { attackPower = value; }
    }

    // ����
    public int Level
    {
        get { return level; }
        set { level = value; }
    }

    // ����ġ��
    public int Exp
    {
        get { return exp; }
        set { exp = value; }
    }

    // �ִ�HP
    public float MaxHp
    {
        get { return maxhp; }
        set { maxhp = value; }
    }

    // ����HP?
    public float Hp
    {
        get { return hp; }
        set { hp = value; }
    }

    // ũ��Ȯ��
    public int CriChance
    {
        get { return criChance; }
        set { criChance = value; }
    }

    // ũ��������
    public float CriDamage
    {
        get { return criDamage; }
        set { criDamage = value; }
    }

    // ����
    public int Def
    {
        get { return def; }
        set { def = value; }
    }

    // ������Ÿ��?
    public float GageTime
    {
        get { return gageTime; }
        set { gageTime = value; }
    }

    public void LoadData() // ����������, ����
    {

    }
    public void SaveDate() // ���������Ҷ� ����
    {

    }
}
