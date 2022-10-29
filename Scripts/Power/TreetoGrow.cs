using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TreetoGrow : MonoBehaviour, IInteractable
{

    public UnityEvent powered;

    public void Interact()
    {
        throw new System.NotImplementedException();
    }

    public void Power()
    {
        powered.Invoke();
    }

}
