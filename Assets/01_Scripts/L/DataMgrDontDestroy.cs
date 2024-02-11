using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataMgrDontDestroy : MonoBehaviour
{
    public static DataMgrDontDestroy _instance;
    public bool playerDie = false;
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
    public int classNum; //0:����, 1:�ų�, 2:����
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
    public bool isCompleted;
    public bool isDoing;
    public bool isFirst;
    int completed;
    int doing;
    int tutoIsFirst;
    #endregion

    #region ������ ������ �����ϴ� ����
    [Header("������ ����")]
    public int dungeonSortIdx = 0; //�̱�, ī����, ���̵�
    public int dungeonNumIdx = 0; // n-1, n-2, n-3�� 1, 2, 3��
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

    #region ĳ������ ��������
    // �г���
    public string NickName
    {
        get { return nickName; }
        set { nickName = value; }
    }

    // Ŭ������ȣ 0:����, 1:�ų�, 2:����
    public int ClassNum
    {
        get { return classNum; }
        set { classNum = value; }
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
    public int QuestIdx
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

    public bool IsCompleted
    {
        get { return isCompleted; }
        set { isCompleted = value; }
    }

    public bool IsDoing
    {
        get { return isDoing; }
        set { isDoing = value; }
    }

    public bool IsFirst
    {
        get { return isFirst; }
        set { isFirst = value; }
    }
    #endregion

    #region ���� ����
    public int DungeonSortIdx
    {
        get { return dungeonSortIdx; }
        set { dungeonSortIdx = value; }
    }

    public int DungeonNumIdx
    {
        get { return dungeonNumIdx; }
        set { dungeonNumIdx = value; }
    }
    #endregion

    public void SaveData() // ���������Ҷ� ����
    {
        int slotNum = SelectSlot.slotNum;
        Debug.Log("���� ���Գѹ��� : "+slotNum);
        //PlayerPrefs.SetString($"SlotNum_{slotNum}", slotNum.ToString());
        // �ּ��κ��� �ʿ�����Ű����� ���ӳ����� ���ϴ� ������ �ƴ�
        //PlayerPrefs.SetString($"{slotNum}_Class", className);
        //PlayerPrefs.SetInt($"{slotNum}_ClassNum", classNum);
        #region ĳ���� ���� ����
        PlayerPrefs.SetString($"{slotNum}_NickName", NickName);
        PlayerPrefs.SetInt($"{slotNum}_Level", Level);
        PlayerPrefs.SetInt($"{slotNum}_Exp", Exp);
        PlayerPrefs.SetFloat($"{slotNum}_MaxHp", MaxHp);
        PlayerPrefs.SetFloat($"{slotNum}_Hp", Hp);
        PlayerPrefs.SetInt($"{slotNum}_WeaponLevel", WeaponLevel);
        PlayerPrefs.SetInt($"{slotNum}_AttackPower", AttackPower);
        PlayerPrefs.SetInt($"{slotNum}_CriChance", CriChance);
        PlayerPrefs.SetFloat($"{slotNum}_CriDamage", CriDamage);
        PlayerPrefs.SetInt($"{slotNum}_UserGold", UserGold);
        PlayerPrefs.SetInt($"{slotNum}_Material", UserMaterial);
        PlayerPrefs.SetInt($"{slotNum}_ExpPotion", userExpPotion);
        #endregion

        #region ����Ʈ ���� ����
        PlayerPrefs.SetInt($"{slotNum}_QuestIdx", questIdx);
        PlayerPrefs.SetInt($"{slotNum}_QuestCurCnt", questCurCnt);
        PlayerPrefs.SetInt($"{slotNum}_QuestMaxCnt", questMaxCnt);
        PlayerPrefs.SetString($"{slotNum}_GoalTxt", goalTxt);
        if (isCompleted == true)
        {
            completed = 1;
            PlayerPrefs.SetInt($"{slotNum}_IsCompleted", 1);
        }
        else
        {
            completed = 0;
            PlayerPrefs.SetInt($"{slotNum}_IsCompleted", 0);
        }
        if (isDoing == true)
        {
            doing = 1;
            PlayerPrefs.SetInt($"{slotNum}_IsDoing", 1);
        }
        else
        {
            doing = 0;
            PlayerPrefs.SetInt($"{slotNum}_IsDoing", 0);
        }
        if (isFirst == true)
        {
            tutoIsFirst = 1;
            PlayerPrefs.SetInt($"{slotNum}_IsFirst", 1);
        }
        else
        {
            tutoIsFirst = 0;
            PlayerPrefs.SetInt($"{slotNum}_IsFirst", 0);
        }
        #endregion

        PlayerPrefs.Save();
    }
}