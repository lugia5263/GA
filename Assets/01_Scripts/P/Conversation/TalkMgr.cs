using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TalkMgr : MonoBehaviour
{
    public DataMgrDontDestroy dataMgrDontDestroy;
    public Text textName; // �̸�
    public Text textSentence; // ����
    public GameObject nPCConversation;
    public GameObject nextBtn;

    Queue<string> naming = new Queue<string>();
    Queue<string> sentences = new Queue<string>();

    private Dialogue currentDialogue;

    private void Start()
    {
        dataMgrDontDestroy = DataMgrDontDestroy.Instance;
    }

    public void Begin(Dialogue info)
    {
        currentDialogue = info;
        nPCConversation.SetActive(true); 
        naming.Clear();
        sentences.Clear();
        textName.text = null;

        foreach (var sentence in info.name)
        {
            naming.Enqueue(sentence);
        }
        foreach (var sentence in info.sentences)
        {
            sentences.Enqueue(sentence);
        }
        Next();
    }

    public void Next()
    {
        // Ÿ�Ӷ��� ���� ��!!
        Debug.Log("�̰� ��?");
        if (naming.Count == 0)
        {
            End();
            return;
        }
        if (sentences.Count == 0)
        {
            End();
            return;
        }

        textName.text = string.Empty;
        textSentence.text = string.Empty;
        StopAllCoroutines();
        StartCoroutine(TypeName(naming.Dequeue()));
        StartCoroutine(TypeSentence(sentences.Dequeue()));
    }
    IEnumerator TypeName(string name)
    {
        textName.text = name;
        yield return null;
    }
    IEnumerator TypeSentence(string sentence)
    {
        foreach (var letter in sentence)
        {
            textSentence.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void End()
    {
        if (nPCConversation != null)
        {
            nPCConversation.gameObject.SetActive(false);
        }
    }
    public void RealEnd()
    {
        nPCConversation.SetActive(false);
        Debug.Log("��!");
        textSentence.text = string.Empty;
    }
}
