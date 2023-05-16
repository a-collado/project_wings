using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class DropZone2 : MonoBehaviour, IInteractable
{
    
    [SerializeField] private List<Pickable> correctObjects; //Correct object for this DropZone
    private GameObject player; //player GameObject
    private Inventory playerInventory; //player Inventory
    private bool isComplete; // true when this part is done

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
            AnimationsEnum anim = drop();
            stopWatch.Restart();
            //return AnimationsEnum.DROP_TWO_LOW;
            return anim;
        }
        stopWatch.Restart();
        return AnimationsEnum.NONE;
        
    }

    public void Power()
    {
        //Nothing implemented here
        throw new System.NotImplementedException();
    }

    public AnimationsEnum drop(){
        
        //Miramos si el jugador tiene un bloque o si ya hay un objeto dentro de la DropZone
        
        if (playerInventory.getBlock() && this.gameObject.GetComponentInChildren<Pickable>() == null) {
            GameObject objectToDrop = playerInventory.getBlock();
            //Si el objeto es de tipo Interactor
            if (!objectToDrop.transform.GetComponentInParent<Interactor>()) return AnimationsEnum.NONE;
            //drop
            //objectToDrop.transform.SetParent(gameObject.transform);
            //objectToDrop.transform.localPosition = new Vector3(0f,0f,0f);
            //objectToDrop.transform.rotation = Quaternion.identity;

            //check if object is correct
            return checkObject(objectToDrop);
        }else
        {
            //Player picks the current object that is in the Zone without beeing itself

            if (gameObject.GetComponentInChildren<Pickable>() == null) return AnimationsEnum.NONE;
            GameObject children = gameObject.GetComponentInChildren<Pickable>().gameObject;
            playerInventory.addBlock(children);
                
            //children.transform.SetParent(playerHand.transform);
            //children.transform.localPosition = Vector3.zero;
            return AnimationsEnum.GRAB_HIGH;
        }
    }

    //Checks if the dropped object is the good one
    private AnimationsEnum checkObject(GameObject obj) {
        //Debug.Log("[DropZone]: checkObject()");
        Pickable pick = obj.GetComponent<Pickable>();
        if (pick == null) return AnimationsEnum.NONE;
        foreach (var item in correctObjects.Where(item => pick == item))
        {
            //Complete this dropZone
            isComplete = true;
        }

        return pick.dropAnimation;
    }

    public void activate(bool flag){
        this.enabled = flag;      
        if (flag){
            this.gameObject.layer = 3;
        }else{
            this.gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }

     public bool isActive() {

        return !isComplete;
        
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


