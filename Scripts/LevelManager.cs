using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : MonoBehaviour
{
    //GameObjects
    public GameObject player;
    public GameObject stairs;
    public GameObject stairsObstacle;


    //States
    private int torchesPositioned = 0;
    private int numTorches = 2;
    //Stairs
    private bool stairsOut = false;
    private float stairsMoveSpeed = 0.2f;
    private float stairsMaxDistance = 600.0f;
    private float stairsCurrentDistance = 0;


    void Start()
    {
        stairsOut = false;
        numTorches = 1;
    }

    void Update()
    {

        //Step 1: Position Torches and then move stairs
        if (torchesPositioned >= numTorches && stairsCurrentDistance <= stairsMaxDistance && !stairsOut)
        {
            stairs.transform.position += -stairs.transform.up * stairsMoveSpeed * Time.deltaTime;
            stairsCurrentDistance += stairsMoveSpeed;
            Debug.Log("CurrentDistamce = " + stairsCurrentDistance);
            if (stairsCurrentDistance >= stairsMaxDistance) stairsOut = true;
        }
        //Step 2: Once Stairs are moved, unlock stairs and let the player walk
        if (stairsOut)
        {
            //Unlock Obstacle
            Destroy(stairsObstacle);
        }
    }

    public void addTorchPosition()
    {
        torchesPositioned++;

    }
}
