// Shop.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

[System.Serializable]
public class Product
{
    public string name;
    public int price;
}

[System.Serializable]
public class ProductList
{
    public List<Product> products;
}

public class Shop : MonoBehaviour
{
    public RectTransform uiGroup;
    public GameObject itemButtonPrefab;
    public Transform itemButtonParent;
    public Text talkText;

    Player enterPlayer;
    int selectedItemIndex = -1;
    List<Product> productList;

    void Start()
    {
        LoadProductsFromJSON();
        CreateItemButtons();
    }


    void LoadProductsFromJSON()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "ClothesShop.json");

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            ProductList productListData = JsonUtility.FromJson<ProductList>(json);
            productList = productListData.products;
        }
        else
        {
            Debug.LogError("Products JSON file not found!");
        }
    }

    void CreateItemButtons()
    {
        foreach (Product product in productList)
        {
            GameObject itemButton = Instantiate(itemButtonPrefab, itemButtonParent);
            Text buttonText = itemButton.GetComponentInChildren<Text>();
            buttonText.text = product.name + " - $" + product.price;

            Button button = itemButton.GetComponent<Button>();
            int index = productList.IndexOf(product);
            button.onClick.AddListener(() => SelectItem(index));
        }
    }

    public void Enter(Player player)
    {
        enterPlayer = player;
        uiGroup.anchoredPosition = Vector3.zero;
    }

    public void Exit()
    {
        uiGroup.anchoredPosition = Vector3.down * 1000;
    }


    void SelectItem(int index)
    {
        selectedItemIndex = index;
        talkText.text = "Press Alt to purchase item: " + productList[index].name;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if (selectedItemIndex != -1)
            {
                Buy(selectedItemIndex);
            }
        }
    }

    void Buy(int index)
    {
        int price = productList[index].price;

        if (price <= enterPlayer.coin)
        {
            // 코인이 충분한 경우
            enterPlayer.coin -= price;
            talkText.text = "Item purchased: " + productList[index].name;

            // 여기에 추가로 구매 시 동작을 넣을 수 있습니다.
            // 예를 들어, 아이템을 플레이어에게 추가하는 등의 동작을 수행합니다.
            AddItemToPlayer(index);
        }
        else
        {
            // 코인이 부족한 경우
            talkText.text = "Not enough coins to purchase " + productList[index].name;

            // 여기에 추가로 코인 부족 시 동작을 넣을 수 있습니다.
            // 예를 들어, 코인 부족 팝업을 띄우거나 특정 효과를 주는 등의 동작을 수행합니다.
            ShowNotEnoughCoinsPopup();
        }
    }

    void AddItemToPlayer(int index)
    {
        // 여기에 아이템을 플레이어에게 추가하는 코드를 작성합니다.
        // 예를 들어, 아이템의 인덱스를 확인하고 해당 아이템을 플레이어 인벤토리에 추가하는 등의 동작을 수행합니다.
        // 이 부분은 게임에 따라 다르게 구현될 수 있습니다.
    }

    void ShowNotEnoughCoinsPopup()
    {
        // 여기에 코인 부족 팝업을 띄우거나 특정 효과를 주는 코드를 작성합니다.
        // 이 부분은 게임에 따라 다르게 구현될 수 있습니다.
    }
}
