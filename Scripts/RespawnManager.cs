using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FasTPS;

public class RespawnManager : MonoBehaviour
{

    [SerializeField] private FasTPS.CharacterMovement playerMovement;
    [SerializeField] private GameObject[] respawnPoints;
    [SerializeField] private bool fallDamage = true;

    [SerializeField] private float maxSpeed = 10.0f;

    private CharacterController playerController;


    void Awake(){
        playerController = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Update()
    {
    

        if ((playerMovement.GetVelocity().y < -maxSpeed && fallDamage && !playerMovement.IsOnGround()))
        {
            Respawn();
        }
    }

    public void Respawn(){
        
        Debug.Log("Loading respawn");
        Vector3 playerPosition = playerMovement.gameObject.transform.position;

        //Look for the closest respawn point
        GameObject closestRespawnPoint = respawnPoints[0];
        float closestDistance = Vector3.Distance(playerPosition, closestRespawnPoint.transform.position);
        foreach (GameObject respawnPoint in respawnPoints)
        {
            float distance = Vector3.Distance(playerPosition, respawnPoint.transform.position);
            if (distance < closestDistance)
            {
                
                closestRespawnPoint = respawnPoint;
                closestDistance = distance;
            } 
        }

        Debug.Log("playerDeathPos = " + playerPosition +  " from: " + playerMovement + " respawning to closest: " + closestRespawnPoint + " at :" + closestDistance);

        //Move the player to the closest respawn point
        playerController.enabled = false;
        playerController.transform.position = closestRespawnPoint.transform.position;
        playerController.enabled = true;
        playerMovement.ResetVelocity();
       
    }
}
