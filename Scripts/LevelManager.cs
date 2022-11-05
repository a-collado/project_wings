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
    public GameObject portalA;
    public GameObject portalB;
    public GameObject portalPrefab;

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
    private int[] sequence = new int[4];
    private int currentSeq;
    private bool _portalA, _portalB;

    void Start()
    {

        currentSeq = 0;
        sequenceOkay = false;
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
            path.SetActive(true);
            manivela1.SetActive(true);
            manivela2.SetActive(true);
            pathObstacle.SetActive(false);
    }

    public void setSequence(int num){
        int[] correctSeq = {1,2,3,4};
        sequence[currentSeq] = num;
        if(currentSeq == 3)
        {
            if (sequence[0] == correctSeq[0] && sequence[1] == correctSeq[1] && sequence[2] == correctSeq[2] && sequence[3] == correctSeq[3])
                activatePortalPath();
            else
                currentSeq = 0;    
        }
        else    
            currentSeq++;
    }

    public void checkPortalA(){

        Debug.Log(portalA.transform.rotation.y);
        if (portalA.transform.rotation.y >= -0.25  && portalA.transform.rotation.y <= -0.05)
        {
            Debug.Log("A");
            manivela1.GetComponent<Rotation>().enable(false);
            _portalA = true;
            createPortal();
        }
        else
            _portalA = false;    
        
    }

    public void checkPortalB(){

        Debug.Log(portalB.transform.rotation.y);
        if (portalB.transform.rotation.y >= -0.17  && portalB.transform.rotation.y <= 0.16)
        { 
            Debug.Log("B");
            manivela2.GetComponent<Rotation>().enable(false);
            _portalB = true;
            createPortal();
        }
        else
            _portalB = false;
    }

    private void createPortal(){
        if (_portalA && _portalB)
            portalPrefab.SetActive(true);
    }
}
