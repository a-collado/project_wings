using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    GameObject block; //Objeto que tiene que llevar a mano
    GameObject[] item; //Item que se guarda en el bolsillo
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addBlock(GameObject gameObject) {
        block = gameObject;
    }

    public GameObject dropBlock() {
        GameObject tmp = block;
        block = null;
        return tmp;
    }

    public GameObject getBlock() {
        return block;
    }
}
