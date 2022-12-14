using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Made for objects that only have an animation component to be activated when decided

public class Puzzle : MonoBehaviour
{
    
  
    [SerializeField] private GameObject[] toComplete; //! Solo pueden ser objetos de tipo IInteractable

    
    [SerializeField] private Animator[] toActivate;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(checkIfCompleted()){
            Debug.Log("Completado");
            foreach (Animator animator in toActivate)
            {
                //Do the thing
                animator.SetBool("activate", true);

                Debug.Log("[Activable]: " + this.gameObject + " has been activated");
            }
            
            //And then disable
            this.enabled = false;
        }
    }

    public bool checkIfCompleted(){
        bool completed = true;
        foreach (GameObject obj in this.toComplete)
        {   
            IInteractable inter = obj.GetComponent<IInteractable>();
            if (inter.isActive()){ //Si esta activo significa que aun no se ha completado el puzzle
                completed = false;
            }
        }
        return completed;
    }
}
