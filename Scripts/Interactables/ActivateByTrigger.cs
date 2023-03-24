using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateByTrigger : MonoBehaviour
{
    public bool completed = false;

    void Start() {
    }
    private void OnTriggerEnter(Collider other) {
        Complete();
        Debug.Log("[PressurePlate]: Pressed");    
    }

    private void OnTriggerStay(Collider other) {

    }

    private void OnTriggerExit(Collider other) {
        this.completed = false;
    }

    public void Complete(){
        this.completed = true;
    }
}
