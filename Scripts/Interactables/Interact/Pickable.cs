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
            "Train" => AnimationsEnum.PICK_TWO_LOW,
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
                localPosition = new Vector3(-0.05f, 0.02f, 0.01f);
                localRotation = Quaternion.Euler(325.672882f, 193.663574f, 97.9599457f);
                localPos = true;
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
                localPosition = new Vector3(-0.00730000017f, 0.00579999993f, 0.035f);
                localRotation = Quaternion.Euler(6.552f, 108.014f, -120.524f);
                localPos = true;
                dropAnimation = AnimationsEnum.DROP_TWO_HIGH;
                break;
            case "Wood":
                leftHandRestPosition = new Vector3(-0.07177436f, -0.396999f, 0.40f);
                leftHandRestRotation = Quaternion.Euler(-9.639f, -120f, -89.795f);
                localPosition = new Vector3(-0.0604000017f, 0.0107000005f, -0.00240000011f);
                localRotation = Quaternion.Euler(50.6836205f, 163.451202f, 335.313965f);
                localPos = true;
                leftHand = true;
                rightHand = false;
                break;
            case "Gravel":
                dropAnimation = AnimationsEnum.DROP_ONE_MID;
                localPosition = new Vector3(-0.00970000029f, -0.0729999989f, -0.00079999998f);
                localRotation = Quaternion.Euler(16.4638348f, 276.044708f, 180f);
                //localPos = true;
                break;
            case "Train":
                /*leftHandRestPosition = new Vector3(0.0110006537f, -0.17500034f, 0.159000278f);
                leftHandRestRotation = Quaternion.Euler(333.82489f, 194.881485f, 65.0063629f);
                leftHand = true;
                rightHandRestPosition = new Vector3(-0.340999991f, -0.456f, 0.358999997f);
                rightHandRestRotation = Quaternion.Euler(18.3173771f, 131.787231f, 96.9498291f);
                rightHand = true;*/
                localPosition = new Vector3(-0.047f, 0.096f, -0.068f);
                localRotation = Quaternion.Euler(148.739f, 12.192f, 3.197f);
                localPos = true;
                dropAnimation = AnimationsEnum.DROP_TRAIN;
                break;
            case "Toy":
                localPosition = new Vector3(-0.034f, -0.009f, 0.21f);
                localRotation = Quaternion.Euler(-9.819f, -449.786f, -87.595f);
                dropAnimation = AnimationsEnum.DROP_TWO_HIGH;
                break;
            default:
                break;
        }
    }
}


