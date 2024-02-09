using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    public Transform player;
    public Animator anim;


    private void Start()
    {
        player = GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }

    public void SetForward()
    {
        player.transform.rotation = Quaternion.Euler(0, 0, 0);
        anim.SetBool("MoveTrigger", true);
    }

    public void SetBack()
    {
        player.transform.rotation = Quaternion.Euler(0, 180, 0);
        anim.SetBool("MoveTrigger", true);
    }

    public void SetLeft()
    {
        player.transform.rotation = Quaternion.Euler(0, -90, 0);
        anim.SetBool("MoveTrigger", true);
    }

    public void SetRight()
    {
        player.transform.rotation = Quaternion.Euler(0, 90, 0);
        anim.SetBool("MoveTrigger", true);
    }



    public void SetRun()
    {
        anim.SetBool("RunTrigger", true);
    }

    public void SetReturn()
    {
        anim.SetBool("MoveTrigger", false);
    }

    public void SetNormal()
    {
        anim.SetBool("RunTrigger", false);
    }
    public void SetJump()
    {
        anim.SetBool("Jump", true);
    }

}
