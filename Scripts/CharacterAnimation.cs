using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class CharacterAnimation : MonoBehaviour
{

    [SerializeField] private InputActionReference powerButton;
    private Animator playerAnimator;
    private NavMeshAgent player;

    private bool objectPicked;
/*
    private void OnEnable() {
        IInteractable._interaction += Interact();
    }

    private void OnDisable() {
        IInteractable._interaction -= Interact();

    }
*/
    private void Start() {
        playerAnimator = gameObject.GetComponent<Animator>();
        player = gameObject.GetComponent<NavMeshAgent>();
        objectPicked = false;
    }

    // Update is called once per frame
    void Update()
    {
        //  Maquina de estados para las animaciones

        if(player.velocity != Vector3.zero){    // Si la velocidad es no 0, se usa la animacion de caminar.
            playerAnimator.SetBool("isWalking", true);
        }else {                                 // En caso contrario, se usa la animacion de idle.
            playerAnimator.SetBool("isWalking", false);
        }

        /*if (powerButton.action.ReadValue<float>() > 0){
            
        }*/

    
    }

    public void Interact(){
        playerAnimator.SetTrigger("interact");
    }

    public void pickObject(bool b){
        objectPicked = b;
        playerAnimator.SetBool("objectPicked", b);

    }

}
