using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate2 : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Transform beam1;
    [SerializeField] private Transform beam2;

    private bool completed = false;

    void Start() {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player" && !completed){
            animator.SetBool("pressed", true);
            beam1.localRotation = Quaternion.Euler(160f, 0, 0);
            beam2.localRotation = Quaternion.Euler(160f, 0, 0);
        }
            
    }

    private void OnTriggerStay(Collider other) {

    }

    private void OnTriggerExit(Collider other) {
         if (other.tag == "Player" && !completed)
            animator.SetBool("pressed", false);
    }

    public void Complete(){
        this.completed = true;
    }
}
