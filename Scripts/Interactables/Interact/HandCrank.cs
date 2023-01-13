using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HandCrank : MonoBehaviour, IInteractable
{

    private bool RotEnabled = true; //To set if this is enabled or not
    [SerializeField] private GameObject toRotate;//GameObject to be rotated
    [SerializeField] private Vector3 lockRotAngle; //Angle at which it will stop rotating
    [SerializeField] private Vector3 rotationAxis; //Axis to rotate (for ex: ( 0 0 1))
    [SerializeField] private float angularSpeed = 10f;//Rotation Speed
    [SerializeField] private float margin = 0.4f;// Error margin for locking rotation


    public void Update(){
        
    }
    public AnimationsEnum Interact(){
        rotate(toRotate);
        return AnimationsEnum.CRANK;
    }

    public void Power()
    {
        throw new System.NotImplementedException();
    }

    public void enable(bool enable){
        RotEnabled = enable;
    }

    public void rotate(GameObject toRotate){

        if (RotEnabled){
            toRotate.transform.rotation *= Quaternion.Euler(angularSpeed*Time.deltaTime * rotationAxis);
            transform.Rotate(new Vector3(0,0,1), angularSpeed*Time.deltaTime*3);// *= Quaternion.Euler(angularSpeed*Time.deltaTime*transform.right*3);
            //Debug.Log(toRotate.transform.rotation.eulerAngles);
            if (rotationCompleted()){ 
                RotEnabled = false;
                activate(false);
            }
        }
    }
    public bool rotationCompleted() {
        Vector3 angle = toRotate.transform.rotation.eulerAngles;
        return (Vector3.Distance(angle, lockRotAngle) < margin);
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
