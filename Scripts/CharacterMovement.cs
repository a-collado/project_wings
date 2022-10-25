using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    // "SerializeField" hace que un elemento aparezca en el Inspector de Unity aunque este en privado.

    [SerializeField] private InputActionReference mouseButton, mousePosition;
    [SerializeField] private Camera cam;
    [SerializeField] private NavMeshAgent player;
    [SerializeField] private GameObject targetDest; 


    // Estos dos metodos son para utilizar el Input System
    private void OnEnable() {
        mouseButton.action.performed += Move;
    }

    private void OnDisable() {
        mouseButton.action.performed -= Move;
    }

    // Esta funcion se llama cada vez que alguien pulse el boton de moverse (click izquierdo)
    private void Move(InputAction.CallbackContext obj)
    {
        Ray ray = cam.ScreenPointToRay(mousePosition.action.ReadValue<Vector2>());    // Se dispara un rayo desde la camara a la posicion del raton
        RaycastHit hitPoint;
        if(Physics.Raycast(ray, out hitPoint))  // Si este rayo inpacta sobre cualquier geometria:
        {                 
            targetDest.transform.position = hitPoint.point;     // Se mueve el GameObject "targetDest" a la posicion en la que ha golpeado el rayo
            player.SetDestination(hitPoint.point);              // El jugador se mueve hacia el esta posicion.
        }
        
    }

}
