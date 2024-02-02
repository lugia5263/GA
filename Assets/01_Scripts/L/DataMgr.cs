using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataMgr : MonoBehaviour
{
    public DataMgrDontDestroy dataMgrDontDestroy;
    public InventoryManager inventoryMgr;
    public StateManager stateMgr;
    public RewardMgr rewardMgr;
    public TrophyMgr trophyMgr;

    public TextAsset forcetxtFile; //Jsonfile

    [Header("��ȭ�Ͻðڽ��ϱ�?")]
    public GameObject enforcePanel;
    public Text weaponNowTxt;
    public Text weaponAftTxt;
    public Text wantEnforceTxt;
    public Text needMaterial;
    public Text needGold;

    public GameObject enforceEffect;
    public Jun_TweenRuntime lessTween;

    [Header("��ü���")]
    public GameObject successPanel;
    public Text successweaponNowTxt;
    public Text beforeAtk;
    public Text afterAtk;

    [Header("��ý���")]
    public GameObject failedPanel;
    public Text failweaponNowTxt;
    public Text beforeAtkF;
    public Text afterAtkF;

    [Header("����Ŭ�����")]
    public int failEnforceCount;
    public int playerWeaponLv;

    private void Awake()
    {
        var jsonitemFile = Resources.Load<TextAsset>("Json/EnforceTable");
        forcetxtFile = jsonitemFile;
        lessTween = GameObject.Find("lessTween").GetComponent<Jun_TweenRuntime>();
        enforcePanel = GameObject.Find("EnforcePanel");
        rewardMgr = GameObject.Find("RewardMgr").GetComponent<RewardMgr>();
        //trophyMgr = GameObject.Find("TrophyMgr").GetComponent<TrophyMgr>();
        wantEnforceTxt = GameObject.Find("ReallyTxt").GetComponent<Text>();
        weaponNowTxt = GameObject.Find("ReadyBefore").GetComponent<Text>();
        weaponAftTxt = GameObject.Find("ReadyAfter").GetComponent<Text>();
        needMaterial = GameObject.Find("needMaterial").GetComponent<Text>();
        needGold = GameObject.Find("needGold").GetComponent<Text>();
        enforceEffect = GameObject.Find("EnforceEffect");

        successPanel = GameObject.Find("SuccessPanel");
        successweaponNowTxt = GameObject.Find("ForceLvS").GetComponent<Text>();
        beforeAtk = GameObject.Find("beforeAtk").GetComponent<Text>();
        afterAtk = GameObject.Find("afterAtk").GetComponent<Text>();


        failedPanel = GameObject.Find("FailedPanel");
        failweaponNowTxt = GameObject.Find("ForceLvF").GetComponent<Text>();
        beforeAtkF = GameObject.Find("beforeAtkF").GetComponent<Text>();
        afterAtkF = GameObject.Find("afterAtkF").GetComponent<Text>();
    }
}