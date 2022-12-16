using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Rotatable : MonoBehaviour, IInteractable
{

    //public UnityEvent interacted;
    private bool RotEnabled = true;
    [SerializeField] private GameObject toRotate;
    [SerializeField] private float lockRotAngle;
    [SerializeField] private float angularSpeed = 0.05f;


    public void Interact(){
        //interacted.Invoke();
        rotate(toRotate);

    }

    public void Power()
    {
        throw new System.NotImplementedException();
    }

    public void enable(bool enable){
        RotEnabled = enable;
    }

    public void rotate(GameObject toRotate){
        //Quaternion rotation = Quaternion.Euler(0, 91, 0);
        //gameObject.transform.rotation = Quaternion.Slerp(gameObject.transform.rotation, rotation, Time.deltaTime);
        //Debug.Log(gameObject.transform.rotation);
        if (RotEnabled)
            toRotate.transform.rotation *= Quaternion.Euler(0, 0, angularSpeed);
        //Debug.Log(toRotate.transform.rotation);
    }

}
