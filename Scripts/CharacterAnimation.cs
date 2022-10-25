using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
public class CharacterAnimation : MonoBehaviour
{

    private Animator playerAnimator;
    private NavMeshAgent player;

    private void Start() {
        playerAnimator = gameObject.GetComponent<Animator>();
        player = gameObject.GetComponent<NavMeshAgent>();
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
    
    }
}
