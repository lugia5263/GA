using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerd : MonoBehaviour
{
    public QuestPopUpManager questPopUpManager;
    public DataMgrDontDestroy dataMgrDontDestroy;

    [Header("NPC ��ȭ")]
    public DialogueTrigger dialogueTrigger; //�뺻
    public GameObject nPCConversation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PhotonView>().IsMine)
            {
                dialogueTrigger.Trigger();
                nPCConversation.SetActive(true);
                if (dataMgrDontDestroy.QuestCurCnt < dataMgrDontDestroy.QuestMaxCnt)
                {
                    dataMgrDontDestroy.QuestCurCnt++;
                    if(dataMgrDontDestroy.QuestCurCnt == dataMgrDontDestroy.QuestMaxCnt)
                    {
                        dataMgrDontDestroy.IsCompleted = true;
                    }
                }
                else
                {
                    return;
                }
                questPopUpManager.UpdateQuestStatus();
                Debug.Log("����");
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        nPCConversation.SetActive(false);
    }
    void Start()
    {
        dataMgrDontDestroy = DataMgrDontDestroy.Instance;
    }

    void Update()
    {
        
    }
}
