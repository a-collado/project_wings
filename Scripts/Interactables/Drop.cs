using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Drop : MonoBehaviour, IInteractable
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

    public void drop(GameObject p){

        if (p.transform.GetComponentInParent<Interactor>() != null)
        {
            p.transform.SetParent(gameObject.transform);
            p.transform.localPosition = new Vector3(0f,0f,0f);
        }
    }
}