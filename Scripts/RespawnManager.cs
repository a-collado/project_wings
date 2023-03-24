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

    // Start is called before the first frame update
    void Update()
    {
        //Debug.Log(playerMovement.GetVelocity().y);
        bool hitDeath = false;
        

        if ((playerMovement.GetVelocity().y < -maxSpeed && fallDamage && !playerMovement.IsOnGround()) || hitDeath)
        {
            Respawn();
        }
    }

    public void Respawn(){
        
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

        //Move the player to the closest respawn point
        playerMovement.enabled = false;
        playerMovement.gameObject.transform.position = closestRespawnPoint.transform.position;
        playerMovement.enabled = true;
        playerMovement.ResetVelocity();
       
    }
}
