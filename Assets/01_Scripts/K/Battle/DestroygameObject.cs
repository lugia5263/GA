using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroygameObject : MonoBehaviour
{
    // Start is called before the first frame update
    public void AutoDestroy()
    {
        gameObject.SetActive(false);
    }
}
