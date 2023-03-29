using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attachToPlatform : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other) {
        other.transform.GetComponent<FasTPS.CharacterMovement>().SteepSlope = true;
        other.transform.parent.GetComponent<FasTPS.PlayerInput>().disableIK(false);
        other.transform.parent.SetParent(transform.parent.transform);
    }

    private void OnTriggerExit(Collider other) {
        other.transform.GetComponent<FasTPS.CharacterMovement>().SteepSlope = false;
        other.transform.parent.SetParent(null);
        other.transform.parent.GetComponent<FasTPS.PlayerInput>().disableIK(true);
    }
}
