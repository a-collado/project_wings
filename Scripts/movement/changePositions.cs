using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changePositions : MonoBehaviour
{
    [SerializeField] List<GameObject> waypoints;
    int currentWP=0;

    [SerializeField] float speed = 0;
    [SerializeField] bool repeat = true;

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 destination = waypoints[currentWP].transform.position;
        
        this.transform.position = Vector3.MoveTowards(this.transform.position, destination, speed*Time.deltaTime);
        
        if (Vector3.Distance(this.transform.position, waypoints[currentWP].transform.position) < 3)
        if (!repeat && currentWP < waypoints.Count - 1 || repeat)
        {
            currentWP++;
        }    

        if (repeat && currentWP >= waypoints.Count)
            currentWP = 0;

        
    }

}
