using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{

    [SerializeField] private InputActionReference mouseButton, mousePosition;
    [SerializeField] private Camera cam;
    [SerializeField] private Transform targetDest; 
    [SerializeField] private CursorController cursorController;

    private NavMeshAgent player;
    private Interactor interactor;

    private MeshRenderer destinationIcon;

    private void Awake() {
        player = GetComponent<NavMeshAgent>();
        interactor = GetComponent<Interactor>();
        destinationIcon = targetDest.transform.GetChild(0).GetComponent<MeshRenderer>();
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
         int layerMask = 1 << 8;
        if(Physics.Raycast(ray, out hitPoint, Mathf.Infinity, layerMask))  // Si este rayo inpacta sobre cualquier geometria:
        {   


            var interactable = hitPoint.transform.GetComponent<IInteractable>(); // Comprobas si es un objeto interactuable

            if (mouseButton.action.ReadValue<float>() > 0)
            {
                Animation destAnim = destinationIcon.GetComponentInChildren<Animation>();
                destAnim.Stop();
                destAnim.Play();
                destinationIcon.enabled = true;   // Mostramos el marcador    
            }

            if (interactable != null)                                            // Si el objeto es interactuable: 
            {
                //cursorController.ChangeCursor(CursorController.Cursors.cursorClicked);          // Cambiamos el cursor
                destinationIcon.enabled = false;  // No mostramos en marcador     
            }

            /*
            if (!interactor.inRange(interactable))    // Detectamos si esta en el rango del personaje
            {
                 Move(hitPoint);                         // Interactuamos con el objeto
            }*/

            Move(hitPoint);

        }
        
    }

    private void Move(RaycastHit hitPoint){
        targetDest.position = hitPoint.point;     // Se mueve el GameObject "targetDest" a la posicion en la que ha golpeado el rayo
        player.SetDestination(hitPoint.point);              // El jugador se mueve hacia el esta posicion.
    }

}
