using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class InventoryManager : MonoBehaviourPunCallbacks
{
    //#region 싱글톤
    //private static InventoryManager instance;
    //public static InventoryManager Instance
    //{
    //    get
    //    {
    //        if (instance == null)
    //        {
    //             인스턴스가 없으면 새로 생성
    //            GameObject singletonObject = new GameObject("InventoryManager");
    //            instance = singletonObject.AddComponent<InventoryManager>();
    //            DontDestroyOnLoad(singletonObject); // 씬 전환 시에도 유지되도록 설정
    //        }

    //        return instance;
    //    }
    //}
    //#endregion  
    public DataMgrDontDestroy dataMgrDontDestroy;
    public PhotonView pv;

    [Space(1)]
    //public StateManager stateMgr;
    public GameObject rewardCanvas;
    public GameObject inventoryPanel;
    public bool isInven;

    // 이 데이터들도 DataManager같은 싱글톤에서 땡겨와야한다.
    public int playerLevel;
    public int weaponLv;
    public int attackPower;
    public string playerTitle; // 칭호
    public string playerNick; // 닉네임
    public int expPotion;
    public int materials;
    public int gold;


    public Image goldImage;
    public Image expImage;
    public Image materialImage;
    public Text goldTxt;
    public Text expTxt;
    public Text materialTxt;
    public Text atkInfo;
    public Text playerLvTxt;   // 인벤토리창 레벨
    public Text playerTitleTxt; // 인벤토리창 칭호
    public Text playerNickTxt; // 인벤토리창 이름


    private void Awake()
    {
        dataMgrDontDestroy = DataMgrDontDestroy.Instance;
        pv = GetComponent<PhotonView>();
        playerLevel = dataMgrDontDestroy.Level;
        weaponLv = dataMgrDontDestroy.WeaponLevel;
        attackPower = dataMgrDontDestroy.AttackPower;
        expPotion = dataMgrDontDestroy.UserExpPotion;
        materials = dataMgrDontDestroy.UserMaterial;
        gold = dataMgrDontDestroy.UserGold;
        playerNick = dataMgrDontDestroy.NickName;
        playerTitle = "Faker"; // 현창 추가. 필요시에 바꾼다.

        inventoryPanel.SetActive(false);
        //Transform tr = transform.GetChild(0).GetChild(0).GetChild(1);
        //rewardCanvas = GameObject.Find("RewardContent").gameObject;//TODO:
        //inventoryPanel = GameObject.Find("InventoryCanvas").gameObject;
        //goldImage = tr.Find("item_gold").GetComponent<Image>();
        //expImage = tr.transform.Find("item_exp").GetComponent<Image>();
        //materialImage = tr.transform.Find("item_material").GetComponent<Image>();
        //goldTxt = tr.transform.Find("item_goldTxt").GetComponent<Text>();
        //expTxt = tr.transform.Find("item_expTxt").GetComponent<Text>();
        //materialTxt = tr.transform.Find("item_materialTxt").GetComponent<Text>();
        //atkInfo = tr.transform.Find("atkInfo").GetComponent<Text>();
        //playerLvTxt = tr.transform.Find("lvInfo").GetComponent<Text>();
        //playerNickTxt = tr.transform.Find("PlayerNick").GetComponent<Text>();
        //playerTitleTxt = tr.transform.Find("PlayerTitle").GetComponent<Text>();
    }

    private void Update() // i 눌러서 인벤토리 열기
    {
        if (pv.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (!isInven)
                {
                    inventoryPanel.SetActive(true);
                    InitInventory();
                    isInven = true;
                }
                else
                {
                    InitInventory();
                    inventoryPanel.SetActive(false);
                    isInven = false;
                }
            }
        }
    }
    public void InitInventory()
    {
        #region 아이템 1개 이상이면 불들어오게
        if (gold >= 1)
        {
            goldImage.color = Color.white;
        }
        else
        {
            goldImage.color = Color.gray;
        }

        if (expPotion >= 1)
        {
            expImage.color = Color.white;
        }
        else
        {
            expImage.color = Color.gray;
        }

        if (materials >= 1)
        {
            materialImage.color = Color.white;
        }
        else
        {
            materialImage.color = Color.gray;
        }
        #endregion

        #region DataMgrDontDestroy랑 정보 동기화
        // 레벨, 공격력, 재료, 경험치포션, 골드, 칭호만 초기화하면될듯
        playerLevel = dataMgrDontDestroy.Level;
        attackPower = dataMgrDontDestroy.AttackPower;
        expPotion = dataMgrDontDestroy.UserExpPotion;
        materials = dataMgrDontDestroy.UserMaterial;
        gold = dataMgrDontDestroy.UserGold;
        playerTitle = "Faker";
        #endregion

        goldTxt.text = gold.ToString();
        expTxt.text = expPotion.ToString();
        materialTxt.text = materials.ToString();
        atkInfo.text = attackPower.ToString();
        playerLvTxt.text = playerLevel.ToString();

        playerNickTxt.text = playerNick;
        playerTitleTxt.text = playerTitle;
    }
}
