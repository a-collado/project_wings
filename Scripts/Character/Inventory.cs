using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    [SerializeField] private GameObject block; //Objeto que tiene que llevar a mano
    [SerializeField] private List<GameObject> items; //Item que se guarda en el bolsillo
    private FasTPS.CharacterMovement movement;
    // Start is called before the first frame update

    private void Awake() {
        movement = GetComponent<FasTPS.CharacterMovement>();
    }

    void Start()
    {
        items = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addBlock(GameObject gameObject) {
        movement.IsCarrying = true;
        block = gameObject;
    }

    public GameObject dropBlock() {
        movement.IsCarrying = false;
        GameObject tmp = block;
        block = null;
        return tmp;
    }

    public GameObject getBlock() {
        return block;
    }

    public void addItem(GameObject item) {
        this.items.Add(item);

    }

    public int dropItems(int numItems, string correctItemTag){
        int itemsDropped = 0;

        for(int i = items.Count - 1; i >= 0; i--)
        {
            if (items[i].CompareTag(correctItemTag) && itemsDropped <= numItems) {
                itemsDropped++;
                items.Remove(items[i]);
            }
        }
        return numItems - itemsDropped;

    }

    public List<GameObject> getItems()
    {
        return items;
    }

}
