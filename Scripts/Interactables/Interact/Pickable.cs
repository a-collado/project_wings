using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pickable : MonoBehaviour, IInteractable
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
            pick();
            stopWatch.Restart();
            return AnimationsEnum.PICK_TWO_LOW;
        }
        stopWatch.Restart();
        return AnimationsEnum.NONE;
    }

    public void Power()
    {
        throw new System.NotImplementedException();
    }

    public void pick(){

        playerInventory.addBlock(this.gameObject);
        gameObject.transform.SetParent(player.transform);
        gameObject.transform.localPosition = new Vector3(0.07f,0.87f,0.64f);
        activate(false);
        this.gameObject.layer = LayerMask.NameToLayer("Default");
        Debug.Log("["+ this.gameObject + "]: attachToPlayer()");
    

    }

   /*  public void rotateObjectXN90(GameObject o)
    {
        o.transform.Rotate(new Vector3(-90,0,0));

    } */

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


