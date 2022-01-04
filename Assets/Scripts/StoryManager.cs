using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StoryManager : MonoBehaviour {

    public GameObject[] images;

    public GameObject loadingCanvas;

    public GameObject infoText;

    public float time = 3f;

    private int currentIndex;

    private bool animating;

    private bool firstImage;

    private void OnEnable()
    {
        firstImage = true;
        currentIndex = 0;
        animating = false;
        Cursor.lockState = CursorLockMode.Locked;
        //startAnimation();
        index = images.Length;
        DataManager.saveData(1, FileNames.newGame);

        StartCoroutine(autoPlay());
    }

    private void startAnimation ()
    {

        animating = true;

        if (currentIndex.Equals(0) && firstImage)
        {
            

            images[currentIndex].SetActive(true);
            images[currentIndex].GetComponent<Animator>().SetTrigger("nextImage");
            
            firstImage = false;
        }
        else
        {
            if (currentIndex < images.Length)
            {
                images[currentIndex - 1].GetComponent<Animator>().SetTrigger("hideImage");
                //images[currentIndex].SetActive(false);
                images[currentIndex].SetActive(true);
                images[currentIndex].GetComponent<Animator>().SetTrigger("nextImage");
                
            }
            else
            {
                if (currentIndex  < images.Length)
                    images[currentIndex - 1].GetComponent<Animator>().SetTrigger("hideImage");
                    //images[currentIndex].SetActive(false);

                infoText.SetActive(false);
                
                //yield return new WaitForSeconds(1f);

                loadingCanvas.SetActive(true);
                //loadingCanvas.GetComponent<CastleLoadingCanvas>().load();
            }
        }

        //yield return new WaitForSeconds(1f);

        currentIndex++;

        animating = false;
    }

    int index;
    private IEnumerator autoPlay ()
    {
        if (firstImage)
        startAnimation();


        while (index >= 0)
        {
            yield return new WaitForSeconds(time);

            startAnimation();

            index--;

        }
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            StopAllCoroutines();
            startAnimation();
            index--;
            StartCoroutine(autoPlay());
        }
    }
}
