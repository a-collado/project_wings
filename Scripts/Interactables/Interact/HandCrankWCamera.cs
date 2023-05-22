using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HandCrankWCamera : MonoBehaviour, IInteractable
{

    private bool RotEnabled = true; //To set if this is enabled or not
    [SerializeField] private GameObject toRotate;//GameObject to be rotated
    [SerializeField] private Vector3 lockRotAngle; //Angle at which it will stop rotating
    [SerializeField] private Vector3 rotationAxis; //Axis to rotate (for ex: ( 0 0 1))
    [SerializeField] private float angularSpeed = 10f;//Rotation Speed
    [SerializeField] private float margin = 0.4f;// Error margin for locking rotation
    [SerializeField] private GameObject prompts;
    [Header("Cameras")]
    [SerializeField] private int resolveCameraIndex = -1;
    [SerializeField] private VirtualCamerasManager cameraManager;
    private bool interacting = false;

    private FasTPS.PlayerInput input;
    private FasTPS.CharacterMovement movement;

    private void Start() {
        input = GameObject.FindGameObjectWithTag("Player").GetComponentInParent<FasTPS.PlayerInput>();
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<FasTPS.CharacterMovement>();
        activate(true);
        
    }

    public AnimationsEnum Interact(){
        cameraManager.SetVirtualCamera(resolveCameraIndex, 0);
        prompts.SetActive(true);
        interacting = true;
        
        return AnimationsEnum.NONE;
    }

    public void Power()
    {}

    public void enable(bool enable){
        RotEnabled = enable;
    }

    public void rotate(GameObject toRotate){

        if (input.MoveInput.x == 1.0f){
            toRotate.transform.rotation *= Quaternion.Euler(-angularSpeed*Time.deltaTime * rotationAxis);
            transform.Rotate(new Vector3(0,0,1), angularSpeed*Time.deltaTime*3);// *= Quaternion.Euler(angularSpeed*Time.deltaTime*transform.right*3);
            
            /*if (rotationCompleted()){ 
                RotEnabled = false;
                activate(false);
            }*/
        }
        if (input.MoveInput.x == -1.0f){
            toRotate.transform.rotation *= Quaternion.Euler(angularSpeed*Time.deltaTime * rotationAxis);
            transform.Rotate(new Vector3(0,0,1), angularSpeed*Time.deltaTime*3);// *= Quaternion.Euler(angularSpeed*Time.deltaTime*transform.right*3);
            
            /*if (rotationCompleted()){ 
                RotEnabled = false;
                activate(false);
            }*/
        }
    }
    public bool rotationCompleted() {
        Vector3 angle = toRotate.transform.rotation.eulerAngles;
        return (Vector3.Distance(angle, lockRotAngle) < margin);
    }

    public void activate(bool flag){
        this.RotEnabled = true;
        this.enabled = flag;      

    }

    public bool isActive() {
        return this.enabled;
    }

    public bool isCompleted()
    {
        return isActive();
    }

    private void Update() {
        if(interacting)
        {
            movement.enabled = false;
            if(input.Jump){
                prompts.SetActive(false);
                cameraManager.resetCamera();
                interacting = false;
                movement.enabled = true;
            }
            rotate(toRotate);
        }
    }

    void IInteractable.Update()
    {
        
    }
    public GameObject getGameObject()
    {
        return this.transform.gameObject;
    }
    
}
