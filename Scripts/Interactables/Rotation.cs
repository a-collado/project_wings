using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Rotation : MonoBehaviour, IInteractable
{

    public UnityEvent interacted;
    private bool enabled = true;


    public void Interact(){
        Debug.Log("Interacting with " + name);
        interacted.Invoke();
    }

    public void Power()
    {
        throw new System.NotImplementedException();
    }

    public void enable(bool enable){
        enabled = enable;
    }

    public void Rotate(GameObject toRotate){
        //Quaternion rotation = Quaternion.Euler(0, 91, 0);
        //gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, rotation, Time.deltaTime);
        //Debug.Log(gameObject.transform.rotation);
        if (enabled)
            toRotate.transform.rotation *= Quaternion.Euler(0,0.05f,0);
        //Debug.Log(toRotate.transform.rotation);
    }

}
