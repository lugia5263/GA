using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerd : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PhotonView>().IsMine)
            {
                Debug.Log("·¯µå");
            }
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
