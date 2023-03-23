using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crouchCollider : MonoBehaviour
{
    private FasTPS.CharacterMovement movement;
    private BoxCollider _collider;

    private void Awake() {
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<FasTPS.CharacterMovement>();
        _collider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movement.IsCrouching){
            _collider.enabled = false;
        } else {
            _collider.enabled = true;
        }
    }
}
