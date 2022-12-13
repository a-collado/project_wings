using UnityEngine;
using System.Collections;


public class PlaceItemsToMove : Puzzle{

    
    [SerializeField] private GameObject[] objectsToPlace;
    private int objectsPlaced;
    [SerializeField] private GameObject[] objectsToMove;
    


    
    public void Start() {
        //Initialize Variables
        objectsPlaced = 0;

        //Make sure that objectsToPickUp are visible from the start
        foreach (GameObject obj in objectsToPlace)
        {
            obj.SetActive(true);
        }
        foreach (GameObject obj in objectsToMove)
        {
            obj.SetActive(true);
        }

    }

    public override void initializeObjectPositions()
    {
        throw new System.NotImplementedException();
    }

    public void checkPuzzle() {
        objectsPlaced++;

        if (objectsPlaced == objectsToPlace.Length){
            isCompleted = true;
        }
    }
 
}