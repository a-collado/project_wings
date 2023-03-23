using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleNames : MonoBehaviour
{
    [SerializeField] private Animator[] toActivate;
    [SerializeField] private List<GameObject> letters;
    [SerializeField] private List<int> positions;

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
        if(checkIfCorrect()){
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

    public bool checkIfCorrect(){
        
        bool correct = true;
        for (int i = 0; i < letters.Count ; i++)
        {
            if (letters[i].GetComponent<RotateBy90>().currentPosition != positions[i])
            {
                correct = false;
            }
        }
        return correct;
    }
}
