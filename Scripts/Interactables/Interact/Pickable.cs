using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pickable : MonoBehaviour, IInteractable
{

    //public UnityEvent interacted;

    private Inventory _playerInventory;
    private System.Diagnostics.Stopwatch _stopWatch;
    

    void Awake() {
        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
        _playerInventory = player.GetComponent<Inventory>();
        
        _stopWatch = new System.Diagnostics.Stopwatch();
        _stopWatch.Start();
    }

    public void Update() {
        
    }

    public AnimationsEnum Interact()
    {
        double time = _stopWatch.Elapsed.TotalMilliseconds/1000;
        if(time > 0.2)
        {
            _stopWatch.Restart();
            AnimationsEnum animation = pick();
            return animation;
        }
        _stopWatch.Restart();
        return AnimationsEnum.NONE;
    }

    public void Power()
    {
        throw new System.NotImplementedException();
    }

    public AnimationsEnum pick(){
        
        if (_playerInventory.getBlock() != null) return AnimationsEnum.NONE;

        AnimationsEnum animation = transform.tag switch
        {
            "Torch" => AnimationsEnum.GRAB_TORCH,
            "Orb" => AnimationsEnum.PICK_TWO_LOW,
            _ => AnimationsEnum.PICK_TWO_LOW
        };

        _playerInventory.addBlock(gameObject);
        return animation;
    }
    

    public void activate(bool flag){
       
        this.enabled = flag;
        this.gameObject.layer = flag ? 3 : 0;
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


