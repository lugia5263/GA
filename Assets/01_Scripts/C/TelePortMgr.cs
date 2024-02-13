using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelePortMgr : MonoBehaviour
{
    public Transform targetTransform; // �̵��Ϸ��� ����� Transform
    public float moveSpeed = 5f; // �̵� �ӵ�

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.position = targetTransform.position;
        }
    }

}