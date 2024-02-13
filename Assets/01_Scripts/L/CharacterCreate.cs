using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CharacterCreate : MonoBehaviour
{
    public GameObject selectPanel;
    public GameObject nickNamePanel;

    public InputField nickNameIF;
    public string characterNickName;

    public static string currentCharacterClass;

    public static int currentClassNum;
    public static int currentSlotNum;

    public LoadPlayerInfo loadPlayerInfo;
    public Button[] slots;

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
                currentClassNum = 0;
                break;
            case "Gunner":
                currentClassNum = 1;
                break;
            case "Magician":
                currentClassNum = 2;
                break;
            default:
                break;
        }
        Debug.Log(currentCharacterClass);
        StartCoroutine(CreateCharacter(currentSlotNum, characterNickName, currentCharacterClass, currentClassNum));
        Debug.Log("�г���, ĳ���ͻ��� �Ϸ�");
        loadPlayerInfo.LoadEverySlotData();
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
        switch (classNum)
        {
            case 0:
                PlayerPrefs.SetFloat($"{slotNum}_MaxHp", 1500);
                PlayerPrefs.SetFloat($"{slotNum}_Hp", 1500);
                PlayerPrefs.SetInt($"{slotNum}_CriChance", 50);
                break;
            case 1:
                PlayerPrefs.SetFloat($"{slotNum}_MaxHp", 1000);
                PlayerPrefs.SetFloat($"{slotNum}_Hp", 1000);
                PlayerPrefs.SetInt($"{slotNum}_CriChance", 30);
                break;
            case 2:
                PlayerPrefs.SetFloat($"{slotNum}_MaxHp", 1000);
                PlayerPrefs.SetFloat($"{slotNum}_Hp", 1000);
                PlayerPrefs.SetInt($"{slotNum}_CriChance", 50);
                break;
            default:
                break;
        }
        PlayerPrefs.SetInt($"{slotNum}_Level", 1);
        PlayerPrefs.SetInt($"{slotNum}_Exp", 0);
        PlayerPrefs.SetInt($"{slotNum}_WeaponLevel", 1);
        PlayerPrefs.SetInt($"{slotNum}_AttackPower", 150);
        PlayerPrefs.SetFloat($"{slotNum}_CriDamage", 120f);
        PlayerPrefs.SetInt($"{slotNum}_UserGold", 10000);
        PlayerPrefs.SetInt($"{slotNum}_Material", 100);
        PlayerPrefs.SetInt($"{slotNum}_ExpPotion", 1000);
        PlayerPrefs.SetInt($"{slotNum}_QusetIdx", 0);
        PlayerPrefs.SetString($"{slotNum}_GoalTxt", "");
        PlayerPrefs.SetInt($"{slotNum}_QuestCurCnt", 0);
        PlayerPrefs.SetInt($"{slotNum}_QuestMaxCnt", 0);
        PlayerPrefs.SetInt($"{slotNum}_IsFirst", 1);

        PlayerPrefs.Save();

        yield return null;
    }

    public void DeleteCharacter()
    {
        int slotNum = SelectSlot.slotNum;
        Debug.Log(slotNum);
        slots[slotNum].GetComponent<RawImage>().texture = null;
        if (PlayerPrefs.HasKey($"SlotNum_{slotNum}"))
        {
            PlayerPrefs.DeleteKey($"{slotNum}_NickName");
            PlayerPrefs.DeleteKey($"{slotNum}_Class");
            PlayerPrefs.DeleteKey($"{slotNum}_ClassNum");
            PlayerPrefs.DeleteKey($"{slotNum}_Level");
            PlayerPrefs.DeleteKey($"{slotNum}_MaxHp");
            PlayerPrefs.DeleteKey($"{slotNum}_Hp");
            PlayerPrefs.DeleteKey($"{slotNum}_WeaponLevel");
            PlayerPrefs.DeleteKey($"{slotNum}_AttackPower");
            PlayerPrefs.DeleteKey($"{slotNum}_CriChance");
            PlayerPrefs.DeleteKey($"{slotNum}_CriDamage");
            PlayerPrefs.DeleteKey($"{slotNum}_UserGold");
            PlayerPrefs.DeleteKey($"{slotNum}_Material");
            PlayerPrefs.DeleteKey($"{slotNum}_ExpPotion");
            PlayerPrefs.DeleteKey($"{slotNum}_QuestIdx");
            PlayerPrefs.DeleteKey($"{slotNum}_GoalTxt");
            PlayerPrefs.DeleteKey($"{slotNum}_QuestCurCnt");
            PlayerPrefs.DeleteKey($"{slotNum}_QuestMaxCnt");
            PlayerPrefs.DeleteKey($"{slotNum}_IsCompleted");
            PlayerPrefs.DeleteKey($"{slotNum}_IsDoing");
            PlayerPrefs.DeleteKey($"{slotNum}_IsFirst");

            PlayerPrefs.Save();

            loadPlayerInfo.LoadEverySlotData();
        }
        else
        {
            Debug.Log("���罽�Կ� Ű���� ����");
        }
    }
}
