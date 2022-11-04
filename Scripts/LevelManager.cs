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
    public GameObject water1;
    public GameObject water2;
    public GameObject waterObstacle;
    public GameObject path;
    public GameObject manivela1;
    public GameObject manivela2;
    public GameObject pathObstacle;

    //States
    private int torchesPositioned = 0;
    private int numTorches = 2;
    private bool key = false;
    //Stairs
    private bool stairsOut = false;
    private float stairsMoveSpeed = 0.2f;
    private float stairsMaxDistance = 600.0f;
    private float stairsCurrentDistance = 0;
    //Sequence
    private bool sequenceOkay = false;


    void Start()
    {


        sequenceOkay = true;
        key = true;
        //TestPlaying - Delete to play the complete level
        /*
        key = true;
        stairsOut = false;
        numTorches = 1;
        */
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
            stairsObstacle.SetActive(false);
        }
        
    }

    public void addTorchPosition()
    {
        torchesPositioned++;

    }

    public void pickUpKey()
    {
        key = true;
    }

    public void activateFountain()
    {
        if (key) {
            water1.SetActive(false);
            water2.SetActive(false);
            waterObstacle.SetActive(false);
        }
    }


    public void activatePortalPath()
    {
        //if combination okay
        //activate path and delete obstacle
        if (sequenceOkay)
        {
            path.SetActive(true);
            manivela1.SetActive(true);
            manivela2.SetActive(true);
            pathObstacle.SetActive(false);
}
    }
}
