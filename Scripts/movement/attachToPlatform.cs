using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attachToPlatform : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other) {
        other.transform.SetParent(transform.parent.transform);
    }

    private void OnTriggerExit(Collider other) {
        other.transform.SetParent(null);
    }
}
