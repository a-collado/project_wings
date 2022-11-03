using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickNDrop : MonoBehaviour, IInteractable
{

    public UnityEvent interacted;

    public void Interact()
    {
        interacted.Invoke();
    }

    public void Power()
    {
        throw new System.NotImplementedException();
    }

    public void attachToPlayer(GameObject player){
        gameObject.transform.SetParent(player.transform);
        gameObject.transform.localPosition = new Vector3(0.07f,0.87f,0.64f);
    }

    public void drop(GameObject p){

        if (p.transform.GetComponentInParent<Interactor>() != null)
        {
            p.transform.SetParent(gameObject.transform);
            p.transform.localPosition = new Vector3(0f,0f,0f);
            p.transform.rotation = Quaternion.identity;
        }
    }

    public void rotateObjectXN90(GameObject o)
    {
        o.transform.Rotate(new Vector3(-90,0,0));

    }

}


