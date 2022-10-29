using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Rotation : MonoBehaviour, IInteractable
{

    public UnityEvent interacted;
    public void Interact(){
        Debug.Log("Interacting with " + name);
        interacted.Invoke();
    }

    public void Power()
    {
        throw new System.NotImplementedException();
    }

    public void Rotate(GameObject toRotate){
        //Quaternion rotation = Quaternion.Euler(0, 91, 0);
        //gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, rotation, Time.deltaTime);
        //Debug.Log(gameObject.transform.rotation);
        toRotate.transform.rotation *= Quaternion.Euler(0,0.05f,0);
    }

}
