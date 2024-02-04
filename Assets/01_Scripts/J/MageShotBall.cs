using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageShotBall : MonoBehaviour
{

    public Player player;
    public GameObject p6Bullet;
    [Header("ShotSpeed")]
    public float p1Speed;
    public float p3Speed;
    public float p6Speed;

    [Header("ShotKind")]
    public bool p1Shot;
    public bool p3Shot;
    public bool p6Shot;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(p1Shot)
        {
            transform.Translate(0, 0, p1Speed + Time.deltaTime);
        }
        if(p3Shot)
        {
            StartCoroutine(P3delay());
        }
        if (p6Shot)
        {
            Vector3 directionToTarget = player.transform.position - transform.position;
            Vector3 moveDirection = directionToTarget.normalized;
            transform.Translate(player.transform.position * Time.deltaTime);
            p6Bullet.transform.Rotate(Vector3.up * 2000f * Time.deltaTime);
        }
    }
    IEnumerator P3delay()
    {
        yield return new WaitForSeconds(2f);
        transform.Translate(0, 0, p3Speed + Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
