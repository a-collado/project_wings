using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsInOrder : MonoBehaviour, IInteractable
{
    [SerializeField] int maximum_items;
    List<int> positions;
    private bool isComplete;

    // Start is called before the first frame update
    public void Awake()
    {
        positions = new List<int>();
        isComplete = false;
    }

    // Update is called once per frame
    public void Update()
    {

    }

    public bool ItemToOrderEvent(List<int> positions_child){

        bool completed = false;

        if(positions.Count == 0)
        {
            if (positions_child.Contains(1))
            {
                positions.Add(1);
            }
        }
        else
        {
            int nextPosition = positions.Count+1;

            if (positions_child.Contains(nextPosition)){
                positions.Add(nextPosition);

                if(nextPosition == maximum_items)
                {
                    completed = true;
                    isComplete = true;
                }
            }
            else
            {
                positions.Clear();
            }
        }

        Debug.Log("Items in order: " + positions.Count);
        foreach (var x in positions)
        {
           
            Debug.Log(x.ToString());
            
        }
        Debug.Log("isCompleted: " + isComplete);
        return completed;
    }

    public AnimationsEnum Interact()
    {
        //Nothing implemented here
        return AnimationsEnum.NONE;
    }

    public void Power()
    {
        //Nothing implemented here
        throw new System.NotImplementedException();
    }

    public void activate(bool flag) { }

    public bool isActive() { return false; }

    public bool isCompleted() { return !isComplete; }

    public GameObject getGameObject()
    {
        return this.transform.gameObject;
    }

}
