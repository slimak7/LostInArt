using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using pauseSettings;


public class Box : MonoBehaviour
{

    [Header("Box settings")]
    public int index;
    public TimeRewindingManager manager;
    public bool controlBox;
    public float force;
    public ForceDirection forceDirection = ForceDirection.forward;
    public ForceMode forceMode = ForceMode.addForce;
    public bool removeCollider;

    //public bool debug = false;

    public int layerIndexAfterDestroy = 0;

    public bool doNotChangeLayer = false;

    [HideInInspector]
    public bool timeHasBeenRewinded;

    private AudioSource AS;

    public bool playSoundOnCollision;

    public bool kinematicRigidbody = false;

    [Header("Debug")]
    public bool debug;

    private bool soundHasBeenPlayed;

    [HideInInspector]
    public bool storeData;

    private float time;
    private float timeBeforeDestroying;


    private float timeInCourotine;

    private Rigidbody rb;

    private Vector3 startPos;
    private Quaternion startRot;

    private List<BoxInfo> boxesInfos = new List<BoxInfo>();

    private int currentIndex;

    private int maxNumberOfData;

    private Coroutine c;

    private bool b;

    private Vector3 direction;

    private bool timeIsRewinding;

    private bool getInformations;

    private float cTime;

    public enum ForceDirection
    {
        up, down, forward, back, right, left
    };

    public enum ForceMode
    {
        addForce, velocity, none
    };

    private void Awake()
    {
        cTime = Time.time;

        storeData = false;
        b = false;

        startPos = transform.localPosition;
        startRot = transform.localRotation;

        soundHasBeenPlayed = false;

        AS = GetComponent<AudioSource>();

        time = manager.settings[index - 1].time;
        timeBeforeDestroying = manager.settings[index - 1].timeBeforeDestroying;

        maxNumberOfData = Mathf.RoundToInt(1/ Time.fixedDeltaTime * time);

        timeHasBeenRewinded = false;
        timeIsRewinding = false;

        rb = GetComponent<Rigidbody>();


        if (forceDirection.Equals(ForceDirection.up))
        {
            direction = Vector3.up;
        }
        if (forceDirection.Equals(ForceDirection.down))
        {
            direction = Vector3.down;
        }
        if (forceDirection.Equals(ForceDirection.forward))
        {
            direction = Vector3.forward;
        }
        if (forceDirection.Equals(ForceDirection.back))
        {
            direction = -Vector3.forward;
        }
        if (forceDirection.Equals(ForceDirection.right))
        {
            direction = Vector3.right;
        }
        if (forceDirection.Equals(ForceDirection.left))
        {
            direction = -Vector3.right;
        }


    }

    private void Start()
    {
        StartCoroutine(waitToAddRB());

    }

    private IEnumerator waitToAddRB()
    {
        if (AS != null)
        {
            //if (transform.tag != "Panel1" && transform.tag != "Panel2" && transform.tag != "Panel3")
            AS.Play();

        }

        yield return new WaitForSeconds(timeBeforeDestroying);

        storeData = true;

        yield return new WaitForSeconds(Time.deltaTime * 3f);

        rb = gameObject.AddComponent<Rigidbody>();
        rb.mass = 5f;
        rb.angularDrag = 0.01f;
        rb.drag = 0.01f;

        if (kinematicRigidbody)
            rb.isKinematic = true;

        //rb.maxDepenetrationVelocity = 6.9f;
        //rb.maxAngularVelocity = 6.9f;
        if (forceMode.Equals(ForceMode.addForce))
            rb.AddForce(direction * force);
        if (forceMode.Equals(ForceMode.velocity))
            rb.velocity = direction * force;

    }



