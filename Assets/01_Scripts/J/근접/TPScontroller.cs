using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using UnityEngine.UI;

public class TPScontroller  : MonoBehaviourPunCallbacks, IPunObservable
{

    private Vector3 currPos;
    private Quaternion currRot;
    private Transform tr;
    //private Text nickNameTxt;
    public PhotonView pv;
    public Transform players;
    public Transform CameraArm;
    Vector3 velocity;
    float DeshCool;
    float CurDeshCool = 8f;
    float gravity = -9.8f;
    public bool isJumping;
    public bool isGrounded;
    public CharacterController characterController;
    Player player;
    Animator animator;
    public float rotationSpeed = 5f;
    public float speed = 1;

    void Start()
    {
        players = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = players.GetComponent<Animator>();
        characterController = GetComponentInChildren<CharacterController>();
        player = GetComponentInChildren<Player>();
        animator = GetComponentInChildren<Animator>();
        player = GetComponentInChildren<Player>();
        tr = GetComponent<Transform>();
        pv = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        //players = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        moves();
        lookAround();

        if (!player.characterController.isGrounded)
        { 
            if(isJumping)
            {
                isJumping = false;
            }

            //velocity.y = 0;
            velocity.y += gravity * Time.deltaTime;
            player.characterController.Move(velocity * Time.deltaTime);
        }
       else if(isJumping)
        {
            isGrounded = false;
            velocity.y += 7f;
            player.characterController.Move(velocity * Time.deltaTime);
        }

       if(Input.GetKeyDown(KeyCode.C))
        {
            isJumping = true;
        }
    }

    void moves()
    {
        if (pv.IsMine)
        {
            if (player.skillUse == true)
                return;
            if (player.isFireReady == false)
                return;
            if (player.downing == true)
                return;
            if (player.isDeath == true)
                return;
            if (player.downing)
                return;

            Vector2 moveinput = new Vector2(Input.GetAxis("Horizontal") * Time.deltaTime * 1.5f, Input.GetAxis("Vertical") * Time.deltaTime * 1.5f);
            bool ismove = moveinput.magnitude != 0;
            animator.SetBool("isRun", ismove);



            if (ismove)
            {
                Vector3 lookForward = new Vector3(CameraArm.forward.x, 0f, CameraArm.forward.z).normalized;
                Vector3 lookRight = new Vector3(CameraArm.right.x, 0f, CameraArm.right.z).normalized;
                Vector3 moveDir = lookForward * moveinput.y + lookRight * moveinput.x;

                players.forward = moveDir;
                transform.position += moveDir * Time.deltaTime * 0.01f;
                characterController.Move(moveDir * 5f);
            }
        }
    }
    void lookAround()
    {
        if (pv.IsMine)
        {
            Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            Vector3 camAngle = CameraArm.rotation.eulerAngles;
            float x = camAngle.x - mouseDelta.y;
            if (x < 180f)
            {
                x = Mathf.Clamp(x, -1f, 70f);
            }
            else
            {
                x = Mathf.Clamp(x, 335f, 361f);
            }
            CameraArm.rotation = Quaternion.Euler(0, camAngle.y + mouseDelta.x, camAngle.z);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //통신을 보내는 
        if (stream.IsWriting)
        {
            stream.SendNext(tr.position);
            stream.SendNext(tr.rotation);
        }

        //클론이 통신을 받는 
        else
        {
            currPos = (Vector3)stream.ReceiveNext();
            currRot = (Quaternion)stream.ReceiveNext();
        }
    }
}
