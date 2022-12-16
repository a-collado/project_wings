using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pickable : MonoBehaviour, IInteractable
{

    //public UnityEvent interacted;
    private GameObject player;
    private Inventory playerInventory;

    void Awake() {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        playerInventory = player.GetComponent<Inventory>();
    }

    public void Interact()
    {
        //interacted.Invoke();
        pick();
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
    }

   /*  public void rotateObjectXN90(GameObject o)
    {
        o.transform.Rotate(new Vector3(-90,0,0));

    } */

}


