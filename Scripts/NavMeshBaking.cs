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
    }

    void Update(){
        navMesh.gameObject.transform.position = playerTransform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        navMesh.BuildNavMesh();
    }
}
