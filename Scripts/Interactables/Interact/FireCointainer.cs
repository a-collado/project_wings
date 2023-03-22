using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCointainer : MonoBehaviour, IInteractable
{

    private GameObject player;
    private Inventory playerInventory;
    [SerializeField]
    private ParticleSystem particles;

    private void Awake() {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        playerInventory = player.GetComponent<Inventory>();
    }

    public void activate(bool flag)
    {
        this.enabled = flag;
    }

    public AnimationsEnum Interact()
    {
        if(playerInventory.getBlock() != null && playerInventory.getBlock().GetComponent<Pickable>() != null){
            particles.gameObject.SetActive(true);
            activate(false);
        }

        return AnimationsEnum.NONE;
    }


    public bool isActive() {
        return this.enabled;
    }

    public bool isCompleted()
    {
        return isActive();
    }

    public void Power()
    {
    }

    void IInteractable.Update()
    {
    }
}
