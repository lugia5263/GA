using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class CharacterCreate : MonoBehaviour
{
    public GameObject selectPanel;
    public GameObject nickNamePanel;

    public InputField nickNameIF;
    public string characterNickName;

    public static string currentCharacterClass;

    public static int currentClassNum;
    public static int currentSlotNum;
    public Sprite[] sprites;
    public Button[] slots;

    public LoadPlayerInfo loadPlayerInfo;
    private int classNum;

    void Start()
    {
        selectPanel.SetActive(false);
        nickNamePanel.SetActive(false);

        loadPlayerInfo = GameObject.Find("LoadPlayerInfo").GetComponent<LoadPlayerInfo>();
    }

    // ���� ���� Ŭ�� ��, ĳ���ͻ�����ư Ŭ�� => �гζ���
    public void OnClickPanelActiveBtn()
    {
        currentSlotNum = SelectSlot.slotNum;
        // �Ҵ�� ���Թ�ȣ Ȯ��
        Debug.Log($"�����ϱ���, Selected Slot: {currentSlotNum+1}");
        selectPanel.SetActive(true);
    }

    //ĳ���ͻ������ ��ư
    public void OnClickCancelCharacterBtn()
    {
        selectPanel.SetActive(false);
    }

    // ������ ĳ���͸� ����, ������ư Ŭ�� => �г��� ���� �гζ���
    public void OnClickSelectAndCreateBtn()
    {
        currentClassNum = SelectChar.CharNum;
        Debug.Log($"Ŭ�����ѹ� : {currentClassNum+1}");
        nickNameIF.text = "";
        nickNamePanel.SetActive(true);
    }

    //�г��Ӱ������ϰ� �ڷΰ���(ĳ���� �ٽð��� �ʹٴ���)
    public void OnClickCancelNickBtn()
    {
        nickNamePanel.SetActive(false);
    }

    // �г��� �����Ϸ� ��ư
    public void OnClickDecideNickBtn()
    {
        currentSlotNum = SelectSlot.slotNum;
        characterNickName = nickNameIF.text;
        currentCharacterClass = SelectChar.currentCharacter;
        
        switch (currentCharacterClass) //Warrior, Gunner, Magician
        {
            case "Warrior":
                classNum = 0;
                break;
            case "Gunner":
                classNum = 1;
                break;
            case "Magician":
                classNum = 2;
                break;
            default:
                break;
        }
        Debug.Log(currentCharacterClass);
        StartCoroutine(CreateCharacter(currentSlotNum, characterNickName, currentCharacterClass, currentClassNum));
        Debug.Log("�г���, ĳ���ͻ��� �Ϸ�");
        loadPlayerInfo.LoadEverySlotData();

        // ���߿� �̺κ�(���Կ� sprite�ֱ�) ���ӷε��ϰ� ���Կ��� �ְԲ� �߰��غ���
        slots[currentSlotNum].GetComponent<Image>().sprite = sprites[currentClassNum];
        selectPanel.SetActive(false);
        nickNamePanel.SetActive(false);
    }

    IEnumerator CreateCharacter(int slotNum, string nickName, string className, int classNum)
    {
        Debug.Log(nickName);
        Debug.Log(className);
        PlayerPrefs.SetString($"SlotNum_{slotNum}", slotNum.ToString());

        PlayerPrefs.SetString($"{slotNum}_NickName", nickName);
        PlayerPrefs.SetString($"{slotNum}_Class", className);
        PlayerPrefs.SetInt($"{slotNum}_ClassNum", classNum);
        PlayerPrefs.SetInt($"{slotNum}_Level", 1);
        PlayerPrefs.SetInt($"{slotNum}_MaxHp", 500);
        PlayerPrefs.SetInt($"{slotNum}_WeaponLevel", 1);
        PlayerPrefs.SetInt($"{slotNum}_ATK", 50);
        PlayerPrefs.SetInt($"{slotNum}_CriticalPer", 50);
        PlayerPrefs.SetInt($"{slotNum}_UserGold", 0);
        PlayerPrefs.SetInt($"{slotNum}_Material", 0);
        PlayerPrefs.SetInt($"{slotNum}_ExpPotion", 0);

        PlayerPrefs.Save();

        yield return null;
    }

    public void DeleteCharacter()
    {
        int slotNum = SelectSlot.slotNum;
        Debug.Log(slotNum);

        if (PlayerPrefs.HasKey($"SlotNum_{slotNum}"))
        {
            PlayerPrefs.DeleteKey($"{slotNum}_NickName");
            PlayerPrefs.DeleteKey($"{slotNum}_Class");
            PlayerPrefs.DeleteKey($"{slotNum}_ClassNum");
            PlayerPrefs.DeleteKey($"{slotNum}_Level");
            PlayerPrefs.DeleteKey($"{slotNum}_MaxHp");
            PlayerPrefs.DeleteKey($"{slotNum}_WeaponLevel");
            PlayerPrefs.DeleteKey($"{slotNum}_ATK");
            PlayerPrefs.DeleteKey($"{slotNum}_CriticalPer");
            PlayerPrefs.DeleteKey($"{slotNum}_UserGold");
            PlayerPrefs.DeleteKey($"{slotNum}_Material");
            PlayerPrefs.DeleteKey($"{slotNum}_ExpPotion");

            PlayerPrefs.Save();

            loadPlayerInfo.LoadEverySlotData();
        }
        else
        {
            Debug.Log("���罽�Կ� Ű���� ����");
        }
    }

    // ���� �������� ������ �ٸ��ٸ�.. ���⼭ switch�� Ŭ������޾ƿͼ� createcharacter���� ȣ���ϰԲ��ϱ�
    //void CreateDefaultPlayerData(string userID)
    //{
        // ���÷� ���� 1�����϶��� �ɷ�ġ.. ������ ���鶧 �ʱ�ȭ�ϱ�
        //int defaultLevel = 1;
        //int defaultAtk = 10;

        //PlayerPrefs.SetInt(userID, defaultLevel);
        //PlayerPrefs.SetInt(userID, defaultAtk);
        //PlayerPrefs.Save();
    //}

    public void OnClickGoLoginSceneBtn() // �κ񿡼� ������� �����°� �߰��ϱ�
    {
        SceneManager.LoadScene("Login");
    }
}
