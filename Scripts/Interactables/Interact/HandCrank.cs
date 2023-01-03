using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HandCrank : MonoBehaviour, IInteractable
{

    //public UnityEvent interacted;
    private bool rotEnabled = true;
    [SerializeField] private GameObject toRotate;
    [SerializeField] private float lockTargetAngle;
    [SerializeField] private float angularSpeed = 10f;


    public void Update(){
        
    }
    public void Interact(){
        rotate(toRotate);
    }

    public void Power()
    {
        throw new System.NotImplementedException();
    }

    public void enable(bool enable){
        rotEnabled = enable;
    }

    public void rotate(GameObject toRotate){
        if (rotEnabled){
            toRotate.transform.rotation *= Quaternion.Euler(0, angularSpeed*Time.deltaTime, 0);
            //Debug.Log(toRotate.transform.rotation.eulerAngles.y);
            if ((toRotate.transform.rotation.eulerAngles.y > lockTargetAngle - 0.2) && (toRotate.transform.rotation.eulerAngles.y < lockTargetAngle + 0.2)){
                rotEnabled = false;
            }
        }
            
    }

    public void activate(bool flag){
        this.rotEnabled = flag;
        this.enabled = flag;      
       if (flag){
            this.gameObject.layer = LayerMask.NameToLayer (LayerMask.LayerToName(3));
        }else{
            this.gameObject.layer = LayerMask.NameToLayer("Default");
        }  
    }

    public bool isActive() {
        return this.enabled;
    }

    public bool isCompleted() {
        return !this.rotEnabled;
    }

}
