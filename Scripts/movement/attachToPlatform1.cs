using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attachToPlatform1 : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other) {
        Transform otherTransform = other.transform;
        if (otherTransform.name != "TPSController") return;
        
        otherTransform.GetComponent<FasTPS.CharacterMovement>().SteepSlope = true;
        var parent = otherTransform.parent;
        parent.GetComponent<FasTPS.PlayerInput>().disableIK(false);
        StartCoroutine(disableMovement(parent.GetComponent<FasTPS.PlayerInput>()));
        StartCoroutine(enableMovement(parent.GetComponent<FasTPS.PlayerInput>()));
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

    IEnumerator disableMovement(FasTPS.PlayerInput input)
    {
        yield return new WaitForSeconds(0.5f);
        input.disableMovement = true;
        input.Jump = false;
    }
    
    IEnumerator enableMovement(FasTPS.PlayerInput input)
    {
        yield return new WaitForSeconds(4.0f);
        input.disableMovement = false;
        input.Jump = true;
    }
    

}
