using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlowerTrail : MonoBehaviour
{

    //TODO Aqui hay que crear un sistema de Object Pulling para no reventar el rendiminto del juego.

    [SerializeField] 
    private float _flowerSpacer = 1.0f;
    [SerializeField]
    private GameObject[] _flowerPrefabs;
    [SerializeField]
    private Transform flowerParent;
    private Vector3 _lastFlower;
    private NavMeshAgent player;
    private int flowerNumber;
    
    private void Start() {
        player = gameObject.GetComponent<NavMeshAgent>();
        _lastFlower = player.transform.position;
    }    
        
    void Update()
    {
      if(player != null && player.velocity != Vector3.zero) 
      {
        float distanceSinceLastFlower = Vector3.Distance(_lastFlower, this.transform.position);
        if (distanceSinceLastFlower >= _flowerSpacer){
            spawnFlower();
        }
        
      }
        
    }

    private void spawnFlower(){
        int _randomFlower = 0; //Esto todabia no es random;

        Vector3 from = this.transform.position;
        Vector3 to = new Vector3(this.transform.position.x, this.transform.position.y - (transform.localScale.y / 2.0f) + 
        0.1f, this.transform.position.z);
        Vector3 direction = to - from;

        RaycastHit hit;
        if (Physics.Raycast(from, direction, out hit)){
            GameObject flower = Instantiate(_flowerPrefabs[_randomFlower], hit.point, Quaternion.identity, flowerParent);
            _lastFlower = flower.transform.position;
        }
    }
}
