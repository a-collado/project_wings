using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pickable : MonoBehaviour, IInteractable
{

    //public UnityEvent interacted;

    private Inventory _playerInventory;
    private System.Diagnostics.Stopwatch _stopWatch;

    [HideInInspector]
    public Vector3 leftHandRestPosition;
    [HideInInspector]
    public Quaternion leftHandRestRotation;
    [HideInInspector]
    public bool leftHand = false;
    
    [HideInInspector]
    public Vector3 rightHandRestPosition;
    [HideInInspector]
    public Quaternion rightHandRestRotation;
    [HideInInspector]
    public bool rightHand = false;
    
    [HideInInspector]
    public Vector3 localPosition;
    [HideInInspector]
    public Quaternion localRotation;
    [HideInInspector]
    public bool localPos = false;

    [HideInInspector] public AnimationsEnum dropAnimation;
    

    void Awake() {
        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
        _playerInventory = player.GetComponent<Inventory>();
        setHands();
        
        _stopWatch = new System.Diagnostics.Stopwatch();
        _stopWatch.Start();
    }

    public void Update() {
        
    }

    public AnimationsEnum Interact()
    {
        double time = _stopWatch.Elapsed.TotalMilliseconds/1000;
        if(time > 0.2)
        {
            _stopWatch.Restart();
            AnimationsEnum animation = pick();
            return animation;
        }
        _stopWatch.Restart();
        return AnimationsEnum.NONE;
    }

    public void Power()
    {
        throw new System.NotImplementedException();
    }

    public AnimationsEnum pick(){
        
        if (_playerInventory.getBlock() != null) return AnimationsEnum.NONE;

        AnimationsEnum animation = transform.tag switch
        {
            "Torch" => AnimationsEnum.GRAB_TORCH,
            "Orb" => AnimationsEnum.PICK_TWO_LOW,
            "Toy" => AnimationsEnum.PICK_HIGH_TWO,
            _ => AnimationsEnum.GRAB_TORCH
        };

        _playerInventory.addBlock(gameObject);
        return animation;
    }
    

    public void activate(bool flag){
       
        this.enabled = flag;
        this.gameObject.layer = flag ? 3 : 0;
    }

     public bool isActive() {
        return this.enabled;
    }

    public bool isCompleted()
    {
        return isActive();
    }

    public GameObject getGameObject()
    {
        return this.transform.gameObject;
    }

    private void setHands()
    {
        switch (transform.tag)
        {
            case "Torch": 
                leftHandRestPosition = new Vector3(-0.07177436f, -0.396999f, 0.40f);
                leftHandRestRotation = Quaternion.Euler(-9.639f, -120f, -89.795f);
                leftHand = true;
                rightHand = false;
                dropAnimation = AnimationsEnum.DROP_ONE_MID;
                break;
            case "Orb":
                leftHandRestPosition = new Vector3(0.0110006537f, -0.17500034f, 0.159000278f);
                leftHandRestRotation = Quaternion.Euler(-9.35f, -104.536f, -92.361f);
                leftHand = true;
                rightHandRestPosition = new Vector3(-0.284199923f, -0.122600317f, 0.235597655f);
                rightHandRestRotation = Quaternion.Euler(19.105f, 123.288f, -265.782f);
                rightHand = true;
                localPosition = new Vector3(-0.00730000017f, 0.00579999993f, 0.0241999999f);
                localRotation = Quaternion.Euler(6.552f, 108.014f, -120.524f);
                localPos = true;
                dropAnimation = AnimationsEnum.DROP_TWO_HIGH;
                break;
            default:
                break;
        }
    }
}


