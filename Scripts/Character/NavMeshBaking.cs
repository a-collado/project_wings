using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class NavMeshBaking : MonoBehaviour
{
    // Start
    private NavMeshSurface navMesh;
    [SerializeField] private Transform playerTransform;
    
    void Start()
    {
        navMesh = this.gameObject.GetComponent<NavMeshSurface>();
        navMesh.BuildNavMesh();
    }

    void Update(){
        //navMesh.center = playerTransform.gameObject.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //navMesh.RemoveData();
        //navMesh.gameObject.transform.position = playerTransform.position;
        navMesh.center = playerTransform.localPosition;
        navMesh.UpdateNavMesh(navMesh.navMeshData);
    }
}
