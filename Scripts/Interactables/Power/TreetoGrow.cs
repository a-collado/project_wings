using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TreetoGrow : MonoBehaviour, IInteractable
{

    public UnityEvent powered;

    public void Update(){

    }

    public void Interact()
    {
        throw new System.NotImplementedException();
    }

    public void Power()
    {
        powered.Invoke();
    }

    public void ActivateObject(GameObject o)
    {
        o.GetComponent<Animator>().SetBool("hasGrown", true);
        Debug.Log("Grow");
    }

    public void activate(bool flag){
        this.enabled = flag;
    }

     public bool isActive() {
        return this.enabled;
    }
}
