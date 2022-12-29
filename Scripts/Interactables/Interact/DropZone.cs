using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DropZone : MonoBehaviour, IInteractable
{
    
    [SerializeField] private Pickable correctObject; //Correct object for this DropZone
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
        Debug.Log("[DropZone]: drop");
        //Miramos si el jugador tiene un bloque o si ya hay un objeto dentro de la DropZone
        

        if (playerInventory.getBlock() && this.gameObject.GetComponentInChildren<Pickable>() == null) {
            GameObject objectToDrop = playerInventory.dropBlock();
            //Si el objeto es de tipo Interactor
            if (objectToDrop.transform.GetComponentInParent<Interactor>())
            {
                //drop
                objectToDrop.transform.SetParent(gameObject.transform);
                objectToDrop.transform.localPosition = new Vector3(0f,0f,0f);
                objectToDrop.transform.rotation = Quaternion.identity;

                //check if object is correct
                checkObject(objectToDrop);
            }
        }else {
            //Player picks the current object that is in the Zone without beeing itself
            
            if (gameObject.GetComponentInChildren<Pickable>() != null){
                GameObject children = gameObject.GetComponentInChildren<Pickable>().gameObject;
                playerInventory.addBlock(children);
                Debug.Log("[DropZone]: children: " + children);
                children.transform.SetParent(player.transform);
                children.transform.localPosition = new Vector3(0.07f,0.87f,0.64f);
            }
        }
    }

    //Checks if the dropped object is the good one
    public void checkObject(GameObject obj) {
        Debug.Log("[DropZone]: checkObject()");
        Pickable pick = obj.GetComponent<Pickable>();
        if( pick != null) {
            if(pick == correctObject) {
                //Complete this dropZone
                isCompleted = true;
                complete();
            }
        }
    }


    //Check to complete everything and lock everything in place
    public void complete() {
        bool isNotComplete = false;
        //If this and all childrens of parents that have the dropZone Component have
        GameObject parent = this.gameObject.transform.parent.gameObject;
        DropZone[] zones = parent.GetComponentsInChildren<DropZone>();
        Debug.Log("[DropZone]: complete() loading parent of " + this + " parent is: " + parent);
        Debug.Log("[DropZone]: complete() loading all childrens of parent " + parent + " : " + zones + " ,is of length: " + zones.Length);


        //Check for every child
        Debug.Log("[DropZone]: complete() checking if all DropZones are completed");
        foreach (DropZone zone in zones)
        {
             Debug.Log("[DropZone]: complete() checking zone: " + zone + " zone is completed = " + zone.isCompleted);
            if(!zone.isCompleted){ //if one is not completed
                isNotComplete = true; //the puzzle is not completed
                Debug.Log("[DropZone] One of the zones is not completed so it is not completed, zone : " + zone);
                break; // so we exit
            }
        }

        if (!isNotComplete){ // if everything has been completed we lock and activate next
            //activate parent
            Debug.Log("[DropZone]: complete() iscomplete");
            Debug.Log("[DropZone]: Activating parent of DropZone: " + parent.GetComponent<IInteractable>());

            parent.GetComponent<IInteractable>().activate(true); 
            //This is because we will always activate the parent component of the
            //DropZone once it is correctly dropped
            //lock currents
            foreach (DropZone zone in zones){
                   Debug.Log("[DropZone]: Deactivating childrens of DropZone: " + parent.GetComponent<IInteractable>());
                   zone.activate(false);
            }
        }
        
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

        //Check if player is holding an object
        if (this.enabled){
            if (playerInventory.getBlock() != null){
                return true;
            }
            return false;        
        }
        return false;
        
    }
}


