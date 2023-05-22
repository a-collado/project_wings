using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private Inventory inventory;
    [SerializeField] private FasTPS.PlayerInput input;
    [SerializeField] private FasTPS.CharacterMovement movement;
    
    [SerializeField] private InteractorTrigger trigger;
    [SerializeField] private InteractorTrigger Powertrigger;
    private IInteractable _interactable = null;
    
    private void Update() 
    {
        prompt.lookAtCamera(cam);
        prompt.show(false);
        
        if (Powertrigger.isCollider && !movement.MenuOpen){
            _interactable = Powertrigger.collider;

            if (_interactable != null && _interactable.isActive())
            {
                _interactable.Power();
            };
        }

        if (trigger.isCollider && !movement.MenuOpen)
        {
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
        }

    }

    private void setPrompt(Transform obj){
        prompt.moveToObject(obj.transform);
        prompt.show(true);
    }
    
    public void Attach()
    {
        //Debug.Log("VAR");
        var o = _interactable.getGameObject();
        o.transform.SetParent(animator.leftHandPicker.transform);
        o.transform.localPosition = Vector3.zero;
        
        Pickable item;
        if (o.TryGetComponent<Pickable>(out item))
        {
            if (item.localPos)
            {
                o.transform.localPosition = item.localPosition;
                o.transform.localRotation = item.localRotation;
            }
        }
        _interactable.activate(false);
        o.layer = LayerMask.NameToLayer("Default");
        _interactable = null;
    }

    public void Detach()
    {
        if (inventory.getBlock() == null || _interactable == null) return;
        inventory.getBlock().transform.SetParent(_interactable.getGameObject().transform);
        inventory.getBlock().transform.localPosition = new Vector3(0f,0f,0f);
        inventory.getBlock().transform.localRotation = Quaternion.Euler(0,0,-30f);
        inventory.dropBlock();
    }

    public void moveLeftHandToInteractor()
    {
        if (_interactable == null) return;
        animator.leftHandTarget.transform.position = _interactable.getGameObject().transform.position;
    }

    public void moveLeftHandToNormal()
    {
        Pickable item;
        if (inventory.getBlock() != null && inventory.getBlock().TryGetComponent<Pickable>(out item))
        {
            if (item.leftHand)
            {
                animator.leftHandTarget.transform.localPosition = item.leftHandRestPosition;
                animator.leftHandTarget.transform.localRotation = item.leftHandRestRotation;
                return;
            }
        }
            
        // DEFAULT HAND POSITION
        animator.leftHandTarget.transform.localPosition =
            new Vector3(-0.07177436f, -0.396999f, 0.40f);
        animator.leftHandTarget.transform.localRotation = Quaternion.Euler(-9.639f, -120f, -89.795f);
    }
    
    public void moveRightHandToInteractor()
    {
        if (_interactable == null) return;
        animator.rightHandTarget.transform.position = _interactable.getGameObject().transform.position;
    }
    
    public void moveRightHandToNormal()
    {
        Pickable item;
        if (inventory.getBlock() != null && inventory.getBlock().TryGetComponent<Pickable>(out item))
        {
            if (item.rightHand)
            {
                animator.rightHandTarget.transform.localPosition = item.rightHandRestPosition;
                animator.rightHandTarget.transform.localRotation = item.rightHandRestRotation;
                return;
                Debug.Log("2");
            }
        }
        
        // DEFAULT HAND POSITION
        animator.rightHandTarget.transform.localPosition =
            new Vector3(-0.297f, -0.359f, 0.392f);
        animator.rightHandTarget.transform.localRotation = Quaternion.Euler(16.977f, 141.297f, -260.161f);
    }

    public void AttachFromInventory()
    {
        var o = inventory.getBlock();
        if (o == null) return;
        o.transform.SetParent(animator.leftHandPicker.transform);
        //o.transform.rotation = Quaternion.Euler(0, 90, 0);
        o.transform.localPosition = Vector3.zero;
        o.layer = LayerMask.NameToLayer("Default");
    }

    public void attachLastPickedObject()
    {
        var o = inventory.getItems();
        if (o.Count <= 0) return;
        var item = o.Last();
        Grabable i;
        item.TryGetComponent<Grabable>(out i);
        item.transform.SetParent(animator.leftHandPicker.transform);
        item.transform.localPosition = Vector3.zero;

        if (i == null || !i.localPos) return;
        item.transform.localPosition = i.localPosition;
        item.transform.localRotation = i.localRotation;

    }
    
    public void moveLeftHandToGrabbable()
    {
        var o = inventory.getItems();
        if (o.Count <= 0) return;
        var item = o.Last();
        animator.leftHandTarget.transform.position = item.transform.position;
    }

    public void moveLeftHandSave()
    {
        animator.leftHandTarget.transform.localPosition = new Vector3(-0.0513999984f, -0.138500005f, 0.0317000002f);
        animator.leftHandTarget.transform.localRotation = Quaternion.Euler(350.360992f, 240f, 321.971527f);
    }

    public void deactivateItem()
    {        
        var o = inventory.getItems();
        if (o.Count <= 0) return;
        var item = o.Last();
        item.SetActive(false);
    }
    
    //this.gameObject.SetActive(false);
}
