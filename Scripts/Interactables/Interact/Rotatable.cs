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
    [SerializeField] private float angularSpeed = 10f;


    public void Update(){
        
    }
    public void Interact(){
        //interacted.Invoke();
        //Debug.Log("[Rotatable]: Rotating");
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
        if (RotEnabled){
            toRotate.transform.rotation *= Quaternion.Euler(0, angularSpeed*Time.deltaTime, 0);
            //Debug.Log(toRotate.transform.rotation.eulerAngles.y);
            if ((toRotate.transform.rotation.eulerAngles.y > lockRotAngle - 0.2) && (toRotate.transform.rotation.eulerAngles.y < lockRotAngle + 0.2)){
                RotEnabled = false;
            }
        }
            
    }

    public void activate(bool flag){
        this.RotEnabled = true;
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

}
