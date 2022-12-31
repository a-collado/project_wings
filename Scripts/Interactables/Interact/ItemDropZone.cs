using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemDropZone : MonoBehaviour, IInteractable
{
    
    [SerializeField] private string correctObjectTag; //Correct object for this ItemDropZone
    [SerializeField] private int numItems;
    private GameObject player; //player GameObject
    private Inventory playerInventory; //player Inventory
    private bool isCompleted; // true when this part is done

    private System.Diagnostics.Stopwatch stopWatch; // delay to prevent key spam

    void Awake() { // Load and initialize stuff
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        playerInventory = player.GetComponent<Inventory>();
        isCompleted = false;
        stopWatch = new System.Diagnostics.Stopwatch();
        stopWatch.Start();
    }

    public void Update() {
        //Something
    }

    public void Interact()
    {   //On Interact do drop() and prevent spam
        double time = stopWatch.Elapsed.TotalMilliseconds/1000;
        if(time > 0.2)
        {
            drop();
        }
        stopWatch.Restart();
        
    }

    public void Power()
    {
        //Nothing implemented here
        throw new System.NotImplementedException();
    }

    public void drop(){
        Debug.Log("[ItemDropZone]: drop: " + numItems);

        if (!isCompleted ){
            numItems = playerInventory.dropItems(numItems, correctObjectTag);

            if (numItems <= 0){
                complete();
            }
        }        

        
    }

   

    //Completes everything and lock everything in place
    public void complete() {
        //Deactivate this
        this.activate(false);
        //Activate childs
        Debug.Log("[ItemDropZone]: Completed");


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
}


