using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemToOrder : MonoBehaviour, IInteractable
{
    
    [SerializeField] AudioSource audioSource; //sound that may do when you interact
    [SerializeField] List<int> positions; //positions that should take this child in the parent
    private ItemsInOrder parent; //parent check the order of the children
    private System.Diagnostics.Stopwatch stopWatch; // delay to prevent key spam

    void awake()
    {
        parent = GetComponentInParent<ItemsInOrder>();
        
    }

    // Update is called once per frame
    public void Update()
    {
        //something
    }

    public AnimationsEnum Interact()
    {
        
        double time = stopWatch.Elapsed.TotalMilliseconds / 1000;
        if (time > 0.2)
        {
            parent.ItemToOrderEvent(positions) ;
            stopWatch.Restart();
            return AnimationsEnum.DROP_TWO_LOW;
        }
        stopWatch.Restart();
        return AnimationsEnum.NONE;
    }

    public void Power() {
        //Nothing implemented here
        throw new System.NotImplementedException();
    }

    public void activate(bool flag) { }

    public bool isActive() { return false; }

    public bool isCompleted() { return false; }
}
