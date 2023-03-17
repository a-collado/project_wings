using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemToOrder : MonoBehaviour, IInteractable
{
    
    private AudioSource audioSource; //sound that may do when you interact
    [SerializeField] List<int> positions; //positions that should take this child in the parent
    private ItemsInOrder parent; //parent check the order of the children

    void Awake()
    {
        parent = GetComponentInParent<ItemsInOrder>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void Update()
    {
        //something
    }

    public AnimationsEnum Interact()
    {
  
        if (isActive()){
            audioSource.Play();
            parent.ItemToOrderEvent(positions);
            activate(false);
            StartCoroutine(activateCoroutine(true));
        }

        return AnimationsEnum.NONE;
      
    }

    IEnumerator activateCoroutine(bool flag){
        yield return new WaitForSeconds(0.25f);
        this.enabled = flag;
    }

    public void Power() {
        //Nothing implemented here
        throw new System.NotImplementedException();
    }

    public void activate(bool flag) { 
        this.enabled = flag;
    }

    public bool isActive() { return this.enabled; }

    public bool isCompleted() { return false; }
}
