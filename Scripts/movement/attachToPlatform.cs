using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attachToPlatform : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other) {
        Transform otherTransform = other.transform;
        if (otherTransform.name != "TPSController") return;
        
        otherTransform.GetComponent<FasTPS.CharacterMovement>().SteepSlope = true;
        var parent = otherTransform.parent;
        parent.GetComponent<FasTPS.PlayerInput>().disableIK(false);
        parent.SetParent(transform.parent.transform);
    }

    private void OnTriggerExit(Collider other) {
        Transform otherTransform = other.transform;
        if (otherTransform.name != "TPSController") return;
        
        otherTransform.GetComponent<FasTPS.CharacterMovement>().SteepSlope = false;
        var parent = otherTransform.parent;
        parent.SetParent(null);
        parent.GetComponent<FasTPS.PlayerInput>().disableIK(true);
    }
}
