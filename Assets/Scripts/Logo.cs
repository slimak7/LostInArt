using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logo : MonoBehaviour {

    public GameObject logoCanvas;
    public GameObject menuCanvas;

    private void Start()
    {
        
        if (PlayerPrefs.GetInt("ft") != 0)
        {
            logoCanvas.SetActive(false);
            menuCanvas.SetActive(true);
            PlayerPrefs.SetInt("ft", 1);
            Destroy(gameObject);
           
        }

        PlayerPrefs.SetInt("ft", 1);
    }

    // Update is called once per frame
    void Update () {
		
        if (Input.GetKeyDown(KeyCode.Space))
        {
            logoCanvas.SetActive(false);
            menuCanvas.SetActive(true);
            Destroy(gameObject);
        }

	}
}