    // Update is called once per frame
    float test;
    float currentTime;
    int a = 0;
    void FixedUpdate()
    {

        if (timeIsRewinding)
        {
                if (boxesInfos.Count > 0)
                {
                    a++;
                    currentIndex = boxesInfos.Count - 1;

                    transform.localPosition = boxesInfos[currentIndex].position;
                    transform.localRotation = boxesInfos[currentIndex].rotation;


                    boxesInfos.RemoveAt(currentIndex);
                }
                else
                {
                    timeHasBeenRewinded = true;

                    Collider col = GetComponent<Collider>();

                    if (col != null)
                        col.enabled = true;


                    if (controlBox)
                        manager.increaseIndex();


                    if (removeCollider)
                        Destroy(GetComponent<Collider>());

                    if (!doNotChangeLayer)
                        gameObject.layer = 0;

                    transform.localPosition = startPos;
                    transform.localPosition = transform.localPosition + Vector3.right * 0.0001f;
                    transform.localRotation = startRot;

                    if (debug)
                        Debug.Log(a/(Time.time - currentTime));

                    Destroy(this);
                }
            
            
            return;
        }
        if (!timeIsRewinding)
        {
            test = Time.fixedDeltaTime;
            currentIndex = boxesInfos.Count;
            currentTime = Time.time;
        }


        

        if (manager.isRewinding && manager.currentIndex == index)
        {

            getBoxInfo();
        }

        if (manager.isRewinding && manager.currentIndex != index)
        {
            if (c == null)
                c = StartCoroutine(moveAfterRebuilding());
        }



        if (storeData)
            storeBoxInfo();

       
    }

    private IEnumerator moveAfterRebuilding()
    {
        if (!SceneManager.GetActiveScene().name.Equals("dabrowo"))
            yield break;

        yield return new WaitForSeconds(1);

        int numberOfData = Mathf.RoundToInt(5f / Time.fixedDeltaTime);

        maxNumberOfData += numberOfData;

        storeData = true;
        rb.constraints = RigidbodyConstraints.None;
        rb.velocity = Vector3.down * 0.5f;
        
    }



    private void getBoxInfo()
    {

        if (timeIsRewinding)
            return;

        Collider col = GetComponent<Collider>();

        if (col != null)
            col.enabled = false;

        StopAllCoroutines();

        storeData = false;

        Destroy(rb);

        //timeHasBeenRewinded = true;
        timeIsRewinding = true;

       // StartCoroutine(getInfo());

    }

    private IEnumerator getInfo()
    {
        float currentTime = Time.time;

        currentIndex = boxesInfos.Count;

        while (currentIndex > 0)
        {
            currentIndex = boxesInfos.Count - 1;


            transform.localPosition = boxesInfos[currentIndex].position;
            transform.localRotation = boxesInfos[currentIndex].rotation;


            //yield return new WaitForSeconds(timeInCourotine);
            //yield return new WaitForEndOfFrame();
            
            yield return null;
                
            //if (!PauseManager.isPaused)
            //boxesInfos.RemoveAt(currentIndex);

        }

        timeHasBeenRewinded = true;

        Collider col = GetComponent<Collider>();

        if (col!=null)
        col.enabled = true;


        if (controlBox)
            manager.increaseIndex();


        if (removeCollider)
            Destroy(GetComponent<Collider>());

        if (!doNotChangeLayer)
        gameObject.layer = 0;

        transform.localPosition = startPos;
        transform.localPosition = transform.localPosition + Vector3.right * 0.0001f;
        transform.localRotation = startRot;

        if (debug)
            Debug.Log(Time.time - currentTime);

        Destroy(this);

    }

    

    private void storeBoxInfo()
    {
        if (timeHasBeenRewinded || Time.timeScale.Equals(0))
            return;


        BoxInfo bi = new BoxInfo();
        bi.position = transform.localPosition;
        bi.rotation = transform.localRotation;

        boxesInfos.Add(bi);

       

        if (boxesInfos.Count > maxNumberOfData)
        {

            if (debug)
                Debug.Log(Time.time - cTime);

            if (AS != null)
            AS.Stop();

            if (!doNotChangeLayer)
            gameObject.layer = layerIndexAfterDestroy;

            rb.constraints = RigidbodyConstraints.FreezeAll;

            storeData = false;

        }
        

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (playSoundOnCollision)
        {
            if (!soundHasBeenPlayed)
            {
                if (AS != null)
                {
                    AS.Play();
                    soundHasBeenPlayed = true;
                }
            }
        }
    }


}
