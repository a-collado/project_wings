using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DropZone : MonoBehaviour, IInteractable
{

    public UnityEvent interacted;
    private GameObject block;
    public void Interact()
    {
        interacted.Invoke();
    }

    public void Power()
    {
        throw new System.NotImplementedException();
    }

    public void drop(){
        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
        Inventory inventory = player.GetComponent<Inventory>();
        if (inventory.getBlock()) {
            GameObject objectToDrop = inventory.dropBlock();
            block = objectToDrop;
            Debug.Log("["+ this.gameObject + "]: drop()");
            if (objectToDrop.transform.GetComponentInParent<Interactor>())
            {
                objectToDrop.transform.SetParent(gameObject.transform);
                objectToDrop.transform.localPosition = new Vector3(0f,0f,0f);
                objectToDrop.transform.rotation = Quaternion.identity;
            }
        }else {
            inventory.addBlock(block);
            block = null;
            block.transform.SetParent(player.transform);
            block.transform.localPosition = new Vector3(0.07f,0.87f,0.64f);
        }
    }

    public GameObject getBlock() {
        return this.block;
    }

}


