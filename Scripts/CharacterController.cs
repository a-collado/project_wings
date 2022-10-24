using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cinemachine;

public class CharacterController : MonoBehaviour
{
    // "SerializeField" hace que un elemento aparezca en el Inspector de Unity aunque este en privado.

    [SerializeField] private Camera cam;
    [SerializeField] private NavMeshAgent player;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private GameObject targetDest;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))    // Detectamos el click izquierdo del raton
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);    // Se dispara un rayo desde la camara a la posicion del raton
            RaycastHit hitPoint;

            if(Physics.Raycast(ray, out hitPoint))  // Si este rayo inpacta sobre cualquier geometria:
            {                 
                targetDest.transform.position = hitPoint.point;     // Se mueve el GameObject "targetDest" a la posicion en la que ha golpeado el rayo
                player.SetDestination(hitPoint.point);              // El jugador se mueve hacia el esta posicion.
            }
        }

        //  Maquina de estados para las animaciones

        if(player.velocity != Vector3.zero){    // Si la velocidad es no 0, se usa la animacion de caminar.
            playerAnimator.SetBool("isWalking", true);
            Debug.Log("Walking");
        }else {                                 // En caso contrario, se usa la animacion de idle.
            playerAnimator.SetBool("isWalking", false);
            Debug.Log("Not Walking");
        }
    
    }
}
