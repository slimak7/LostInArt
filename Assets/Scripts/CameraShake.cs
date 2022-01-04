using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    public float durationTime;
    public float xIntensity;
    public float yIntensity;

    
    public bool increaseShaking;

    [Range(0,1f)]
    public float multiplier;

    [Range(0,1f)]
    public float increaseMultiplier;

    public void shake ()
    {
        StartCoroutine(shakeCam());
    }

    private IEnumerator shakeCam ()
    {
        Vector3 startPos = transform.position;

        if (!increaseShaking)
        {
            float minX = startPos.x - xIntensity;
            float maxX = startPos.x + xIntensity;

            float minY = startPos.y - yIntensity;
            float maxY = startPos.y + yIntensity;



            float time = durationTime;

            while (time > 0)
            {

                Vector3 newPos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), transform.position.z);

                transform.position = newPos;

                time -= Time.deltaTime;

                yield return null;
            }
        }
        else
        {
            float time = durationTime;

            float multiplier = this.multiplier;

            float increaseMultiplier = this.increaseMultiplier;

            while (time > 0)
            {

                float minX = startPos.x - xIntensity * multiplier;
                float maxX = startPos.x + xIntensity * multiplier;

                float minY = startPos.y - yIntensity * multiplier;
                float maxY = startPos.y + yIntensity * multiplier;
                
                Vector3 newPos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), transform.position.z);

                transform.position = newPos;

                time -= Time.deltaTime;
                multiplier += increaseMultiplier;

                yield return null;
            }
        }

        transform.position = startPos;

    }

}
