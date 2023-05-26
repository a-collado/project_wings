using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HandCrank : MonoBehaviour, IInteractable
{

    private bool RotEnabled = true; //To set if this is enabled or not
    [SerializeField] private GameObject[] toRotate;//GameObject to be rotated
    [SerializeField] private Vector3 lockRotAngle; //Angle at which it will stop rotating
    [SerializeField] private Vector3[] rotationAxis; //Axis to rotate (for ex: ( 0 0 1))
    [SerializeField] private float angularSpeed = 10f;//Rotation Speed
    [SerializeField] private float margin = 0.4f;// Error margin for locking rotation
    [SerializeField] private LightBeamHit _lightBeamHit;


    public void Update(){
        
    }
    public AnimationsEnum Interact(){
        rotate(toRotate);
        return AnimationsEnum.NONE;
    }

    public void Power()
    {
        throw new System.NotImplementedException();
    }

    public void enable(bool enable){
        RotEnabled = enable;
    }

    public void rotate(GameObject[] toRotate){

        if (RotEnabled){
            int i = 0;
            foreach (GameObject obj in toRotate){
                obj.transform.rotation *= Quaternion.Euler(angularSpeed*Time.deltaTime * rotationAxis[i]);
                i++;
            }
            transform.Rotate(new Vector3(1,0,0), angularSpeed*Time.deltaTime*3);           

           // *= Quaternion.Euler(angularSpeed*Time.deltaTime*transform.right*3);
            //Debug.Log(toRotate.transform.rotation.eulerAngles);
            if (rotationCompleted()){ 
                RotEnabled = false;
                activate(false);
            }
        }
    }
    public bool rotationCompleted() {
        return false;
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

    public bool isCompleted()
    {
        return !_lightBeamHit.completed;
    }
    public GameObject getGameObject()
    {
        return this.transform.gameObject;
    }
    

}
