using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawn_trigger : MonoBehaviour
{

    // Start is called before the first frame update
    [SerializeField] GameObject respawn;
    GameObject player;

    void Awake()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player"){
            Debug.Log("Trigger entered");
            player.transform.position = respawn.transform.position;
        }

    }
}
