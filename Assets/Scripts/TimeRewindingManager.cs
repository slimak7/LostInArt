using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRewindingManager : MonoBehaviour {
    
    [HideInInspector]
    public int currentIndex;

    [HideInInspector]
    public bool isRewinding;

    public Settings[] settings;

    [Header("Extra (remove col)")]
    public GameObject collider;
    public int colIndex;
    public float time;

    public float timeBeforeSound;

    [Header("Destruction - sound")]
    public AudioSource AS;
    [Header("Rewind time - sound")]
    public AudioSource AS2;

    [System.Serializable]
    public class Settings
    {
        public string name;
        [Header("Time settings")]
        [Range(1f, 20f)]
        public float time = 10f;
        [Range(0f,3f)]
        public float timeBeforeDestroying;
    }

    private void Awake()
    {
        

        isRewinding = false;
        currentIndex = 1;

        StartCoroutine(playSound());
    }

    private IEnumerator playSound ()
    {

        yield return new WaitForSeconds(timeBeforeSound);

        if (AS != null)
        AS.Play();
    }

    private void Start()
    {
        StartCoroutine(removeCol());
    }

   

    public void increaseIndex ()
    {

        isRewinding = false;
        currentIndex += 1;
    }

    public void rewind ()
    {
        isRewinding = true;

        if (AS2 != null)
        {
            AS2.Play();
        }
    }

    


    private IEnumerator removeCol ()
    {
        if (collider==null || settings.Length.Equals(0))
            yield break;

        yield return new WaitForSeconds(time + settings[0].timeBeforeDestroying);

        Destroy(collider);
    }

}
