using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    public UnityEvent pulsed;

    private void OnTriggerEnter(Collider other) {

        if (other.tag == "Player")
            gameObject.transform.position =  new Vector3 (gameObject.transform.position.x, 1f ,gameObject.transform.position.z);// Aqui hay que poner una animacion
    }

    private void OnTriggerStay(Collider other) {
        
        if (other.tag == "Player")
            pulsed.Invoke();
    }

    private void OnTriggerExit(Collider other) {
         if (other.tag == "Player")
            gameObject.transform.position =  new Vector3 (gameObject.transform.position.x, 1.379f ,gameObject.transform.position.z);// Aqui hay que poner una animacion  
    }
}
