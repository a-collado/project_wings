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
    [SerializeField] private GameObject targetDest; 
    private NavMeshAgent player;
    private Interactor interactor;

    private void Awake() {
        player = GetComponent<NavMeshAgent>();
        interactor = GetComponent<Interactor>();
    }

    // Estos dos metodos son para utilizar el Input System
    private void OnEnable() {
        mouseButton.action.performed += Click;
    }

    private void OnDisable() {
        mouseButton.action.performed -= Click;
    }

    // Esta funcion se llama cada vez que alguien pulse el boton de moverse (click izquierdo)
    private void Click(InputAction.CallbackContext obj)
    {
        Ray ray = cam.ScreenPointToRay(mousePosition.action.ReadValue<Vector2>());    // Se dispara un rayo desde la camara a la posicion del raton
        RaycastHit hitPoint;
        if(Physics.Raycast(ray, out hitPoint))  // Si este rayo inpacta sobre cualquier geometria:
        {   
            var interactable = hitPoint.transform.GetComponent<IInteractable>(); // Comprobas si es un objeto interactuable
            if (!interactor.inRange(interactable))    // Detectamos si lo estamos pulsando con el raton
            {
                 Move(hitPoint);                         // Interactuamos con el objeto
            }

        }
        
    }

    private void Move(RaycastHit hitPoint){
        targetDest.transform.position = hitPoint.point;     // Se mueve el GameObject "targetDest" a la posicion en la que ha golpeado el rayo
        player.SetDestination(hitPoint.point);              // El jugador se mueve hacia el esta posicion.
    }

}
