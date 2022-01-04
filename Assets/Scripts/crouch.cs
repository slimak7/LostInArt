using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class crouch : MonoBehaviour
{



    CharacterController characterCollider;
    //FirstPersonController fpc;
    void Start()
    {
        characterCollider = gameObject.GetComponent<CharacterController>();
        //fpc = GameObject.FindObjectOfType<FirstPersonController>();
    }

    void Update()
    {
        /*
        if (!fpc.rotate)
            return;

        if (!fpc.m_CharacterController.isGrounded)
            return;

        if (Input.GetKey(KeyCode.LeftControl))
        {
            characterCollider.height = 1.5f;
            fpc.m_WalkSpeed = (3);
            fpc.m_RunSpeed = (3);
        }
        else
        {
            if (characterCollider.height < 2.5f)
            characterCollider.height += 0.25f;
            fpc.m_WalkSpeed = (7);
            fpc.m_RunSpeed = (12);
        }
	}
    */
    }
}
