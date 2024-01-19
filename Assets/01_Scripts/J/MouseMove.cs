using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove :MonoBehaviour
{
    public float movementSpeed = 10f;

    public float rotationSpeed = 10f;

    private  Vector3 destinationPoint;

    private bool Move = false;

    public float wayPoint;

    Animator animator;
    Player player;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            animator.SetBool("isRun", true);
            player.Desh = Input.GetKeyDown(KeyCode.Space);
            animator.SetBool("isDesh", player.Desh);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f))
            {
                destinationPoint = new Vector3(hit.point.x, transform.position.y, hit.point.z);

                Move = true;
            }
        }

        if (Move)
        {
            Quaternion targetRotation = Quaternion.LookRotation(destinationPoint - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            transform.position = Vector3.MoveTowards(transform.position, destinationPoint, movementSpeed * Time.deltaTime);
            
            float dists = Vector3.Distance(destinationPoint, transform.position);
            if (dists <= wayPoint)
            {
               Move = false;
                animator.SetBool("isRun", false);
            }
        }
    }
}
