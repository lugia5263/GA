using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPScontroller  : MonoBehaviour
{
    public Transform players;
    public Transform CameraArm;

    float DeshCool;
    float CurDeshCool = 8f;

    CharacterController characterController;
    Player player;
    Animator animator;
    
    
    void Start()
    {
        animator = players.GetComponent<Animator>();
        characterController = players.GetComponent<CharacterController>();
        player = GetComponentInChildren<Player>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moves();
        lookAround();
    }
    void moves()
    {
      
        if (player.isFireReady == false)
            return;
        if (player.downing == true)
            return;
        if (player.isDeath == true)
            return;

            Vector2 moveinput = new Vector2(Input.GetAxis("Horizontal") * Time.deltaTime* 1.5f, Input.GetAxis("Vertical") * Time.deltaTime* 1.5f ) ;
        bool ismove = moveinput.magnitude != 0;
        animator.SetBool("isRun", ismove);
        

        if (ismove)
        {
            Vector3 lookForward = new Vector3(CameraArm.forward.x, 0f, CameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(CameraArm.right.x, 0f, CameraArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveinput.y + lookRight * moveinput.x;

            players.forward = moveDir;
            transform.position += moveDir * Time.deltaTime * 0.01f ;
            characterController.Move(moveDir * 5f ) ;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                transform.Translate(0, 0, Time.deltaTime * 10f);
            }
        }

        
    }
    void lookAround()
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
