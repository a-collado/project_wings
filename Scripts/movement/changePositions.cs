using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changePositions : MonoBehaviour
{
    [SerializeField] List<GameObject> waypoints;
    int currentWP=0;

    [SerializeField] float speed = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 destination = waypoints[currentWP].transform.position;
        //this.transform.LookAt(destination);
        this.transform.position = Vector3.MoveTowards(this.transform.position, destination, speed*Time.deltaTime);
        
        if (Vector3.Distance(this.transform.position, waypoints[currentWP].transform.position) < 3)
            currentWP++;

        if (currentWP >= waypoints.Count)
            currentWP = 0;

        
    }

}
