using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public class ChatManager : MonoBehaviourPunCallbacks
{
    public List<string> chatList = new List<string>();
    public Text chatLog;
    public Text chattingList;
    public InputField inputChat;
    public ScrollRect scroll_rect;
    public string chatters;
}