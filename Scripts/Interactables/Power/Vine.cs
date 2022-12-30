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
       /*  if(gameObject.transform.childCount > 0)
        {
            if (player.GetComponent<Interactor>().triggerInRange(colliderA))
                player.transform.position = colliderB.transform.position;
            if (player.GetComponent<Interactor>().triggerInRange(colliderB))
                player.transform.position = colliderA.transform.position;    
        } */
        animator.SetBool("isGrown", true);

    }

    public void activate(bool flag){
        this.enabled = flag;
    }

    public bool isActive() {
        return this.enabled;
    }

}
