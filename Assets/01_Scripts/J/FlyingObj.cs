using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObj : MonoBehaviour
{
    public float floatSpeed = 1.0f; // ���� � �ӵ�
    public float floatHeight = 1.0f; // ��ü�� �� �ִ� ����

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // ��ü�� ������ �ӵ��� ���� ���Ŵ
        transform.position = startPosition + new Vector3(0.0f, Mathf.Sin(Time.time * floatSpeed) * floatHeight, 0.0f);
    }
}
