using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractorTrigger : MonoBehaviour
{

    [SerializeField] private LayerMask interactableMask;
    public IInteractable collider = null;
    public bool isCollider = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (interactableMask != (interactableMask | (1 << other.gameObject.layer))) return;
        if(other.gameObject.TryGetComponent<IInteractable>(out collider))
        {
            isCollider = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (interactableMask == (interactableMask | (1 << other.gameObject.layer)))
        {
            collider = null;
            isCollider = true;
        }
    }
}
