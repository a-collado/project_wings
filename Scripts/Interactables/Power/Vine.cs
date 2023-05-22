using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class Vine : MonoBehaviour, IInteractable
{

    [SerializeField] private Animator animator;
    [SerializeField] private GameObject climbingStation;
    ////private OffMeshLink meshLink;

    private System.Diagnostics.Stopwatch stopWatch; // delay to prevent key spam

    void Awake() { // Load and initialize stuff
        stopWatch = new System.Diagnostics.Stopwatch();
        //animator = GetComponent<Animator>();
        stopWatch.Start();
        ////meshLink = GetComponent<OffMeshLink>();
    }
    public void Update() {

    }
    public AnimationsEnum Interact()
    {
       throw new System.NotImplementedException();
    }

    public void Power()
    {
        
        activateWine();
    }

    public void activateWine(){
        ////meshLink.activated = true;
        animator.SetBool("isGrown", true);
        activate(false);
        this.gameObject.GetComponent<Collider>().enabled = false;
        climbingStation.SetActive(true);

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

    public GameObject getGameObject()
    {
        return this.transform.gameObject;
    }
}
