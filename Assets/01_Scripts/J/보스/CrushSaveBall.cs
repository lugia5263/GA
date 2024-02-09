using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushSaveBall : MonoBehaviour
{
    public GameObject SaveZone;
    public StateManager Sm;
    void Start()
    {
        Sm = GetComponent<StateManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Sm.hp <=0)
        {
            Instantiate(SaveZone, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        Destroy(gameObject, 15f);
    }
}
