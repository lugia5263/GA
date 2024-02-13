using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDependentComponent : MonoBehaviour
{
    public ChatManager componentA;

    void Awake()
    {
        componentA = GetComponent<ChatManager>();
        // ���� �� �̸� ��������
        string currentSceneName = SceneManager.GetActiveScene().name;
        Debug.Log("���� �� �̸� : "+currentSceneName);

        // ���� ���� ���� ������Ʈ A�� B�� Ȱ��ȭ �Ǵ� ��Ȱ��ȭ
        if (currentSceneName == "Town" || currentSceneName == "Raid")
        {
            // �� A������ ������Ʈ A Ȱ��ȭ, ������Ʈ B ��Ȱ��ȭ
            componentA.enabled = true;

        }
        else // Ȩ�� �ƴҶ�, �ַδ����̳� ���̵�����϶�
        {
            // �� B������ ������Ʈ A ��Ȱ��ȭ, ������Ʈ B Ȱ��ȭ
            componentA.enabled = false;

        }
    }
}
