using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player_test : MonoBehaviourPunCallbacks
{
    //Playerstate에 들어가야하나?

    private DataMgr dataMgr;

    public int level;
    public int exp;
    public float maxhp;
    public float hp;
    public int attackPower;
    public int criChance = 50; //in percentage
    public float criDamage = 120f;
    public int def; //방어력 안쓰지않나?
    public float gageTime; // 이건뭐지

    void Start()
    {
        // DataMgr의 인스턴스 가져오기
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

        // 플레이어 상태 초기화
        InitializePlayerState();
    }

    // 캐릭터 Spawn했을때, 그 캐릭터의 정보 불러오기
    private void InitializePlayerState()
    {
        // DataMgr에서 저장된 정보 불러오기
        attackPower = dataMgr.AttackPower;
        level = dataMgr.Level;
    }

    // 강화하기 이따가 강화매니저로 이동
    public void Enhance()
    {
        //if (photonView.IsMine)
        //{
            // 강화 작업 수행
            attackPower += 100;
            // 강화 후 DataMgr에 저장된 정보 업데이트
            dataMgr.AttackPower = attackPower;
        //}
    }

    // 레벨업하기 레벨업매니저로이동
    public void LevelUp()
    {
        //if (photonView.IsMine)
        //{
            // 레벨업 작업 수행
            level += 1;
            // 레벨업 후 DataMgr에 저장된 정보 업데이트
            dataMgr.Level = level;
        //}
    }
}
