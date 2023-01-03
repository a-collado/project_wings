using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Made for objects that only have an animation component to be activated when decided

public class Activable : MonoBehaviour
{
    private IInteractable[] childs;
    private Animator animator;
    void Start()
    {
        childs = this.gameObject.GetComponentsInChildren<IInteractable>();
        animator = this.gameObject.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(checkIfCompleted()){
            //Do the thing
            animator.SetBool("activate", true);
            //And then disable
            this.enabled = false;
        }
    }

    public bool checkIfCompleted(){
        bool completed = true;
        foreach (IInteractable child in this.childs)
        {
            if (child.isActive()){
                completed = false;
            }
        }
        return completed;
    }
}
