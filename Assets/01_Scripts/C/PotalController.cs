using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class PotalController : MonoBehaviour
{
    public bool isPotal = false;
    public GameObject potalPanel;

    public GameObject bigPortal;

    private void Awake()
    {
        bigPortal = GameObject.Find("Portal red");
        bigPortal.SetActive(false);
    }
    void Start()
    {
        potalPanel = GameObject.Find("PortalPanel");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Potal();
            bigPortal.SetActive(true);
            bigPortal.GetComponent<Jun_TweenRuntime>().Play();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bigPortal.SetActive(false);
            Potal();
        }
    }
    public void Potal()
    {
        if (isPotal == false)
        {
            Jun_TweenRuntime[] gameObjects = potalPanel.GetComponents<Jun_TweenRuntime>();
            gameObjects[0].Play();
            isPotal = true;
        }
        else
        {
            Jun_TweenRuntime[] gameObjects = potalPanel.GetComponents<Jun_TweenRuntime>();
            gameObjects[1].Play();
            isPotal = false;
        }
    }
}
