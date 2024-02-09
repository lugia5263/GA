using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;







// private void OnTriggerStay(Collider other)
//{
//   if(other.gameObject.CompareTag("Player"))
//   {
//       other.transform.position = teleportPos.position;
//   }
//}

//     loadPanel.fillAmount = -Time.deltaTime;
// if(loadPanel.fillAmount == 0)
//  {
//          Destroy(loadPanel);
//  }
public class MoveTowardsOtherTransform : MonoBehaviour
{
    public Transform targetTransform; // �̵��Ϸ��� ����� Transform
    public float moveSpeed = 5f; // �̵� �ӵ�

    void Update()
    {
        // ���� targetTransform�� �������� �ʴ´ٸ� ����
        if (targetTransform == null)
        {
            Debug.LogWarning("Target Transform is not assigned.");
            return;
        }

        // ���� ��ġ���� ��ǥ ��ġ���� ������ ���
        Vector3 direction = targetTransform.position - transform.position;

        // ��ǥ ��ġ�� �����ߴ��� �˻��ϰ� �̵� �Ÿ��� ���
        float distanceToMove = Mathf.Min(moveSpeed * Time.deltaTime, direction.magnitude);

        // �̵� �Ÿ��� 0���� Ŭ ��쿡�� �̵�
        if (distanceToMove > 0f)
        {
            // ������ �̵�
            transform.position += direction.normalized * distanceToMove;
        }
    }
}

