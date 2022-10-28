using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float _interactionPointRadius = 1.5f;
    [SerializeField] private LayerMask _interactableMask;
    [SerializeField] private InputActionReference mouseButton, mousePosition;
    [SerializeField] private Camera cam;


    private int _numFound;

    private Collider[] _colliders = new Collider[3];

    // Esta funcion detecta objetos interactuables en un cierto radio.
    private void Update() 
    {
        _numFound = Physics.OverlapSphereNonAlloc(gameObject.transform.position, 
        _interactionPointRadius, _colliders, _interactableMask);

        if (_numFound > 0)
        {
            var interactable = _colliders[0].GetComponent<IInteractable>();
            if (interactable != null && InteractorClicked(interactable))
            {
                interactable.Interact();
            }
        }

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
