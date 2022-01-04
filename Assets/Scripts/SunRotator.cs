using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRotator : MonoBehaviour {

    [Header("Speed")]
    public float rotationSpeed;
    [Header("Light settings")]
    public float minLightIntensity;
    public float maxLightIntensity;
    public Light light;

    private Vector3 origin;
    private float currentAngle;

    [HideInInspector]
    public bool rotating;

    public static SunRotator instance;

    private int numberOfRotation;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start () {

        origin = new Vector3(transform.position.x, 0, transform.position.z);
        currentAngle = 360f;
        rotating = false;
        numberOfRotation = 0;
	}
	
	
    public void rotate (float angle)
    {
        if (!rotating)
        {
            StartCoroutine(rotateSun(angle));
            //StartCoroutine(HandleOfClockRot.instance.rotate());
            rotating = true;
        }
        
        
    }

    private IEnumerator rotateSun (float angle)
    {
        numberOfRotation++;

        float currentRotation = 0;

        bool decrease = false;
        bool increase =false;

        bool rotate = false;

        currentAngle -= angle;

        if (numberOfRotation.Equals(4))
        {
            decrease = true;
            rotate = true;
        }
        if (numberOfRotation.Equals(5))
        {
            increase = true;
            numberOfRotation = -1;
        }

        while (currentRotation < angle)
        {

            transform.RotateAround(origin, new Vector3(0, 0, 1), rotationSpeed * Time.deltaTime);

            currentRotation += rotationSpeed * Time.deltaTime;

            if (increase)
            {
                light.intensity = Mathf.Lerp(light.intensity, maxLightIntensity, 0.05f);
                //Debug.Log("ggggg");
            }
            if (decrease)
            {
                light.intensity = Mathf.Lerp(light.intensity, minLightIntensity, 0.05f);
                //Debug.Log("ggg");
            }


            yield return null;

        }

        if (rotate)
        {
            transform.RotateAround(origin, new Vector3(0, 0, 1), 90);
            currentAngle -= 90;
        }

        rotating = false;

        if (currentAngle <= 0)
            currentAngle = 360f;
    }
}
