using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Box : MonoBehaviour, IInteractable
{
    public UnityEvent interacted;
    public void Interact(){
        Debug.Log("Interacting with " + name);
        interacted.Invoke();
    }

    public void Power()
    {
        Debug.Log("Using your power with " + name);
    }
    
}
