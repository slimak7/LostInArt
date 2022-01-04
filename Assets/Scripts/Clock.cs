using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour {

    private Box b;

    

    private void Awake()
    {
        b = GetComponent<Box>();
        
        
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (b != null)
        if (!b.timeHasBeenRewinded)
            return;

        if (other.CompareTag("Missile"))
        {
            if (SunRotator.instance != null)
            if (!SunRotator.instance.rotating)
            {
                SunRotator.instance.rotate(45f);

            }

        }
    }

    
}
