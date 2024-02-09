using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCollider : MonoBehaviour
{
    public Collider lumicollider;
    public GameObject talkBtn;
    public GameObject talkMent;

    private void Start()
    {
        talkBtn.SetActive(false);
        talkMent.SetActive(false);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        talkBtn.SetActive(true);
        Debug.Log("¸¸³µÀ½");
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("f´­·¶´Ù!!");
            talkMent.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("³ª°¬À½");
        talkBtn.SetActive(false);
        talkMent.SetActive(false);
    }
}
