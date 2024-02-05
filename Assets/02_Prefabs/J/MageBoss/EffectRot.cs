using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectRot : MonoBehaviour
{
    public float rotSpeed = 30f;

    void Update()
    {
        transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime);
        transform.Rotate(Vector3.forward * rotSpeed * Time.deltaTime);
        transform.Rotate(Vector3.right * rotSpeed * Time.deltaTime);
    }
}
