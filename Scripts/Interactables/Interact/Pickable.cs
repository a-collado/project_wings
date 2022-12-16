using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pickable : MonoBehaviour, IInteractable
{

    //TODO:
    /*
        - Evitar long press, como?

    */


    //public UnityEvent interacted;
    private GameObject player;
    private Inventory playerInventory;
      private System.Diagnostics.Stopwatch stopWatch;
    private bool actionPerformed;

    void Awake() {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        playerInventory = player.GetComponent<Inventory>();
        stopWatch = new System.Diagnostics.Stopwatch();
        stopWatch.Start();
        actionPerformed = false;
    }

    void Update() {
        double time = stopWatch.Elapsed.TotalMilliseconds/1000;
        if(actionPerformed){
            if(time > 0.2)
            {
                pick();
            }
            stopWatch.Restart();
        }

    }

    public void Interact()
    {
        //interacted.Invoke();
        actionPerformed = true;
    }

    public void Power()
    {
        throw new System.NotImplementedException();
    }

    public void pick(){

        playerInventory.addBlock(this.gameObject);
        gameObject.transform.SetParent(player.transform);
        gameObject.transform.localPosition = new Vector3(0.07f,0.87f,0.64f);
        Debug.Log("["+ this.gameObject + "]: attachToPlayer()");
    
        actionPerformed=false;

    }

   /*  public void rotateObjectXN90(GameObject o)
    {
        o.transform.Rotate(new Vector3(-90,0,0));

    } */

}


