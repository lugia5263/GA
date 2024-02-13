using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System.Linq;

public class PlayerScript : MonoBehaviourPunCallbacks, IPunObservable
{
    //Ŭ���� ����� �޴� ���� ����
    public Vector3 currPos;
    public Quaternion currRot;

    public float speed = 10.0f;

    private Transform tr;
    public PhotonView pv;
    public Text nickNameTxt;

    public ChatManager chatManager;

    public bool allowMove = true;

    void Start()
    {
        tr = GetComponent<Transform>();
        pv = GetComponent<PhotonView>();

        chatManager = GetComponent<ChatManager>();
        Canvas nickCanvas = GetComponentInChildren<Canvas>();
        nickNameTxt = nickCanvas.GetComponentInChildren<Text>();

        chatManager.StartCoroutine(chatManager.CheckEnterKey());
    }

    void Update()
    {
        if(pv.IsMine)
        {
            nickNameTxt.text = PhotonNetwork.NickName + " (��)";
            //���� ä��â�� Ȱ��ȭ�Ǿ� ������ �÷��̾ �������� ����
            if (allowMove)
            {
                float moveHorizontal = Input.GetAxis("Horizontal");
                float moveVertical = Input.GetAxis("Vertical");
                Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
                tr.Translate(movement * Time.deltaTime * speed);
            }
        }
        else
        {
            nickNameTxt.text = pv.Owner.NickName;
            nickNameTxt.color = Color.red;
            //������ �ð��� �ʹ� �� ���(�ڷ���Ʈ)
            if ((tr.position - currPos).sqrMagnitude >= 10.0f * 10.0f)
            {
                tr.position = currPos;
                tr.rotation = currRot;
            }
            //������ �ð��� ª�� ���(�ڿ������� ���� - ���巹Ŀ��)
            else
            {
                tr.position = Vector3.Lerp(tr.position, currPos, Time.deltaTime * 10.0f);
                tr.rotation = Quaternion.Slerp(tr.rotation, currRot, Time.deltaTime * 10.0f);
            }
        }
    }


    // ��� �ۼ���
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //����� ������ 
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }

        //Ŭ���� ����� �޴� 
        else
        {
            currPos = (Vector3)stream.ReceiveNext();
            currRot = (Quaternion)stream.ReceiveNext();
        }
    }
}