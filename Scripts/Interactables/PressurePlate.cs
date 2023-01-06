using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private Toggleable[] toggleables;
    private Animator animator;

    private bool completed = false;

    void Start() {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other) {
        Debug.Log(other);
        if (other.tag == "Player" && !completed){
            Debug.Log("[PressurePlate]: Pressed");
            animator.SetBool("pressed", true);
            foreach (Toggleable tog in toggleables)
            {
                tog.toggle();
            }
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
