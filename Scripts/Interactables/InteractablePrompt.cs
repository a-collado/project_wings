using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePrompt : MonoBehaviour
{
    public void lookAtCamera(Camera playerCamera)
    {
        gameObject.transform.LookAt(playerCamera.transform.position);
        //gameObject.transform.position.y
    }

    public void moveToObject(Transform intObject)
    {
        gameObject.transform.position = intObject.position;
    }
}
