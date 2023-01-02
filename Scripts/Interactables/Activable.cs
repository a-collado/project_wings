using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activable : MonoBehaviour
{
    private IInteractable[] childs;
    void Start()
    {
        childs = this.gameObject.GetComponentsInChildren<IInteractable>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(checkIfCompleted()){
            //Do the thing

            //And then disable
            this.enabled = false;
        }
    }

    public bool checkIfCompleted(){
        bool completed = true;
        foreach (IInteractable child in this.childs)
        {
            if (child.isActive()){
                completed = false;
            }
        }
        return completed;
    }
}
