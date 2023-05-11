using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float _interactionPointRadius = 1.5f;
    [SerializeField] private LayerMask _interactableMask, _triggerMask;
    [SerializeField] private Camera cam;
    [SerializeField] private InteractablePrompt prompt;
    [SerializeField] private CharacterAnimation animator;
    [SerializeField] private FasTPS.PlayerInput input;
    [SerializeField] private FasTPS.CharacterMovement movement;

    private int _numFound;
    private int _numFoundT;

    private Collider[] _colliders = new Collider[3];
    private Collider[] _triggers = new Collider[3];


    private void Awake() {
        /*
         animator = GetComponent<CharacterAnimation>();
        input = GetComponentInParent<FasTPS.PlayerInput>();
        movement = GetComponent<FasTPS.CharacterMovement>();
        */
    }

    private void EarlyUpdate()
    {
        
    }
    private void Update() 
    {
        prompt.lookAtCamera(cam);

        /*
        _numFound = Physics.OverlapSphereNonAlloc(gameObject.transform.position, 
        _interactionPointRadius, _colliders, _interactableMask);            // Detecta todos los objetos en un radio dado alrededor del personaje.

        _numFoundT = Physics.OverlapSphereNonAlloc(gameObject.transform.position, 
        _interactionPointRadius, _triggers, _triggerMask);            // Detecta todos los objetos en un radio dado alrededor del personaje.


        string log = "[Interactor] colliders found num= " + _numFound + " (" + _colliders + ": " + _colliders.Length + " ) : ";
        
        if (_numFound > 0)                                                  // Si se ha encontrado algun objeto
        {

            int indexInteractable = -1;

            for (int i = 0; i < _colliders.Length-1; i++)
            {
                //log += " , ["+ i + "]" + _colliders[i] + " : ";
                if (_colliders[i] && _colliders[i].GetComponent<IInteractable>().isActive()){
                    indexInteractable = i;
                    
                    break;
                }
            }

            //Debug.Log(indexInteractable);

            if (indexInteractable != -1){
                var interactable = _colliders[indexInteractable].GetComponent<IInteractable>(); // Comprobas si es un objeto interactuable
                if(!movement.IsJumping && !movement.IsCovering && !movement.IsClimbUp && !movement.IsSliding && movement.IsGrounded){
                    if (interactable != null && interactable.isActive()){
                    //Debug.Log("[Interactor] setPrompt: interactable = " + interactable + " is = " + interactable.isActive() + " as ray collided with: " + _colliders[0]);
                    setPrompt(_colliders[indexInteractable].transform);
                

                    if (InteractorClicked(interactable))    // Detectamos si lo estamos pulsando con el raton
                    {
                        animator.playAnimation(interactable.Interact());                           // Interactuamos con el objeto
                    }
                    if (input.Power){
                        interactable.Power();
                    }
                    }
                }
                
            }
            
        }
        */

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Interactuable"))
        {
            var interactable = other.GetComponent<IInteractable>(); // Comprobas si es un objeto interactuable
            if(!movement.IsJumping && !movement.IsCovering && !movement.IsClimbUp && !movement.IsSliding && movement.IsGrounded){
                if (interactable != null && interactable.isActive()){
                    //Debug.Log("[Interactor] setPrompt: interactable = " + interactable + " is = " + interactable.isActive() + " as ray collided with: " + _colliders[0]);
                    setPrompt(other.transform);
                    
                    if (InteractorClicked(interactable))    // Detectamos si lo estamos pulsando con el raton
                    {
                        animator.playAnimation(interactable.Interact());                           // Interactuamos con el objeto
                    }
                    if (input.Power){
                        interactable.Power();
                    }
                }
            }
        }
        else
        {
            prompt.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Interactuable"))
        {
            prompt.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }


    /*public bool inRange(IInteractable target){
        for (int i = 0; i < _numFound; i++)                     // Miramos todos los objetos en nustro rango
        {
            var interactable = _colliders[i].GetComponent<IInteractable>();     // Miramos si son interactuables
            if (interactable == target) return true;                            // Si lo son, devolvemos true
        }
        return false;
    }*/

    public bool triggerInRange(Collider target){
        for (int i = 0; i < _numFoundT; i++)                     // Miramos todos los objetos en nustro rango
        {
            var trigger = _triggers[i].GetComponent<Collider>();     // Miramos si son un collider
            if (trigger == target && trigger.isTrigger) return true;  // Si lo son, devolvemos true
        }
        return false;
    }

    private bool InteractorClicked(IInteractable target){
        return input.Interact;
    }

    private void setPrompt(Transform obj){
        prompt.moveToObject(obj.transform);
        prompt.gameObject.GetComponent<MeshRenderer>().enabled = true;
    }

    private void animate(AnimationsEnum animation){
        switch(animation){
            case AnimationsEnum.NONE:
            break;
            case AnimationsEnum.GRAB_LOW:
                
            break;
        }
    }


    // Esta funcion dibuja el radio de interaccion
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, _interactionPointRadius);
    }


    
}
