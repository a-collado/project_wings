using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TreetoGrow : MonoBehaviour, IInteractable
{


    [SerializeField] Animator animator;

    private System.Diagnostics.Stopwatch stopWatch; // delay to prevent key spam

    void Awake() { // Load and initialize stuff
        stopWatch = new System.Diagnostics.Stopwatch();
        animator = GetComponent<Animator>();
        stopWatch.Start();
    }
    public void Update() {

    }
    public AnimationsEnum Interact()
    {
       throw new System.NotImplementedException();
    }

    public void Power()
    {
        
        activateTree();
    }

    public void activateTree(){

        animator.SetBool("activate", true);
        activate(false);

    }

    public void activate(bool flag){
        this.enabled = flag;
    }

    public bool isActive() {
        return this.enabled;
    }

    public bool isCompleted()
        {
            return isActive();
        }
}
