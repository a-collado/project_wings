using UnityEngine;

public abstract class Puzzle : MonoBehaviour
{

    [SerializeField]
    protected bool isCompleted = false;

    //Method to initialize stuff
    public abstract void Start();

    //Method to check if puzzle is solved
    //Use this method when a part of the puzzle has been solved
    //(example: place and item)
    public abstract void checkPuzzle();

    


}