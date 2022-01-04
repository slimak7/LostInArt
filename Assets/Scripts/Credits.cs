using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour {
    

    private void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (Input.anyKeyDown)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            gameObject.SetActive(false);

            
        }
    }
}
