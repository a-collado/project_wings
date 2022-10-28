using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float _interactionPointRadius = 1.5f;
    [SerializeField] private LayerMask _interactableMask;
    [SerializeField] private InputActionReference mouseButton, mousePosition, powerButton;
    [SerializeField] private Camera cam;


    private int _numFound;

    private Collider[] _colliders = new Collider[3];

    private void Update() 
    {
        _numFound = Physics.OverlapSphereNonAlloc(gameObject.transform.position, 
        _interactionPointRadius, _colliders, _interactableMask);            // Detecta todosl os objetos en un radio dado al rededor del personaje.

        if (_numFound > 0)                                                  // Si se ha encontrado algun objeto
        {
            var interactable = _colliders[0].GetComponent<IInteractable>(); // Comprobas si es un objeto interactuable
            if (interactable != null && InteractorClicked(interactable))    // Detectamos si lo estamos pulsando con el raton
            {
                interactable.Interact();                           // Interactuamos con el objeto
            }
            if (interactable != null && powerButton.action.ReadValue<float>() > 0){
                interactable.Power();
            }
        }

    }

    public bool inRange(IInteractable target){
        for (int i = 0; i < _numFound; i++)                     // Miramos todos los objetos en nustro rango
        {
            var interactable = _colliders[i].GetComponent<IInteractable>();     // Miramos si son interactuables
            if (interactable == target) return true;                            // Si lo son, devolvemos true
        }
        return false;
    }

    private bool InteractorClicked(IInteractable target){
        if (mouseButton.action.ReadValue<float>() > 0)
        {
            Ray ray = cam.ScreenPointToRay(mousePosition.action.ReadValue<Vector2>());    // Se dispara un rayo desde la camara a la posicion del raton
            RaycastHit hitPoint;
            if(Physics.Raycast(ray, out hitPoint))  // Si este rayo inpacta sobre cualquier geometria:
            {         
                var interactable = hitPoint.transform.GetComponent<IInteractable>();
                if (interactable != null)
                    return interactable == target;
            }
        }
        return false;
    }

    // Esta funcion dibuja el radio de interaccion
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(gameObject.transform.position, _interactionPointRadius);
    }

    
}
