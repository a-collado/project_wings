using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    public UnityEvent pulsed;

    private void OnTriggerEnter(Collider other) {

        if (other.tag == "Player")
            gameObject.transform.Translate(new Vector3(0, -0.4f, 0));// Aqui hay que poner una animacion
    }

    private void OnTriggerStay(Collider other) {
        
        if (other.tag == "Player")
            pulsed.Invoke();
    }

    private void OnTriggerExit(Collider other) {
         if (other.tag == "Player")
            gameObject.transform.Translate(new Vector3(0, 0.4f, 0));// Aqui hay que poner una animacion  
    }
}
