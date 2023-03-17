using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawn_trigger : MonoBehaviour
{

    // Start is called before the first frame update
    [SerializeField] GameObject respawn;
    CharacterController playerController;

    void Awake()
    {
        playerController = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player"){
            Debug.Log("Trigger entered");
            playerController.enabled = false;
            playerController.transform.position = respawn.transform.position;
            //player.transform.position = respawn.transform.position;
            playerController.enabled = true;
        }

    }
}
