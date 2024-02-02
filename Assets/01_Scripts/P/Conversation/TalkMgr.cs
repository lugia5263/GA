using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TalkMgr : MonoBehaviour
{
    public Text textName; // �̸�
    public Text textSentence; // ����
    //public Image faceplace; // ��
    public GameObject conversationPanel;
    public GameObject nextBtn; // Ÿ�Ӷ��� ������ ��ư������

    Queue<string> naming = new Queue<string>();
    Queue<string> sentences = new Queue<string>();
    List<Sprite> faceImages = new List<Sprite>();

    private Dialogue currentDialogue;

    void Awake()
    {
        nextBtn = GameObject.Find("NextBtn");
        conversationPanel = GameObject.Find("Conversation");
    }
    private void Start()
    {
        conversationPanel.SetActive(false);        
    }

    public void Begin(Dialogue info)
    {
        //Jun_TweenRuntime[] gameObjects = conversationPanel.GetComponents<Jun_TweenRuntime>();
        // gameObjects[0].Play();

        currentDialogue = info;
        conversationPanel.SetActive(true); //��������
        naming.Clear();
        sentences.Clear();
        //faceImages.Clear();

        textName.text = null;
        //faceplace.sprite = null;

        foreach (var sentence in info.name)
        {
            naming.Enqueue(sentence);
        }


        foreach (var sentence in info.sentences)
        {
            sentences.Enqueue(sentence);
        }

        //foreach (var image in info.faceImages)
        //{
        //    faceImages.Add(image);
        //}

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
        //if (faceImages.Count > 0)
        //{
        //    faceplace.sprite = faceImages[0];
        //    faceImages.RemoveAt(0);
        //}
        StartCoroutine(TypeName(naming.Dequeue()));
        StartCoroutine(TypeSentence(sentences.Dequeue()));
    }
    IEnumerator TypeName(string namer)
    {
        textName.text = namer;
        if (textName.text == "���")
        {
            Color plumColor = new Color(221f / 255f, 160f / 255f, 221f / 255f);
            textName.color = plumColor;
        }
        if (textName.text == "����")
        {
            textName.color = Color.yellow;
        }
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
        conversationPanel.gameObject.SetActive(false);
        //Jun_TweenRuntime[] gameObjects = conversationPanel.GetComponents<Jun_TweenRuntime>();
        //gameObjects[1].Play();
    }
    public void RealEnd()
    {
        conversationPanel.SetActive(false);
        Debug.Log("��!");
        textSentence.text = string.Empty;
    }



    public void NextBtnClose()
    {
        nextBtn.SetActive(false);
    }
}
