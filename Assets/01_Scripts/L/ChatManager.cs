using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public class ChatManager : MonoBehaviourPunCallbacks, IPunObservable
{
    public List<string> chatList = new List<string>();

    public Text chatLog; // viewport�� ä�÷α�
    //public Text chatterList; // ä�͸���Ʈ
    public InputField inputChat; // ��ǲ�ʵ�
    public ScrollRect scrollRect;
    //public string chatters;

    public Text nickNameTxt;

    public PhotonView pv;
    public Player player;
    // ���� ������ �߰��Ѱ�. ��â

    private void Start()
    {
        pv = GetComponent<PhotonView>();
        player = GetComponent<Player>();
        // ���� ������ �߰��Ѱ�. ��â

        GameObject canvasObj = GameObject.Find("WorldCanvas");
        GameObject backChSelect = canvasObj.transform.Find("BackChSelect_Chat").gameObject;
        GameObject chattingBox = backChSelect.transform.Find("ChattingBox").gameObject;
        scrollRect = chattingBox.GetComponentInChildren<ScrollRect>();

        //chatterList = chattingBox.GetComponentInChildren<Text>();
        inputChat = chattingBox.GetComponentInChildren<InputField>();
        chatLog = scrollRect.content.GetComponentInChildren<Text>();

        Canvas nickCanvas = GetComponentInChildren<Canvas>();
        nickNameTxt = nickCanvas.GetComponentInChildren<Text>();

        inputChat.enabled = false;
        chatLog.text = "";

        StartCoroutine(CheckEnterKey());
    }

    public void Update()
    {
        //ChatterUpdate();

        if (pv.IsMine)
        {
            if (!inputChat.isFocused)
            {
                player.allowMove = true; // �������� ������ �����ϼ��ִ�.
            }
            else
            {
                player.allowMove = false; // �����̴� ��������
            }
        }
    }


    #region ä�� �Լ�
    public void OnClickSendBtn()
    {
        if (inputChat.text.Trim().Equals(""))
        {
            Debug.Log("ä��â Empty, ä��â�� ��Ȱ��ȭ �մϴ�");
            // ä��â ��������� ��Ȱ��ȭ
            inputChat.Select();
            inputChat.enabled = false;
            return;
        }
        else
        {
            string msg = string.Format("[{0}] {1}", PhotonNetwork.LocalPlayer.NickName, inputChat.text);
            Debug.Log(msg);
            photonView.RPC("ReceiveMsg", RpcTarget.AllBuffered, msg);
            inputChat.ActivateInputField(); // �޼����� ������ Ȱ��ȭ
            inputChat.text = "";
        }
    }

    //void ChatterUpdate()
    //{
    //    if (pv.IsMine)
    //    {
    //        string[] playerList = PhotonNetwork.PlayerList.Select(p => p.NickName).ToArray();
    //        string concatenatedPlayerList = string.Join("\n", playerList); // ���� ������� ���
    //        photonView.RPC("SyncChatterList", RpcTarget.AllBuffered, concatenatedPlayerList);
    //    }
    //}

    //[PunRPC]
    //public void SyncChatterList(string playerList)
    //{
    //    chatterList.text = playerList;
    //}

    [PunRPC]
    public void ReceiveMsg(string msg)
    {
        chatLog.text += "\n" + msg;
        scrollRect.verticalNormalizedPosition = 0.0f;
    }

    public IEnumerator CheckEnterKey()
    {
        while (true)
        {
            if (pv.IsMine && Input.GetKeyDown(KeyCode.Return))
            {
                if (inputChat != null)
                {
                    if (inputChat.enabled == false)
                    {
                        Debug.Log("enterŰ ����. ä��âȰ��ȭ �մϴ�..");
                        inputChat.enabled = true;
                        inputChat.ActivateInputField();

                        yield return null;
                    }
                    else
                    {
                        if (!inputChat.isFocused)
                        {
                            Debug.Log("enterŰ ����. Focused ����������=�����̰�����=�޼����Է°���");
                            OnClickSendBtn();
                        }
                        else
                        {
                            Debug.Log("enterŰ ����. Focused ��������������=������������=�޼����ԷºҰ�");
                            inputChat.ActivateInputField();
                        }
                    }
                }
            }
            yield return null;
        }
    }
    #endregion
    // IPunObservable ����
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // ���⼭�� ��� �޽����� ���� �ʿ䰡 �����Ƿ� �ƹ� �۾��� ���� �ʽ��ϴ�.
        }
        else
        {
            // ���⼭�� ��� �޽����� ���� �ʿ䰡 �����Ƿ� �ƹ� �۾��� ���� �ʽ��ϴ�.
        }
    }
}