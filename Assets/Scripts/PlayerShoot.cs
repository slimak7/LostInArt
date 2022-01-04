using UnityEngine;
using System.Collections;


public class PlayerShoot : MonoBehaviour
{

    public float wait = 0.5f;

    public float timetoShot = 0f;

    public GameObject projectilePrefab;


    public AudioSource soundSource;

    public AudioClip sound;


    public void shot()
    {

        if (timetoShot <= 0 && !InteractionManager.instance.isReadyToInteract)
        {

            timetoShot = wait;


            Instantiate(projectilePrefab, Camera.main.transform.position + Camera.main.transform.forward * 2, Camera.main.transform.rotation);

            if (soundSource != null)
            {
                soundSource.PlayOneShot(sound);
            }
        }
    }


    public void settimetoShot()
    {
        if (timetoShot > 0)
        {

            timetoShot -= Time.deltaTime;
        }
    }

    private void Update()
    {
        settimetoShot();
        if (Input.GetMouseButtonDown(0))
        {

            shot();
        }
    }
}
