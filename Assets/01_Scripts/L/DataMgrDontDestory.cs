using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataMgrDontDestroy : MonoBehaviour
{
    public static DataMgrDontDestroy _instance;
    public static DataMgrDontDestroy Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DataMgrDontDestroy>();
            }
            return _instance;
        }
    }

    #region �÷��̾��� ������ �����ϴ� ����
    [Header("�÷��̾��� ����")]
    public string nickName;
    public int level;
    public int exp;
    public float maxhp;
    public float hp;
    public int weaponLevel;
    public int attackPower;
    public int criChance;
    public float criDamage;
    public int userGold;
    public int userMateiral;
    public int userExpPotion;
    #endregion

    #region ����Ʈ�� ������ �����ϴ� ����
    [Header("����Ʈ�� ����")]
    public int questIdx;
    public string goalTxt;
    public int questCurCnt;
    public int questMaxCnt;
    #endregion

    // �̱���
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // �ߺ��� �ν��Ͻ� �ı�
        }
    }

    // �ʱ�ȭ �ʿ��ϸ� ���
    void Start()
    {

    }
    #region ĳ������ ��������
    // �г���
    public string NickName
    {
        get { return nickName; }
        set { nickName = value; }
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

    // ������ ����
    public int WeaponLevel
    {
        get { return weaponLevel; }
        set { weaponLevel = value; }
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

    // �������
    public int UserGold
    {
        get { return userGold; }
        set { userGold = value; }
    }

    // ������ȭ
    public int UserMaterial
    {
        get { return userMateiral; }
        set { userMateiral = value; }
    }

    // ���� ����ġ����
    public int UserExpPotion
    {
        get { return userExpPotion; }
        set { userExpPotion = value; }
    }
    #endregion

    #region ����Ʈ����
    public int QusetIdx
    {
        get { return questIdx; }
        set { questIdx = value; }
    }

    public string GoalTxt
    {
        get { return goalTxt; }
        set { goalTxt = value; }
    }

    public int QuestCurCnt
    {
        get { return questCurCnt; }
        set { questCurCnt = value; }
    }

    public int QuestMaxCnt
    {
        get { return questMaxCnt; }
        set { questMaxCnt = value; }
    }
    #endregion

    public void LoadData() // ����������, ���� <- �̰� �̹� loadplayer���� start������ datamgr�� ������ ��.
    {

    }

    public void SaveDate() // ���������Ҷ� ����
    {
        int slotNum = SelectSlot.slotNum;
        Debug.Log("���� ���Գѹ��� : "+slotNum);
        //PlayerPrefs.SetString($"SlotNum_{slotNum}", slotNum.ToString());


        // �ּ��κ��� �ʿ�����Ű����� ���ӳ����� ���ϴ� ������ �ƴ�
        //PlayerPrefs.SetString($"{slotNum}_NickName", NickName);
        //PlayerPrefs.SetString($"{slotNum}_Class", className);
        //PlayerPrefs.SetInt($"{slotNum}_ClassNum", classNum);
        #region ĳ���� ���� ����
        Debug.Log($"�����Ϸ��� Level�� : {Level}");
        PlayerPrefs.SetInt($"{slotNum}_Level", Level);
        Debug.Log($"�����Ϸ��� MaxHp�� : {MaxHp}");
        PlayerPrefs.SetFloat($"{slotNum}_MaxHp", MaxHp);
        Debug.Log($"�����Ϸ��� Hp�� : {Hp}");
        PlayerPrefs.SetFloat($"{slotNum}_Hp", Hp);
        Debug.Log($"�����Ϸ��� WeaponLevel�� : {WeaponLevel}");
        PlayerPrefs.SetInt($"{slotNum}_WeaponLevel", WeaponLevel);
        Debug.Log($"�����Ϸ��� AttackPower�� : {AttackPower}");
        PlayerPrefs.SetInt($"{slotNum}_AttackPower", AttackPower);
        PlayerPrefs.SetInt($"{slotNum}_CriChance", CriChance);
        PlayerPrefs.SetFloat($"{slotNum}_CriDamage", CriDamage);
        PlayerPrefs.SetInt($"{slotNum}_UserGold", UserGold);
        PlayerPrefs.SetInt($"{slotNum}_Material", UserMaterial);
        PlayerPrefs.SetInt($"{slotNum}_ExpPotion", userExpPotion);
        #endregion

        #region ����Ʈ ���� ����

        #endregion

        PlayerPrefs.Save();
    }
}