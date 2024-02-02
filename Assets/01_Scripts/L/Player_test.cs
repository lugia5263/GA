using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player_test : MonoBehaviourPunCallbacks
{
    //Playerstate�� �����ϳ�?

    private DataMgr dataMgr;

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
        dataMgr = DataMgr.Instance;
        level = dataMgr.Level;
        exp = dataMgr.Exp;
        maxhp = dataMgr.MaxHp;
        hp = dataMgr.Hp;
        attackPower = dataMgr.AttackPower;
        criChance = dataMgr.CriChance;
        criDamage = dataMgr.CriDamage;
        def = dataMgr.Def;
        gageTime = dataMgr.GageTime;

        // �÷��̾� ���� �ʱ�ȭ
        InitializePlayerState();
    }

    // ĳ���� Spawn������, �� ĳ������ ���� �ҷ�����
    private void InitializePlayerState()
    {
        // DataMgr���� ����� ���� �ҷ�����
        attackPower = dataMgr.AttackPower;
        level = dataMgr.Level;
    }

    // ��ȭ�ϱ� �̵��� ��ȭ�Ŵ����� �̵�
    public void Enhance()
    {
        //if (photonView.IsMine)
        //{
            // ��ȭ �۾� ����
            attackPower += 100;
            // ��ȭ �� DataMgr�� ����� ���� ������Ʈ
            dataMgr.AttackPower = attackPower;
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
            dataMgr.Level = level;
        //}
    }
}
