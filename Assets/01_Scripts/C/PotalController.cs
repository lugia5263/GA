using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using Photon.Pun;

public class PotalController : MonoBehaviourPunCallbacks
{
    public bool isPortal = false;
    public GameObject portalPanel;

    public GameObject skyPortal;

    private void Awake()
    {
        portalPanel = GameObject.Find("PortalPanel");
        skyPortal = GameObject.Find("SkyPortal");
        skyPortal.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PhotonView>().IsMine)
            {
                Debug.Log("충돌일어남");
                Portal();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PhotonView>().IsMine)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Portal();
            }
        }
    }
    public void Portal()
    {
        if (isPortal == false)
        {
            Jun_TweenRuntime[] gameObjects = portalPanel.GetComponents<Jun_TweenRuntime>();
            gameObjects[0].Play();
            isPortal = true;
            skyPortal.SetActive(true);
            skyPortal.GetComponent<Jun_TweenRuntime>().Play();
        }
        else
        {
            Jun_TweenRuntime[] gameObjects = portalPanel.GetComponents<Jun_TweenRuntime>();
            gameObjects[1].Play();
            isPortal = false;

            skyPortal.SetActive(false);
        }
    }
}