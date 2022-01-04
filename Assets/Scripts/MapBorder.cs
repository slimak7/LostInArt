using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapBorder : MonoBehaviour {

    public Transform player;

    public Transform startingPoint;

    public int time;

    public GameObject canvas;

    public Text timeText;

    public AudioSource AS;

   // private InteractionManager IM;

    private bool timeHasStartedCounting;

    private void Start()
    {
        //IM = FindObjectOfType<InteractionManager>();

        timeHasStartedCounting = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player2") && !timeHasStartedCounting)
        {
            canvas.SetActive(true);
            StartCoroutine(countDownTime());
        }
    }

    private IEnumerator countDownTime ()
    {
        timeHasStartedCounting = true;

       // IM.putOnAnObject();

        float t = time;

        timeText.text = time.ToString();

        while (t > 0)
        {
            //if (!pauseSettings.PauseManager.isPaused)
            t -= 1;

            AS.Play();

            yield return new WaitForSeconds(1f);

            timeText.text = t.ToString();

        }

        goBackToStartPos();

        timeHasStartedCounting = false;
    }

    private void goBackToStartPos ()
    {
        player.position = startingPoint.position;


    }


    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player2"))
        {
            StopAllCoroutines();

            canvas.SetActive(false);

            timeHasStartedCounting = false;
        }
    }

}
