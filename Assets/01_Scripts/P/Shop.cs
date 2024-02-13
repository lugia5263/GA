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
            // ������ ����� ���
            enterPlayer.coin -= price;
            talkText.text = "Item purchased: " + productList[index].name;

            // ���⿡ �߰��� ���� �� ������ ���� �� �ֽ��ϴ�.
            // ���� ���, �������� �÷��̾�� �߰��ϴ� ���� ������ �����մϴ�.
            AddItemToPlayer(index);
        }
        else
        {
            // ������ ������ ���
            talkText.text = "Not enough coins to purchase " + productList[index].name;

            // ���⿡ �߰��� ���� ���� �� ������ ���� �� �ֽ��ϴ�.
            // ���� ���, ���� ���� �˾��� ���ų� Ư�� ȿ���� �ִ� ���� ������ �����մϴ�.
            ShowNotEnoughCoinsPopup();
        }
    }

    void AddItemToPlayer(int index)
    {
        // ���⿡ �������� �÷��̾�� �߰��ϴ� �ڵ带 �ۼ��մϴ�.
        // ���� ���, �������� �ε����� Ȯ���ϰ� �ش� �������� �÷��̾� �κ��丮�� �߰��ϴ� ���� ������ �����մϴ�.
        // �� �κ��� ���ӿ� ���� �ٸ��� ������ �� �ֽ��ϴ�.
    }

    void ShowNotEnoughCoinsPopup()
    {
        // ���⿡ ���� ���� �˾��� ���ų� Ư�� ȿ���� �ִ� �ڵ带 �ۼ��մϴ�.
        // �� �κ��� ���ӿ� ���� �ٸ��� ������ �� �ֽ��ϴ�.
    }
}
