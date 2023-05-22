using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PuzzleActByTrig : MonoBehaviour
{
    [SerializeField] private GameObject[] toComplete; //! Solo pueden ser objetos de tipo ActivateByTrigger

    
    [SerializeField] private Animator[] toActivate;

    [Header("Cameras")]
    [SerializeField] private int resolveCameraIndex = -1;
    [SerializeField] private VirtualCamerasManager cameraManager;

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
                if (resolveCameraIndex != -1 && cameraManager != null)
                    cameraManager.SetVirtualCamera(resolveCameraIndex, 5f);
                
                animator.SetBool("activate", true);
                audioSource.Play();
                //Debug.Log("[Activable]: " + animator + " has been activated");
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
            ActivateByTrigger inter = obj.GetComponent<ActivateByTrigger>();
            if (!inter.completed){ 
                completed = false;
            }
        }
        return completed;
    }
}
