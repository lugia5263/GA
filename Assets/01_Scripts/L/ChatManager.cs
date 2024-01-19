using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class ChatManager : MonoBehaviourPunCallbacks
{
    public List<string> chatList = new List<string>();
    public Button sendBtn;
    public Text chatLog;
    public Text chattingList;
    public InputField inputChat;
    public ScrollRect scroll_rect;
    public string chatters;

//    void Start()
//    {
//        PhotonNetwork.IsMessageQueueRunning = true;
//        //scroll_rect = GameObject.FindObjectOfType<ScrollRect>();
//    }

//    public void OnClickSendBtn()
//    {
//        if(inputChat.text.Equals(""))
//        {
//            Debug.Log("Empty, 채팅창을 비활성화 합니다");
//            // 채팅창 비어있으니 비활성화
//            inputChat.Select();
//            return; 
//        }
//        string msg = string.Format("[{0}] {1}", PhotonNetwork.LocalPlayer.NickName, inputChat.text);
//        Debug.Log(msg);
//        photonView.RPC("ReceiveMsg", RpcTarget.OthersBuffered, msg);
//        inputChat.ActivateInputField(); // 메세지를 보내도 활성화
//        inputChat.text = "";
//    }

//    void Update()
//    {
//        ChatterUpdate();
//        // enter키를 눌렀고 inputChat에 포커스를 가지고있을때 실행(isFocused는 가지고있지않을때 true를 반환함)
//        if (Input.GetKeyDown(KeyCode.Return) && !inputChat.isFocused)
//        {
//            Debug.Log("enter키 누름, msg전송");
//            OnClickSendBtn();
//        }
//        if(Input.GetKeyDown(KeyCode.Return) && inputChat.isFocused)
//        {
//            Debug.Log("enter키 누름 Focused 가지고있지않음");
//        }
//    }

//    void ChatterUpdate()
//    {
//        chatters = "PlayerList\n";
//        foreach(Player p in PhotonNetwork.PlayerList)
//        {
//            chatters += p.NickName + "\n";
//        }
//        chattingList.text = chatters;
//    }

//    [PunRPC]
//    public void ReceiveMsg(string msg)
//    {
//        chatLog.text += "\n" + msg;
//        scroll_rect.verticalNormalizedPosition = 0.0f;
//    }
}
