using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

    public Transform point;

    public Collider col;

    private Vector3 currentTarget;

    private Vector3 startPos;

    public Box b;

    public GameObject particles;

    private Transform player;

    private Vector3 vel;

    public Rigidbody rb;

    private void Awake()
    {
        startPos = transform.position;
        currentTarget = startPos;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    

    private void FixedUpdate()
    {
        
      

        col.enabled = true;

        
        rb.MovePosition(Vector3.Lerp(transform.position,currentTarget, 0.3f/(Vector3.Distance(transform.position,currentTarget))));

        
    }

   
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player2"))
        {
            StartCoroutine(stop());
        }
    }

    private IEnumerator stop ()
    {
        yield return new WaitForSeconds(3f);

        currentTarget = startPos;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!b.timeHasBeenRewinded)
            return;

        if (other.transform.CompareTag("Player2"))
        {
            currentTarget = point.position;
            StopAllCoroutines();
        }
    }

}
