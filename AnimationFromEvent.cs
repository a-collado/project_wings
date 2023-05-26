using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFromEvent : MonoBehaviour
{

    [SerializeField] Animator animator;
    void Start()
    {
        
    }
    
    public void activate(){
        Debug.Log("activate");
        animator.SetBool("activate", true);
    }
        // Update is called once per frame
    void Update()
    {
        
    }
}
