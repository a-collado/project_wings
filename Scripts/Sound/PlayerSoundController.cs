using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{

    private const int WALK_SOUND = 1;


    [SerializeField]
    private AudioClip walkSound;
    private AudioSource audioSource;
    private Animator animator;
    private int currentSound = -1;
  
    void Start() {
        audioSource = gameObject.GetComponent<AudioSource>();
        animator = gameObject.GetComponent<Animator>();

    }


   
    void Update(){
        //if (animator.GetBool("isWalking") && !audioSource.isPlaying){
        if ((animator.GetFloat("StandingVelocityX") != 0 || animator.GetFloat("StandingVelocityZ") != 0) && animator.GetBool("IsGrounded") && !audioSource.isPlaying){
            Debug.Log("VAR");
            currentSound = WALK_SOUND;
            audioSource.clip = walkSound;
            audioSource.Play();
            audioSource.loop = true;
        }else {
            if (currentSound == WALK_SOUND)
                audioSource.Stop();
        }
        
    }


        
    

        
    
}
