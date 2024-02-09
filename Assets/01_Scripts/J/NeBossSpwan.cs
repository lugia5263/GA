using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeBossSpwan : MonoBehaviour
{
    public GameObject neBoss;
    public GameObject wall;
    public bool wallMove;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if(wallMove)
        {
            wall.transform.position = new Vector3(transform.position.x, 1 * -Time.deltaTime, transform.position.z);
            if (wall.transform.position.y <= -3f)
            {
                Destroy(wall, 1f);
                wallMove = false;
            }
        }


    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            neBoss.SetActive(true);
            wallMove = true;
            Destroy(gameObject, 5f);
        }
    }
}
