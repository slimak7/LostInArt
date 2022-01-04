using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour {

    public static Moon instance;

    

    public float speed;

    private void Awake()
    {
        instance = this;
    }

    public void rotate ()
    {
        StartCoroutine(showMoon());
    }

    private IEnumerator showMoon ()
    {

        yield return new WaitForSeconds(2f);

        Vector3 newPos = transform.position + Vector3.up * 250f;

        while (transform.position.y < newPos.y)
        {
            transform.position = Vector3.Lerp(transform.position, newPos, speed * Time.deltaTime);

            yield return null;
        }
    }
}
