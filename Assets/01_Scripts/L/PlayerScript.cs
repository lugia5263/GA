using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System.Linq;

public class PlayerScript : MonoBehaviourPunCallbacks, IPunObservable
{
    public float speed = 10.0f;

    private Vector3 currPos;
    private Quaternion currRot;

    private Transform tr;
    public PhotonView pv;
    public Text nickNameTxt;

    public ChatManager chatManager;

    private void Awake()
    {
        if (pv.IsMine)
        {
            chatManager = GameObject.Find("ChatManager").GetComponent<ChatManager>();
            if (chatManager != null)
            {
                chatManager.inputChat.text = "";
            }
        }
    }

    void Start()
    {
        tr = GetComponent<Transform>();
        pv = GetComponent<PhotonView>();

        chatManager = GameObject.Find("ChatManager").GetComponent<ChatManager>();
        if (chatManager != null)
        {
            chatManager.inputChat.enabled = false;
            chatManager.chatLog.text = "";
        }

        StartCoroutine(CheckEnterKey());
    }

    void Update()
    {
        if (pv.IsMine)
        {
            nickNameTxt.text = PhotonNetwork.NickName + " (나)";

            // 만약 채팅창이 활성화되어 있으면 플레이어를 움직이지 않음
            if (!chatManager.inputChat.isFocused)
            {
                float moveHorizontal = Input.GetAxis("Horizontal");
                float moveVertical = Input.GetAxis("Vertical");
                Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
                tr.Translate(movement * Time.deltaTime * speed);
                ChatterUpdate();
            }
        }
        else
        {
            nickNameTxt.text = pv.Owner.NickName;
            nickNameTxt.color = Color.red;

            if ((tr.position - currPos).sqrMagnitude >= 10.0f * 10.0f)
            {
                tr.position = currPos;
                tr.rotation = currRot;
            }
            else
            {
                tr.position = Vector3.Lerp(tr.position, currPos, Time.deltaTime * 10.0f);
                tr.rotation = Quaternion.Slerp(tr.rotation, currRot, Time.deltaTime * 10.0f);
            }
        }
    }

    #region 채팅 함수

    public void OnClickSendBtn()
    {
        if (chatManager.inputChat.text.Trim().Equals(""))
        {
            chatManager.inputChat.Select();
            chatManager.inputChat.enabled = false;
            return;
        }
        else
        {
            string msg = string.Format("[{0}] {1}", PhotonNetwork.LocalPlayer.NickName, chatManager.inputChat.text);
            Debug.Log(msg);
            photonView.RPC("ReceiveMsg", RpcTarget.AllBuffered, msg);
            chatManager.inputChat.ActivateInputField();
            chatManager.inputChat.text = "";
        }
    }

    void ChatterUpdate()
    {
        if (pv.IsMine)
        {
            string[] playerList = PhotonNetwork.PlayerList.Select(p => p.NickName).ToArray();
            string concatenatedPlayerList = string.Join("\n", playerList); // 들어온 순서대로 출력
            photonView.RPC("SyncChatterList", RpcTarget.AllBuffered, concatenatedPlayerList);
        }
    }

    [PunRPC]
    public void SyncChatterList(string playerList)
    {
        if (chatManager != null)
        {
            chatManager.chattingList.text = playerList;
        }
    }

    [PunRPC]
    public void ReceiveMsg(string msg)
    {
        if (chatManager != null)
        {
            chatManager.chatLog.text += "\n" + msg;
            chatManager.scroll_rect.verticalNormalizedPosition = 0.0f;
        }
    }

    IEnumerator CheckEnterKey()
    {
        while (true)
        {
            if (pv.IsMine && Input.GetKeyDown(KeyCode.Return))
            {
                if (chatManager != null && chatManager.inputChat != null)
                {
                    if (chatManager.inputChat.enabled == false)
                    {
                        Debug.Log("enter키 누름. 채팅창활성화 합니다..");
                        chatManager.inputChat.enabled = true;
                        chatManager.inputChat.ActivateInputField();

                        yield return null;
                    }
                    else
                    {
                        if (!chatManager.inputChat.isFocused)
                        {
                            Debug.Log("enter키 누름. Focused 가지고있음=깜빡이고있음=메세지입력가능");
                            OnClickSendBtn();
                        }
                        else
                        {
                            Debug.Log("enter키 누름. Focused 가지고있지않음=깜빡이지않음=메세지입력불가");
                            chatManager.inputChat.ActivateInputField();
                        }
                    }
                }
            }
            yield return null;
        }
    }
    #endregion

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(tr.position);
            stream.SendNext(tr.rotation);
        }
        else
        {
            currPos = (Vector3)stream.ReceiveNext();
            currRot = (Quaternion)stream.ReceiveNext();
        }
    }
}