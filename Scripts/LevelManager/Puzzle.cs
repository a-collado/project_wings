using UnityEngine;

public abstract class Puzzle : MonoBehaviour
{

    [SerializeField] protected bool isCompleted = false;
    [SerializeField] protected GameObject[] puzzleObjects;

    //Method to initialize stuff
    public abstract void initializeObjectPositions();

    //Method to check if puzzle is solved
    //Use this method when a part of the puzzle has been solved
    //(example: place and item)
    //public abstract void checkPuzzle();

    public void setInteractable(GameObject o, bool flag){
        if(flag){
            o.layer = LayerMask.NameToLayer("Interactuable");

        }else {
            o.layer = LayerMask.NameToLayer("Default");
        }
    }

    


}