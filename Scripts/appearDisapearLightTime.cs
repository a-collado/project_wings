using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class appearDisapearLightTime : MonoBehaviour
{
    [SerializeField] float switchTime;
    [SerializeField] bool startActive;
    MeshCollider trigCollider;
    MeshRenderer trigMesh;
    Light luz;
    bool active;
    float time;

    // Start is called before the first frame update
    void Awake()
    {
        active = startActive;
        time = 0;
        luz = this.GetComponentInParent<Light>();
        trigCollider = this.GetComponent<MeshCollider>();
        trigMesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time >= switchTime)
        {
            time = 0;
            if (active)
            {
                active = false;
                luz.enabled = false;
                trigCollider.enabled = false;
                trigMesh.enabled = false;
            }
            else
            {
                active = true;
                luz.enabled = true;
                trigCollider.enabled = true;
                trigMesh.enabled = true;

            }

        }
        
    }
}
