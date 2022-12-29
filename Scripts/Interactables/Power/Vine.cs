using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Vine : MonoBehaviour, IInteractable
{

    [SerializeField]
    private Collider colliderA;
    [SerializeField]
    private Collider colliderB;

    public UnityEvent interacted;


    public void Update() {

    }
  public void Interact()
    {
        interacted.Invoke();
    }

    public void Power()
    {
        throw new System.NotImplementedException();
    }

    public void transferPlayer(GameObject player){
        if(gameObject.transform.childCount > 0)
        {
            if (player.GetComponent<Interactor>().triggerInRange(colliderA))
                player.transform.position = colliderB.transform.position;
            if (player.GetComponent<Interactor>().triggerInRange(colliderB))
                player.transform.position = colliderA.transform.position;    
        }
    }

    public void activate(bool flag){
        this.enabled = flag;
    }

    public bool isActive() {
        return this.enabled;
    }

}
