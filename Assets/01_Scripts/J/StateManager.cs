using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class StateManager : MonoBehaviourPunCallbacks
{
    public DataMgrDontDestroy dataMgrDontDestroy;

    [Header("Stet")]
    // �÷��̾��� ����!!!!
    public int level = 1;
    public int exp;
    public float maxhp;
    public float hp;
    public int weaponLevel;
    public int attackPower;
    public int criChance;
    public float criDamage;
    //public int classNum; // Ŭ���� �����, �� �κе� �Ű���ּ��� 1���� 2���� 3����

    [Space(10)]
    [Range(0, 100)]
    public int userGold;
    public int userMaterial;
    public int userExpPotion;

    [HideInInspector]
    
    public HUDManager hudManager;
    public PhotonView pv;

    private void Start()
    {
        hudManager = gameObject.GetComponent<HUDManager>();
        pv = GetComponent<PhotonView>();
        dataMgrDontDestroy = DataMgrDontDestroy.Instance;

        level = dataMgrDontDestroy.Level;
        exp = dataMgrDontDestroy.Exp;
        maxhp = dataMgrDontDestroy.MaxHp;
        hp = dataMgrDontDestroy.Hp;
        weaponLevel = dataMgrDontDestroy.WeaponLevel;
        attackPower = dataMgrDontDestroy.AttackPower;
        criChance = dataMgrDontDestroy.CriChance;
        criDamage = dataMgrDontDestroy.CriDamage;
        userGold = dataMgrDontDestroy.UserGold;
        userMaterial = dataMgrDontDestroy.UserMaterial;
        userExpPotion = dataMgrDontDestroy.UserExpPotion;

        hp = maxhp;
    }


    #region ����� ����######
    //���ݷ� 30 100%
    // ��ų�� 60 200 %= 200 / 100
    //���� float���� 200 �־ 60 ���� ��������
    //30 x 200 x n = 60
    //200n = 2
    //100n = 1
    //n = 0.01
    // ���� ���ݷ� x ��ų����� x 0.01f �ؾ� �ۼ�Ʈ������� ���´�!
    // skillDMG�� �⺻������ 100 �����
    /// atk * ( skillDMG * 0.01f); �� �ϸ�, 150%�� ���� ��, 1.5���� ������� ����!

    #endregion

    public void DealDamage(GameObject target, float skillDMG)//�� ���, 105% �������� �� ��!
    {
        Color popupColorsend = Color.white;
        var monster = target.GetComponent<StateManager>();
        if (monster != null)
        {
            float totalDamage = attackPower * (skillDMG * Random.Range(0.005f, 0.01f));
            if (Random.Range(0f, 100f) <= criChance)
            {
                totalDamage *= criDamage * 0.02f;
                popupColorsend = Color.yellow;
            }

            monster.TakeDamage((int)totalDamage, popupColorsend);
        }
    }
    public void TakeDamage(int hit, Color popupColor) // �� �˾�
    {
        hp -= hit;
        Vector3 randomness = new Vector3(Random.Range(-0.45f, 0.45f), Random.Range(-0.45f, 0.45f), Random.Range(0f, 0.25f));
        // hit - (hit*def/100)

        DamagePopUpGenerator.current.CreatePopup(transform.position + randomness, hit.ToString(), popupColor);
        //hudManager.ChangeUserHUD();
    }
}