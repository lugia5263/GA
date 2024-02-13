using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue info;

    public void Trigger()
    {
        var system = FindObjectOfType<TalkMgr>();
        system.Begin(info);
    }

    public void EndTrigger()
    {
        var system = FindObjectOfType<TalkMgr>();
        system.RealEnd();
    }
}
// �ش� ��ü�� ��ũ��Ʈ�� Trigger �Լ��� �ҷ����� ��ũ��Ʈ�� �ִ� ��ȭ�� ������� ������.