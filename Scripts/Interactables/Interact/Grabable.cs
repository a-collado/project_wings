using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Grabable : MonoBehaviour, IInteractable
{

    //public UnityEvent interacted;
    private GameObject player;
    private Inventory playerInventory;
    private Collider collider;
    private System.Diagnostics.Stopwatch stopWatch;

    public bool localPos = false;
    public Vector3 localPosition;
    public Quaternion localRotation;
    

    void Awake() {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        playerInventory = player.GetComponent<Inventory>();
        collider = GetComponent<Collider>();
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
            return tag switch
            {
               // "coin" => AnimationsEnum.NONE,
                _ => AnimationsEnum.GRAB_LOW
            };
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
        setPosition();
        
        this.collider.enabled = false;
        this.enabled = false;

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

    public GameObject getGameObject()
    {
        return this.transform.gameObject;
    }

    private void setPosition()
    {

        switch (tag)
        {
            case "Golden Apple":
                localPosition = new Vector3(-0.0008f, -0.0095f, 0.0246f);
                localRotation = Quaternion.Euler(3.446f, 88.611f, 80.153f);
                localPos = true;
                break;
        }
    }
}


