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
    //Portal
    private bool _portalA, _portalB;
    public float portalAangle = 0;
    public float portalBangle = 0;


    void Start()
    {
        portalAangle = portalA.transform.rotation.eulerAngles.y;
        portalBangle = portalB.transform.rotation.eulerAngles.y;


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

      /*
        Debug.Log(portalA.transform.rotation.eulerAngles.y);
        Quaternion angleA = portalA.transform.rotation;
        angleA.eulerAngles = new Vector3(portalA.transform.rotation.eulerAngles.x, portalAangle, portalA.transform.rotation.eulerAngles.z);
        portalA.transform.rotation = angleA;

        Quaternion angleB = portalB.transform.rotation;
        angleB.eulerAngles = new Vector3(portalB.transform.rotation.eulerAngles.x, portalBangle, portalB.transform.rotation.eulerAngles.z);
        portalB.transform.rotation = angleB;
        */

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

        Debug.Log(portalA.transform.rotation.eulerAngles.y);
        
        if (portalA.transform.rotation.eulerAngles.y >= 607-360  && portalA.transform.rotation.eulerAngles.y <= 609 - 360)
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

        Debug.Log(portalB.transform.rotation.eulerAngles.y);
        
        if (portalB.transform.rotation.eulerAngles.y >= 424 - 360 && portalB.transform.rotation.eulerAngles.y <= 426 - 360)
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
