using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    [Header("Canvas")]
    public GameObject normalPointer;
    public GameObject interactionPointer;

    [Header("Propierties")]
    public float maxRange;
    public Transform pickedUpThingPos;
    public Transform fpsCharacter;

    private Transform camera;

    private bool isInteractionAvailable;
    private bool isInteracted;

    private Transform interactiveThing;

    private Transform pickedUpThing;

    private Rigidbody interactiveThingsRb;

    public bool isReadyToInteract
    {
        get
        {
            if (isInteractionAvailable || isInteracted)
                return true;
            else
                return false;
        }
    }

    public static InteractionManager instance;

    private void Awake()
    {
        instance = this;

        camera = Camera.main.transform;
    }

    private void updateCanvas ()
    {
        if (isInteracted)
        {
            normalPointer.SetActive(true);
            interactionPointer.SetActive(false);
        }
        else
        {
            if (isInteractionAvailable)
            {
                interactionPointer.SetActive(true);
                normalPointer.SetActive(true);
            }
            else
            {
                interactionPointer.SetActive(false);
                normalPointer.SetActive(true);
            }
        }
    }

    private void checkIfInteractionIsAvailable ()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, maxRange))
        {
            if (hit.transform.gameObject.GetComponent<InteractiveThing>() && !isInteracted)
            {
                isInteractionAvailable = true;
                
                interactiveThing = hit.transform;
            }
            else
            {
                isInteractionAvailable = false;

                interactiveThing = null;
            }
        }
        else
        {
            isInteractionAvailable = false;

            interactiveThing = null;
        }
    }
        
    private void pickUp ()
    {
        pickedUpThing = interactiveThing;

        pickedUpThing.transform.parent = fpsCharacter;

        isInteractionAvailable = false;
        isInteracted = true;

        interactiveThingsRb = pickedUpThing.GetComponent<Rigidbody>();

        interactiveThingsRb.isKinematic = true;

        StartCoroutine(moveObjectCloser());
    }

    private IEnumerator moveObjectCloser ()
    {
        while (true)
        {
            if (pickedUpThing == null)
                yield break;

            pickedUpThing.position = Vector3.Lerp(pickedUpThing.position, pickedUpThingPos.position, 5f * Time.deltaTime);

            if (Vector3.Distance(pickedUpThing.position, pickedUpThingPos.position) <= 0.1f)
                yield break;

            yield return null;
        }
    }

    private void dropDown ()
    {
        interactiveThingsRb.isKinematic = false;

        interactiveThing = null;

        pickedUpThing.transform.parent = null;

        pickedUpThing = null;

        isInteracted = false;

        isInteractionAvailable = false;
    }

    private void Update()
    {
        checkIfInteractionIsAvailable();

        updateCanvas();

        if (Input.GetMouseButtonDown(0))
            if (isInteractionAvailable && !isInteracted && interactiveThing != null)
            {
                pickUp();
            }

        if (Input.GetMouseButtonUp(0))
            if (isInteracted)
            {
                dropDown();
            }
    }

}
