using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadPlayerInfo : MonoBehaviour
{

    public int currentSlotNum;

    private DataMgrDontDestroy dataMgrDontDestroy;
    public string nickName;
    public int level;
    public int exp;
    public float maxhp;
    public float hp;
    public int attackPower;
    public int weaponLevel;
    public int criChance;
    public float criDamage;
    public int userGold;
    public int userMaterial;
    public int userExpPotion;

    public int questIdx;
    public int questCurCnt;
    public int questMaxCnt;
    public string goalTxt;

    public Button[] slots;
    public Texture[] textures;
    public Text[] slot1Text;
    public Text[] slot2Text;
    public Text[] slot3Text;

    public CharacterCreate characterCreate;

    private void Start()
    {
        characterCreate = GameObject.Find("CharacterCreate").GetComponent<CharacterCreate>();
        dataMgrDontDestroy = DataMgrDontDestroy.Instance;

        LoadEverySlotData();
    }

    public void LoadEverySlotData()
    {
        for (int slotNum = 0; slotNum < 3; slotNum++)
        {
            Debug.Log($"LoadEverySlotData�� for�� {slotNum}��°");

            if (PlayerPrefs.HasKey($"SlotNum_{slotNum}"))
            {
                string nickName = PlayerPrefs.GetString($"{slotNum}_NickName");
                string className = PlayerPrefs.GetString($"{slotNum}_Class");
                int level = PlayerPrefs.GetInt($"{slotNum}_Level");

                switch (slotNum)
                {
                    case 0:
                        LoadTextrues(0, className);
                        slot1Text[0].text = nickName;
                        slot1Text[1].text = className;
                        if (level == 0)
                        {
                            slot1Text[2].text = "";
                        }
                        else
                        {
                            slot1Text[2].text = level.ToString();
                        }
                        break;
                    case 1:
                        LoadTextrues(1, className);
                        slot2Text[0].text = nickName;
                        slot2Text[1].text = className;
                        if (level == 0)
                        {
                            slot2Text[2].text = "";
                        }
                        else
                        {
                            slot2Text[2].text = level.ToString();
                        }
                        break;
                    case 2:
                        LoadTextrues(2, className);
                        slot3Text[0].text = nickName;
                        slot3Text[1].text = className;
                        if (level == 0)
                        {
                            slot3Text[2].text = "";
                        }
                        else
                        {
                            slot3Text[2].text = level.ToString();
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void LoadTextrues(int slotNum, string className)
    {
        switch (className)
        {
            case "Warrior":
                slots[slotNum].GetComponent<RawImage>().texture = textures[0];
                break;
            case "Gunner":
                slots[slotNum].GetComponent<RawImage>().texture = textures[1];
                break;
            case "Magician":
                slots[slotNum].GetComponent<RawImage>().texture = textures[2];
                break;
            default:
                break;
        }
    }

    public void LoadCurrnetPlayerData()
    {
        currentSlotNum = SelectSlot.slotNum;
        Debug.Log("�����͸� �ҷ��� ���� ������ ��ȣ�� " + currentSlotNum);
        // Start��ư�� ��������, currentSlotNum�� �ִ� ������ �����ͼ� dataMgr�� �־��ش�.
        #region ĳ�������� ������ ���
        nickName = PlayerPrefs.GetString($"{currentSlotNum}_NickName"); //�ʿ��ϸ� ����
        level = PlayerPrefs.GetInt($"{currentSlotNum}_Level");
        exp = PlayerPrefs.GetInt($"{currentSlotNum}_Exp");
        maxhp = PlayerPrefs.GetFloat($"{currentSlotNum}_MaxHp");
        hp = PlayerPrefs.GetFloat($"{currentSlotNum}_Hp");
        attackPower = PlayerPrefs.GetInt($"{currentSlotNum}_AttackPower");
        weaponLevel = PlayerPrefs.GetInt($"{currentSlotNum}_WeaponLevel");
        criChance = PlayerPrefs.GetInt($"{currentSlotNum}_CriChance");
        criDamage = PlayerPrefs.GetFloat($"{currentSlotNum}_CriDamage");
        userGold = PlayerPrefs.GetInt($"{currentSlotNum}_UserGold");
        userMaterial = PlayerPrefs.GetInt($"{currentSlotNum}_Material");
        userExpPotion = PlayerPrefs.GetInt($"{currentSlotNum}_ExpPotion");
        #endregion

        #region ĳ��������. ������ ���� �̱��濡 �����ֱ�
        dataMgrDontDestroy.NickName = nickName;
        dataMgrDontDestroy.Level = level;
        dataMgrDontDestroy.Exp = exp;
        dataMgrDontDestroy.MaxHp = maxhp;
        dataMgrDontDestroy.Hp = hp;
        dataMgrDontDestroy.AttackPower = attackPower;
        dataMgrDontDestroy.WeaponLevel = weaponLevel;
        dataMgrDontDestroy.CriChance = criChance;
        dataMgrDontDestroy.CriDamage = criDamage;
        dataMgrDontDestroy.UserGold = userGold;
        dataMgrDontDestroy.UserMaterial = userMaterial;
        dataMgrDontDestroy.UserExpPotion = userExpPotion;
        #endregion

        #region ����Ʈ ���� ������ ���
        questIdx = PlayerPrefs.GetInt($"{currentSlotNum}_QuestIdx");
        questCurCnt = PlayerPrefs.GetInt($"{currentSlotNum}_QuestCurCnt");
        questMaxCnt = PlayerPrefs.GetInt($"{currentSlotNum}_QuestMaxCnt");
        goalTxt = PlayerPrefs.GetString($"{currentSlotNum}_GoalTxt");
        #endregion

        #region ����Ʈ����. ������ ���� �̱��濡 �����ֱ�
        dataMgrDontDestroy.QuestIdx = questIdx;
        dataMgrDontDestroy.QuestCurCnt = questCurCnt;
        dataMgrDontDestroy.QuestMaxCnt = questMaxCnt;
        dataMgrDontDestroy.GoalTxt = goalTxt;
        #endregion
    }
}