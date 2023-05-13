using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class Interactor : MonoBehaviour
{
    [SerializeField] private LayerMask interactableMask, triggerMask;
    [SerializeField] private Camera cam;
    [SerializeField] private InteractablePrompt prompt;
    [SerializeField] private CharacterAnimation animator;
    [SerializeField] private FasTPS.PlayerInput input;
    [SerializeField] private FasTPS.CharacterMovement movement;
    
    [SerializeField] private InteractorTrigger trigger;
    private IInteractable _interactable = null;
    
    private void Update() 
    {
        prompt.lookAtCamera(cam);
        prompt.show(false);

        if (!trigger.isCollider)
            return;
        
        _interactable = trigger.collider;
        if (movement.IsJumping || movement.IsCovering || movement.IsClimbUp || movement.IsSliding ||
            !movement.IsGrounded) return;
        if (_interactable == null || !_interactable.isActive()) return;
        setPrompt(_interactable.getGameObject().transform);

        if (input.Interact)    // Detectamos si lo estamos pulsando con el raton
        {
            animator.playAnimation(_interactable.Interact());                           // Interactuamos con el objeto
            prompt.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        if (input.Power){
            _interactable.Power();
        }

    }

    private void setPrompt(Transform obj){
        prompt.moveToObject(obj.transform);
        prompt.show(true);
    }
    
    public void Attach()
    {
        var o = _interactable.getGameObject();
        o.transform.rotation = Quaternion.identity;
        o.transform.SetParent(animator.leftHandPicker.transform);
        o.transform.localPosition = Vector3.zero;
        _interactable.activate(false);
        o.layer = LayerMask.NameToLayer("Default");
        _interactable = null;
    }
    
}
