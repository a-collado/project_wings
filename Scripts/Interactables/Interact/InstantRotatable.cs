using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantRotatable : MonoBehaviour, IInteractable
{

    private bool RotEnabled = true; //To set if this is enabled or not
    [SerializeField] private GameObject toRotate;//GameObject to be rotated (if null, it will be the object itself)
    [SerializeField] private bool isBidirectional; //Means it has 2 correct angles

    [SerializeField] private Vector3 currentForwardOrientation;
    [SerializeField] private Vector3 correctForwardOrientation; //Correct angle (calculated from the forward vector)
    [SerializeField] private Vector3 rotationAxis; //Axis to rotate (for ex: ( 0 0 1))
    // Start is called before the first frame update
    
    public void Update(){
        currentForwardOrientation = this.gameObject.transform.forward;
    }
    public AnimationsEnum Interact(){
        if (toRotate == null) toRotate = this.gameObject;
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
            ////Debug.Log("[InstantRotatable: rotate]: " + toRotate.name + " currentAngle: " + toRotate.transform.rotation.eulerAngles + " distance from correctAngle:  " + Vector3.Distance(toRotate.transform.rotation.eulerAngles, correctForwardOrientation) + " forward: " + toRotate.transform.forward);
            toRotate.transform.rotation *= Quaternion.Euler(90 * rotationAxis);
            //Disable for 1 sec so it doesn't rotate again
            RotEnabled = false;
            StartCoroutine(EnableRot());
        }
    }

    IEnumerator EnableRot(){
        yield return new WaitForSeconds(0.25f);
        RotEnabled = true;
    }
    public bool orientationIsCorrect() {
        if (toRotate == null) toRotate = this.gameObject;
        Vector3 currentForward = toRotate.transform.forward;
        if (isBidirectional)
            return (Vector3.Distance(currentForward, correctForwardOrientation) == 0 || Vector3.Distance(currentForward, -correctForwardOrientation) == 0);
        else
            return (Vector3.Distance(currentForward, correctForwardOrientation) == 0);
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

    public bool isCompleted() {
        return orientationIsCorrect();
    }
    
    public GameObject getGameObject()
    {
        return this.transform.gameObject;
    }
}
