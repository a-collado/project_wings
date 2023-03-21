using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class appearDisapearLightTime : MonoBehaviour
{
    [SerializeField] float timeActive;
    [SerializeField] float timeInactive;
    [SerializeField] bool startActive;
    [SerializeField] float startAt;
    MeshCollider trigCollider;
    MeshRenderer trigMesh;
    Light luz;
    bool active;
    float time;

    // Start is called before the first frame update
    void Awake()
    {
        active = startActive;
        time = startAt;
        luz = this.GetComponentInParent<Light>();
        trigCollider = this.GetComponent<MeshCollider>();
        trigMesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(active){
            if(time >= timeActive){
                active = false;
                luz.enabled = false;
                trigCollider.enabled = false;
                trigMesh.enabled = false;
                time = 0;
            }
        }else{
            if(time>=timeInactive){
                active = true;
                luz.enabled = true;
                trigCollider.enabled = true;
                trigMesh.enabled = true;
                time=0;
            }
        }
        
        
    }
}
