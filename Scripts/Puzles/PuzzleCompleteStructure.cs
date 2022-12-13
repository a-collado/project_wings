using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCompleteStructure : Puzzle
{
    [SerializeField] private GameObject[] enableOnComplete;
    [SerializeField] private GameObject[] disableOnComplete;


    // Start is called before the first frame update
    void Start() {
        initializeObjectPositions();
    }
    override public void initializeObjectPositions()
    {
        
    }

    void Update(){
        if (isCompleted){

        }
    }
    // Update is called once per frame
    public void checkPuzzle(GameObject structure)
    {
        if (structure.GetComponent<DropZone>().getBlock().name == "Orb"){
            isCompleted = true;
            foreach (GameObject o in enableOnComplete)
            {
                o.SetActive(true);
            }
            
            Debug.Log("Puzzle completed!");
            foreach (GameObject o in disableOnComplete)
            {
                o.SetActive(false);
            }
        }
    }
}
