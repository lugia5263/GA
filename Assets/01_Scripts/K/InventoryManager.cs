using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventoryManager : MonoBehaviour
{

    public GameObject rewardCanvas;
    public GameObject inventoryCanvas;

    [Header("PlayerState")]

    //public int maxhp;
    //public int hp;


    public int expPotion;
    public int meterials;
    public int gold;

    public Image goldImage;
    public Image expImage;
    public Image materialImage;
    public Text goldTxt;
    public Text expTxt;
    public Text materialTxt;

    private void Awake()
    {
        rewardCanvas = GameObject.Find("RewardContent");
        inventoryCanvas = GameObject.Find("InventoryCanvas");
        goldImage = GameObject.Find("item_gold").GetComponent<Image>();
        expImage = GameObject.Find("item_exp").GetComponent<Image>();
        materialImage = GameObject.Find("item_material").GetComponent<Image>();
        goldTxt = GameObject.Find("item_goldTxt").GetComponent<Text>();
        expTxt = GameObject.Find("item_expTxt").GetComponent<Text>();
        materialTxt = GameObject.Find("item_materialTxt").GetComponent<Text>();
        InitInventory();
        inventoryCanvas.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.C))
            inventoryCanvas.SetActive(true);
        else
            inventoryCanvas.SetActive(false);
    }
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

        if (meterials >= 1)
        {
            materialImage.color = Color.white;
        }
        else
        {
            materialImage.color = Color.gray;
        }
        goldTxt.text = gold.ToString();
        expTxt.text = expPotion.ToString();
        materialTxt.text = meterials.ToString();

    }

    public void AddMaterial() // 태그 찾아서 인벤토리 카운트 늘리기
    {

        for (int i = 0; i < rewardCanvas.transform.childCount; i++)
        {
            GameObject item = rewardCanvas.transform.GetChild(i).gameObject;
            if (rewardCanvas.transform.GetChild(i).CompareTag("Material"))
            {
                meterials += item.GetComponent<ItemJsonData>().count;
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
            //Destroy(item);
        }

    }



    public void SendInventory() // 아이템 수령받기 버튼
    {
        AddMaterial();
        InitInventory();
    }
}
