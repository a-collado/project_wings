using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Grabable : MonoBehaviour, IInteractable
{

    //public UnityEvent interacted;
    private GameObject player;
    private Inventory playerInventory;
    private System.Diagnostics.Stopwatch stopWatch;
    

    void Awake() {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        playerInventory = player.GetComponent<Inventory>();
        stopWatch = new System.Diagnostics.Stopwatch();
        stopWatch.Start();
    }

    public void Update() {
        
    }

    public AnimationsEnum Interact()
    {
        double time = stopWatch.Elapsed.TotalMilliseconds/1000;
        if(time > 0.2)
        {
            grab();
            stopWatch.Restart();
            return AnimationsEnum.GRAB_LOW;
        }
        stopWatch.Restart();
        return AnimationsEnum.NONE;
    }

    public void Power()
    {
        throw new System.NotImplementedException();
    }

    public void grab(){

        ////Debug.Log("[Grabable]: " + playerInventory + " : " + this.gameObject);
        this.playerInventory.addItem(this.gameObject);
        this.gameObject.SetActive(false);
        GetComponent<Collider>().enabled = false;
        GetComponent<Grabable>().enabled = false;

    }

    public void activate(bool flag){
       
        this.enabled = flag;      
       if (flag){
            this.gameObject.layer = LayerMask.NameToLayer (LayerMask.LayerToName(3));
        }else{
            this.gameObject.layer = LayerMask.NameToLayer("Default");
        }
    
    }

     public bool isActive() {
        return this.enabled;
    }

    public bool isCompleted()
    {
        return isActive();
    }

}


