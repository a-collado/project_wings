using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DropZone : MonoBehaviour, IInteractable
{

    //public UnityEvent interacted;
    private GameObject block;
    private GameObject player;
    private Inventory playerInventory;

    void Awake() {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        playerInventory = player.GetComponent<Inventory>();
    }
    public void Interact()
    {
        //interacted.Invoke();
        drop();
    }

    public void Power()
    {
        throw new System.NotImplementedException();
    }

    public void drop(){
        
        if (playerInventory.getBlock()) {
            GameObject objectToDrop = playerInventory.dropBlock();
            block = objectToDrop;
            Debug.Log("["+ this.gameObject + "]: drop()");
            if (objectToDrop.transform.GetComponentInParent<Interactor>())
            {
                objectToDrop.transform.SetParent(gameObject.transform);
                objectToDrop.transform.localPosition = new Vector3(0f,0f,0f);
                objectToDrop.transform.rotation = Quaternion.identity;
            }
        }else {
            playerInventory.addBlock(block);
            block = null;
            block.transform.SetParent(player.transform);
            block.transform.localPosition = new Vector3(0.07f,0.87f,0.64f);
        }
    }

    public GameObject getBlock() {
        return this.block;
    }

}


