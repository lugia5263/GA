using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlow : MonoBehaviour
{
    public bool isSlowing;
    public float slowTime = 0.5f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Boss") && other.CompareTag("NomalMonster"))
        {
            Time.timeScale = slowTime;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Boss") && other.CompareTag("NomalMonster"))
        {
            Time.timeScale = 1f;
        }
    }
}
