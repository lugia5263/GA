using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON; //########################댕겨와야행

public class RewardMgr : MonoBehaviour
{
    public static RewardMgr reward;
    public InventoryManager inventoryMgr;
    public ImageList imagelist;


    public TextAsset txtFile; //Jsonfile
    public GameObject jsonObject; //Prefab (Json char 달린)


    public GameObject rewardContent;


    private void Start()
    {
        inventoryMgr = GameObject.Find("InventoryMgr").GetComponent<InventoryManager>();
        imagelist = GameObject.Find("ImageList").GetComponent<ImageList>();
        var jsonitemFile = Resources.Load<TextAsset>("Json/ItemList");
        txtFile = jsonitemFile;

        jsonObject = Resources.Load<GameObject>("Prefabs/item");
        rewardContent = GameObject.Find("RewardContent");
    }



    public void clear()
    {

        InstExp(3, 100);
    }
            ///예제입니다. 이렇게 불러오세요!!!
    public void MakeItem(int itemIdx, int count) // n번째 아이템을 count개 얻음
    {
        InstMaterial(itemIdx, count);
    }
    public void MakeItemOne(int itemIdx) // n번째 아이템을 1개 얻음
    {
        InstMaterial(itemIdx, 1);
    }
    public void MakeItemRandomBtn()
    {
        int makeidx = Random.Range(1, 5);// 1에서 4까지 나옴
        InstMaterial(makeidx, 1);
    } // 랜덤으로 재료 1개 얻는 버튼

    public void Reward100exp3EABtn()
    {
        //앞자리 3으로 통일할게요!!!!!
        InstExp(3, 100);
        InstMaterial(3, 200);
        InstGOld(100); //3
    }






    // 재료소환 함수
    public void InstMaterial(int n, int itemcount)
    {

        string json = txtFile.text;
        var jsonData = JSON.Parse(json);


        int item = n-1; // 매개변수


        GameObject character = Instantiate(jsonObject); // 만들거야

        character.transform.name = jsonData["Weapon"][item]["Name"]; // 오브젝트명 정의

        character.GetComponent<ItemJsonData>().charname = (jsonData["Weapon"][item]["Name"]);
        character.GetComponent<ItemJsonData>().discription = (jsonData["Weapon"][item]["Discription"]);
        character.GetComponent<ItemJsonData>().atk = (int)(jsonData["Weapon"][item]["Str"]);
        character.GetComponent<ItemJsonData>().rarity = (int)(jsonData["Weapon"][item]["Rarity"]);
        character.GetComponent<ItemJsonData>().count += itemcount;
        Debug.Log(jsonData["Weapon"][item]["Name"]);
        character.GetComponent<Image>().sprite = imagelist.meterialsImage[n];

        character.tag = "Material";
        character.transform.SetParent(rewardContent.transform);

    } 


             //경험치 물약 소환 함수
    public void InstExp(int n, int itemcount) 
    {

        string json = txtFile.text;
        var jsonData = JSON.Parse(json);


        int item = n-1; // 매개변수

        GameObject character = Instantiate(jsonObject); // 만들거야

        character.transform.name = jsonData["Food"][item]["Name"]; // 오브젝트명 정의

        character.GetComponent<ItemJsonData>().charname = (jsonData["Food"][item]["Name"]);
        character.GetComponent<ItemJsonData>().discription = (jsonData["Food"][item]["Discription"]);
        character.GetComponent<ItemJsonData>().exp = (int)(jsonData["Food"][item]["exp"]);
        character.GetComponent<ItemJsonData>().rarity = (int)(jsonData["Food"][item]["Rarity"]);
        character.GetComponent<ItemJsonData>().count += itemcount;
        Debug.Log(jsonData["Food"][item]["Name"]);
        character.GetComponent<Image>().sprite = imagelist.expPotionImage[n];

        character.tag = "Exp";
        character.transform.SetParent(rewardContent.transform);

    } 


             // 골드 소환 함수
    public void InstGOld(int itemcount)
    {
        GameObject character = Instantiate(jsonObject); // 만들거야

        string json = txtFile.text;
        var jsonData = JSON.Parse(json);
        character.GetComponent<ItemJsonData>().count += itemcount;
        character.GetComponent<Image>().sprite = imagelist.goldImage[1];
        character.tag = "Gold";
        character.transform.SetParent(rewardContent.transform);
    }



}