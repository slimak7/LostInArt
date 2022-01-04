using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class maploaderintro : MonoBehaviour
{


    public int pictureindex = 0;
    public GameObject whiteCanvass;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            white();
            StartCoroutine(loadScene());
            DataManager.saveData(1, FileNames.newGame);

        }

    }
    public void load ()
    {
        white();
        StartCoroutine(loadScene());
        DataManager.saveData(1, FileNames.newGame);
    }

    private void white()
    {

        whiteCanvass.SetActive(true);



    }
    private IEnumerator loadScene()
    {
        EndOfTheMap end = FindObjectOfType<EndOfTheMap>();
        if (end != null)
            Destroy(end);


        AsyncOperation async = SceneManager.LoadSceneAsync(pictureindex);

        while (!async.isDone)
        {
            yield return null;
        }

    }

}