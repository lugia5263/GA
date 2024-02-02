using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player_test : MonoBehaviourPunCallbacks
{
    //Playerstate�� �����ϳ�?

    public DataMgrDontDestroy dataMgrDontDestroy;

    public int level;
    public int exp;
    public float maxhp;
    public float hp;
    public int attackPower;
    public int criChance = 50; //in percentage
    public float criDamage = 120f;
    public int def; //���� �Ⱦ����ʳ�?
    public float gageTime; // �̰ǹ���

    void Start()
    {
        // DataMgr�� �ν��Ͻ� ��������
        dataMgrDontDestroy = DataMgrDontDestroy.Instance;
        level = dataMgrDontDestroy.Level;
        exp = dataMgrDontDestroy.Exp;
        maxhp = dataMgrDontDestroy.MaxHp;
        hp = dataMgrDontDestroy.Hp;
        attackPower = dataMgrDontDestroy.AttackPower;
        criChance = dataMgrDontDestroy.CriChance;
        criDamage = dataMgrDontDestroy.CriDamage;
        //def = dataMgr.Def;
        //gageTime = dataMgr.GageTime;

        // �÷��̾� ���� �ʱ�ȭ
        InitializePlayerState();
    }

    // ĳ���� Spawn������, �� ĳ������ ���� �ҷ�����
    private void InitializePlayerState()
    {
        // DataMgr���� ����� ���� �ҷ�����
        attackPower = dataMgrDontDestroy.AttackPower;
        level = dataMgrDontDestroy.Level;
    }

    // ��ȭ�ϱ� �̵��� ��ȭ�Ŵ����� �̵�
    public void Enhance()
    {
        //if (photonView.IsMine)
        //{
            // ��ȭ �۾� ����
            attackPower += 100;
        // ��ȭ �� DataMgr�� ����� ���� ������Ʈ
        dataMgrDontDestroy.AttackPower = attackPower;
        //}
    }

    // �������ϱ� �������Ŵ������̵�
    public void LevelUp()
    {
        //if (photonView.IsMine)
        //{
            // ������ �۾� ����
            level += 1;
        // ������ �� DataMgr�� ����� ���� ������Ʈ
        dataMgrDontDestroy.Level = level;
        //}
    }
}
