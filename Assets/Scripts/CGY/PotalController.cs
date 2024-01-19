using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotalController : MonoBehaviour
{
    public bool isDunMenu = false;
    public GameObject potalPanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UnitSetBtn();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UnitSetBtn();
        }
    }
    public void UnitSetBtn()
    {
        if (isDunMenu == false)
        {
            Jun_TweenRuntime[] gameObjects = potalPanel.GetComponents<Jun_TweenRuntime>();
            gameObjects[0].Play();
            isDunMenu = true;
        }
        else
        {
            Jun_TweenRuntime[] gameObjects = potalPanel.GetComponents<Jun_TweenRuntime>();
            gameObjects[1].Play();
            isDunMenu = false;
        }
    }
}
