using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnTrigger : MonoBehaviour
{

    // Start is called before the first frame update
    [SerializeField] GameObject respawn;

    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player"){
            //Debug.Log("Trigger entered");
            //other.GetComponent<FasTPS.CharacterMovement>().LandingRoll = false;
            CharacterController playerController = playerController = other.GetComponent<CharacterController>();
            playerController.enabled = false;
            playerController.transform.position = respawn.transform.position;
            //player.transform.position = respawn.transform.position;
            playerController.enabled = true;
            //other.GetComponent<FasTPS.CharacterMovement>().LandingRoll = true;

        }

    }
}
