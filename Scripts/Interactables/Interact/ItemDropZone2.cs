using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEditor;
using UnityEditor.UIElements;

public class ItemDropZone2 : MonoBehaviour, IInteractable
{
    
    [SerializeField] private string correctObjectTag; //Correct object for this ItemDropZone
    [SerializeField] private int numItems;

    [SerializeField] private string ouputText1; //Text to show for asking item

    [SerializeField] private string ouputText2; //Text to show after completion

    [SerializeField] private bool text;
    
    private NotificationEvent notification;
    private GameObject player; //player GameObject
    private Inventory playerInventory; //player Inventory
    private bool isComplete; // true when this part is done

    private System.Diagnostics.Stopwatch stopWatch; // delay to prevent key spam

    void Awake() { // Load and initialize stuff
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        playerInventory = player.GetComponent<Inventory>();
        notification = GetComponent<NotificationEvent>();
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
        throw new System.NotImplementedException();
    }

    public void drop()
    {
        if (isComplete) return;
        numItems = playerInventory.dropItems(numItems, correctObjectTag);

        if (text){
            notification.setMessage(ouputText1);
            notification.showNotification();
        }

        if (numItems <= 0){
            complete();
        }


    }

   

    //Completes everything and lock everything in place
    public void complete() {
        if (text){
            notification.setMessage(ouputText2);
            notification.showNotification();
        }
        
        //Deactivate this
        this.activate(false);
        //Activate childs



    }


    public void activate(bool flag){
        this.enabled = flag;
        this.gameObject.layer = LayerMask.NameToLayer(flag ? LayerMask.LayerToName(3) : "Default");
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


