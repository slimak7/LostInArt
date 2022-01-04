using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class mapLoader : MonoBehaviour
{
    public bool mainPicture = false;

    public int pictureindex = 0;
    public int pictureSceneIndex;
    public GameObject whiteCanvass;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Missile")
        {
            if (!mainPicture)
            {

                if (pictureindex.Equals(1))
                {
                    if (DataManager.loadData(FileNames.picture1Completed).Equals(0))
                    {
                        white();
                        StartCoroutine(loadScene());

                        AudioManager.instance.turnDownCurrentSound();

                        return;

                    }

                }
                if (pictureindex.Equals(2))
                {
                    if (DataManager.loadData(FileNames.picture2Completed).Equals(0) && DataManager.loadData(FileNames.picture1Completed).Equals(1))
                    {
                        white();
                        StartCoroutine(loadScene());

                        AudioManager.instance.turnDownCurrentSound();

                        return;
                    }
                }
                if (pictureindex.Equals(3))
                {
                    if (DataManager.loadData(FileNames.picture3Completed).Equals(0) && DataManager.loadData(FileNames.picture2Completed).Equals(1))
                    {
                        white();
                        StartCoroutine(loadScene());

                        AudioManager.instance.turnDownCurrentSound();

                        return;
                    }
                }
            }
            else
            {
                if (DataManager.loadData(FileNames.allPicturesComplete).Equals(1))
                {
                    white();
                    StartCoroutine(loadScene());
                }
            }




        }

    }
    private void white()
    {
       
        whiteCanvass.SetActive(true);



    }
    private IEnumerator loadScene()
    {
        DataManager.saveData(1, FileNames.gameWasPlayed);

        EndOfTheMap end = FindObjectOfType<EndOfTheMap>();
        if (end != null)
            Destroy(end);


        AsyncOperation async = SceneManager.LoadSceneAsync(pictureSceneIndex);

        while (!async.isDone)
        {
            yield return null;
        }

    }

}