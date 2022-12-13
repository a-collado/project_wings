using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Rotatable : MonoBehaviour, IInteractable
{

    public UnityEvent interacted;
    private bool RotEnabled = true;


    public void Interact(){
        interacted.Invoke();
    }

    public void Power()
    {
        throw new System.NotImplementedException();
    }

    public void enable(bool enable){
        RotEnabled = enable;
    }

    public void Rotate(GameObject toRotate){
        //Quaternion rotation = Quaternion.Euler(0, 91, 0);
        //gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, rotation, Time.deltaTime);
        //Debug.Log(gameObject.transform.rotation);
        if (RotEnabled)
            toRotate.transform.rotation *= Quaternion.Euler(0, 0, 0.05f);
        //Debug.Log(toRotate.transform.rotation);
    }

}
