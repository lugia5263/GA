using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlotClass
{
    Slot0, Slot1, Slot2
}

public class SelectSlot : MonoBehaviour
{
    public string currentSlot = null;
    public static int slotNum;
    public SlotClass slot;
    public SelectSlot[] chars;

    public void OnClickSlotBtn()
    {
        // �ش� ��ư�� slot�� ���� ��ũ��Ʈ�� currentSlot�� �Ҵ�
        currentSlot = slot.ToString();
        // ���Թ�ȣ�� �ٸ���ũ��Ʈ�� �Ѱ��ֱ�
        switch (currentSlot)
        {
            case "Slot0":
                slotNum = 0;
                break;
            case "Slot1":
                slotNum = 1;
                break;
            case "Slot2":
                slotNum = 2;
                break;
            default:
                break;
        }
        // �Ҵ�� ���Թ�ȣ Ȯ��
        Debug.Log("Selected Slot: " + currentSlot);
    }
}
