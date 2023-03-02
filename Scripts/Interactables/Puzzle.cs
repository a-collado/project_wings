using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Made for objects that only have an animation component to be activated when decided
[RequireComponent(typeof(AudioSource))]
public class Puzzle : MonoBehaviour
{
    
  
    [SerializeField] private GameObject[] toComplete; //! Solo pueden ser objetos de tipo IInteractable

    
    [SerializeField] private Animator[] toActivate;

    ////[SerializeField] private Camera puzzleCam;

    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(checkIfCompleted()){
            foreach (Animator animator in toActivate)
            {
                //Do the thing
                //Switch Main Camera to puzzleCam
                ////Camera.main.enabled = false;
                ////puzzleCam.enabled = true;
                animator.SetBool("activate", true);
                audioSource.Play();
                Debug.Log("[Activable]: " + this.gameObject + " has been activated");
            }
            Debug.Log("Puzzle completed");
            
            //And then disable
            this.enabled = false;
        }
    }

    public bool checkIfCompleted(){
        bool completed = true;
        foreach (GameObject obj in this.toComplete)
        {   
            IInteractable inter = obj.GetComponent<IInteractable>();
            if (inter.isCompleted()){ //Si esta activo significa que aun no se ha completado el puzzle
                completed = false;
            }
        }
        return completed;
    }
}
