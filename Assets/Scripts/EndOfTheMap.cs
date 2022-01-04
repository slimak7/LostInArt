using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfTheMap : MonoBehaviour {

    public int museumIndex = 0;
    public int mapIndex;

    public GameObject whiteCanvas;
    
   

    public static EndOfTheMap instance;

    public static bool end;

   // private GameObject missilePrefab;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;
        end = false;

        //missilePrefab = GameObject.Find("FPSController").GetComponent<shotp>().lightUpPrefab;
    }

    public void endMap ()
    {
        if (end)
            return;

        end = true;

        StartCoroutine(white());
        StartCoroutine(loadScene());

    }
    private IEnumerator white()
    {
        yield return new WaitForSeconds(1f);
        whiteCanvas.SetActive(true);



    }
    private IEnumerator loadScene ()
    {
        

        yield return new WaitForSeconds(2f);

        //GameObject.Find("FPSController").GetComponent<shotp>().lightUpPrefab = missilePrefab;

        spawnPartOfThePicture();
        AsyncOperation async = SceneManager.LoadSceneAsync(museumIndex);

        while (!async.isDone)
        {
            yield return null;
        }
        /*
       if (mapIndex.Equals(1))
            FPSManager.instance.enableFPS(2);
        if (mapIndex.Equals(2))
            FPSManager.instance.enableFPS(3);
        if (mapIndex.Equals(3))
            FPSManager.instance.enableFPS(4);

            yield return new WaitForSeconds(0.5f);

        

        end = false;
        */
    }

    private void spawnPartOfThePicture ()
    {
        
        

        if (mapIndex.Equals(1))
        {
            DataManager.saveData(1, FileNames.part1Museum);
            DataManager.saveData(1, FileNames.picture1Completed);
        }
        if (mapIndex.Equals(2))
        {


            DataManager.saveData(1, FileNames.picture2Completed);

            DataManager.saveData(1, FileNames.part2Museum);

            
        }
        if (mapIndex.Equals(3))
        {


            DataManager.saveData(1, FileNames.picture3Completed);

            DataManager.saveData(1, FileNames.part3Museum);


        }
    }


}
