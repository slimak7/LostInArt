using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Orb : MonoBehaviour
{
   

    public GameObject todestory;

    public float speed = 6f;

    public float lifeTime = 1f;

    void Start()
    {


    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
        {
            Destroy(gameObject);
        }
    

    }



    void FixedUpdate()
    {
       
    }
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
        {

            Destroy(gameObject);
        }
    }


}
