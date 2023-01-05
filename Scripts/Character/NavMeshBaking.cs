using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class NavMeshBaking : MonoBehaviour
{
    // Start
    private NavMeshSurface navMesh;
    [SerializeField] private Transform playerTransform;
    
    void Awake()
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
        //gameObject.transform.localPosition = playerTransform.localPosition;
        navMesh.center = playerTransform.localPosition;
        gameObject.transform.localPosition = playerTransform.localPosition;
        Debug.Log("[NavMeshBaking]: navMesh.center = " + navMesh.center +
        " || navMeshObject localPos = " + gameObject.transform.localPosition + 
        " || navMeshObject pos = " + gameObject.transform.position + 
        " || player.localPos = " + playerTransform.localPosition  + 
        " || player.pos = " + playerTransform.position);
        navMesh.UpdateNavMesh(navMesh.navMeshData);
    }
}
