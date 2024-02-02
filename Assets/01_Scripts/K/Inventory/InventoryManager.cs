using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryManager : MonoBehaviour
{
    //#region �̱���
    //private static InventoryManager instance;
    //public static InventoryManager Instance
    //{
    //    get
    //    {
    //        if (instance == null)
    //        {
    //             �ν��Ͻ��� ������ ���� ����
    //            GameObject singletonObject = new GameObject("InventoryManager");
    //            instance = singletonObject.AddComponent<InventoryManager>();
    //            DontDestroyOnLoad(singletonObject); // �� ��ȯ �ÿ��� �����ǵ��� ����
    //        }

    //        return instance;
    //    }
    //}
    //#endregion  

    [Space(1)]
    public StateManager stateMgr;
    public GameObject rewardCanvas;
    public GameObject inventoryCanvas;
    public bool isInven;

    [Header("PlayerState")]
    [Header("�̱��� �����")]
    // �� �����͵鵵 DataManager���� �̱��濡�� ���ܿ;��Ѵ�.
    public int weaponLv = 1;
    public string playerNick; // �г���
    public string playerTitle; // Īȣ
    public int playerLevel;

    public int expPotion;
    public int materials;
    public int gold;

    [HideInInspector]
    public Image goldImage;
    public Image expImage;
    public Image materialImage;
    public Text goldTxt;
    public Text expTxt;
    public Text materialTxt;
    public Text atkInfo;
    public Text playerLvTxt;   // �κ��丮â ����
    public Text playerTitleTxt; // �κ��丮â Īȣ
    public Text playerNickTxt; // �κ��丮â �̸�


    private void Awake()
    {
        Transform tr = transform.GetChild(0).GetChild(0).GetChild(1);
        //rewardCanvas = GameObject.Find("RewardContent").gameObject;//TODO:
        inventoryCanvas = GameObject.Find("InventoryCanvas").gameObject;
        goldImage = tr.Find("item_gold").GetComponent<Image>();
        expImage = tr.transform.Find("item_exp").GetComponent<Image>();
        materialImage = tr.transform.Find("item_material").GetComponent<Image>();
        goldTxt = tr.transform.Find("item_goldTxt").GetComponent<Text>();
        expTxt = tr.transform.Find("item_expTxt").GetComponent<Text>();
        materialTxt = tr.transform.Find("item_materialTxt").GetComponent<Text>();
        atkInfo = tr.transform.Find("atkInfo").GetComponent<Text>();
        playerLvTxt = tr.transform.Find("lvInfo").GetComponent<Text>();
        playerNickTxt = tr.transform.Find("PlayerNick").GetComponent<Text>();
        playerTitleTxt = tr.transform.Find("PlayerTitle").GetComponent<Text>();

        inventoryCanvas.SetActive(false);
    }

    private void Start()
    {
    } // �̱��� ����

    private void Update() // i ������ �κ��丮 ����
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!isInven)
            {
                inventoryCanvas.SetActive(true);
                isInven = true;
            }
            else
            {
                inventoryCanvas.SetActive(false);
                isInven = false;
            }
        }
    }






    #region ������
    public void InitInventory()
    {
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
        goldTxt.text = gold.ToString();
        expTxt.text = expPotion.ToString();
        materialTxt.text = materials.ToString();
        atkInfo.text = stateMgr.atk.ToString();
        playerLvTxt.text = stateMgr.level.ToString();
        
        playerNickTxt.text = playerNick;
        playerTitleTxt.text = playerTitle;

    } //������ 1�� �̻��̸� �ҵ�����

    public void AddMaterial() // �±� ã�� -> �κ��丮 ���� �ø���
    {
            for (int i = 0; i < rewardCanvas.transform.childCount; i++)
            {
                GameObject item = rewardCanvas.transform.GetChild(i).gameObject;
                if (rewardCanvas.transform.GetChild(i).CompareTag("Material"))
                {
                    materials += item.GetComponent<ItemJsonData>().count;
                    item.SetActive(false);
                }
                if (rewardCanvas.transform.GetChild(i).CompareTag("Exp"))
                {
                    expPotion += item.GetComponent<ItemJsonData>().count;
                    item.SetActive(false);
                }
                if (rewardCanvas.transform.GetChild(i).CompareTag("Gold"))
                {
                    gold += item.GetComponent<ItemJsonData>().count;
                    item.SetActive(false);
                }

       
            //TODO: Destroy(item);
        }
    }


    public void SendInventory() // Reward�� �ִ� ������ ���ɹޱ� ��ư
    {
        AddMaterial();
        InitInventory();
    }
#endregion


}
