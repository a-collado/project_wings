using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ItemDropMultiple : MonoBehaviour, IInteractable
{
    
    [SerializeField] private List<string> correctObjectTags; //Correct object for this ItemDropZone


    private GameObject player; //player GameObject
    private Inventory playerInventory; //player Inventory
    private bool isComplete; // true when this part is done
    [SerializeField]
    private GameObject next;

    private System.Diagnostics.Stopwatch stopWatch; // delay to prevent key spam

    void Awake() { // Load and initialize stuff
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        playerInventory = player.GetComponent<Inventory>();
        isComplete = false;
        stopWatch = new System.Diagnostics.Stopwatch();
        stopWatch.Start();
    }

    public void Update() {
        //Something
    }

    public AnimationsEnum Interact()
    {   //On Interact do drop() and prevent spam
        double time = stopWatch.Elapsed.TotalMilliseconds/1000;
        if(time > 0.2)
        {
            drop();
            stopWatch.Restart();
            return AnimationsEnum.DROP_TWO_LOW;
        }
        stopWatch.Restart();
        return AnimationsEnum.NONE;
        
    }

    public void Power()
    {
        //Nothing implemented here
    }

    public void drop(){
 
        if (!isComplete && playerInventory.getBlock() != null){

            GameObject block = playerInventory.getBlock();
            bool correctObj = false;
            foreach (string tag in correctObjectTags)
            {
                if (block.tag == tag){
                    correctObj = true;
                }
            }
            
            if (correctObj){
                playerInventory.dropBlock();
                block.transform.parent = gameObject.transform;
                block.transform.position = transform.position;
                if(next != null){
                    next.SetActive(true);
                    complete();
                }
                complete();
            }


        }        

        
    }

   

    //Completes everything and lock everything in place
    public void complete() {
        this.activate(false);

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
