using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Made for objects that only have an animation component to be activated when decided

public class Puzzle : MonoBehaviour
{
    [SerializeField] private TreetoGrow[] toComplete;
    [SerializeField] private Animator[] toActivate;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(checkIfCompleted()){
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
        foreach (TreetoGrow piece in this.toComplete)
        {
            if (piece.isActive()){ //Si esta activo significa que aun no se ha completado el puzzle
                completed = false;
            }
        }
        return completed;
    }
}
