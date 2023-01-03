using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Vine : MonoBehaviour, IInteractable
{

    private bool isCompleted;

    [SerializeField] Animator animator;

    private System.Diagnostics.Stopwatch stopWatch; // delay to prevent key spam

    void Awake() { // Load and initialize stuff
        stopWatch = new System.Diagnostics.Stopwatch();
        animator = GetComponent<Animator>();
        stopWatch.Start();
    }
    public void Update() {

    }
    public void Interact()
    {
       throw new System.NotImplementedException();
    }

    public void Power()
    {
        
        activateWine();
    }

    public void activateWine(){

        animator.SetBool("isGrown", true);

    }

    public void activate(bool flag){
        this.enabled = flag;
    }

    public bool isActive() {
        return this.enabled;
    }

}
