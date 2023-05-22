using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCointainer : MonoBehaviour, IInteractable
{

    private GameObject player;
    private Inventory playerInventory;
    [SerializeField]
    private ParticleSystem particles;
    private AudioSource audio;

    private void Awake() {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        playerInventory = player.GetComponent<Inventory>();
        audio = GetComponent<AudioSource>();
    }

    public void activate(bool flag)
    {
        this.enabled = flag;
    }

    public void Update()
    {
    }

    public AnimationsEnum Interact()
    {
        if(playerInventory.getBlock() != null && playerInventory.getBlock().GetComponent<Pickable>() != null){
            audio.Play();
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
    
    public GameObject getGameObject()
    {
        return this.transform.gameObject;
    }
}
