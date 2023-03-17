using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectorVision : MonoBehaviour
{
    [SerializeField] private GameObject vision;
    [SerializeField] private float timeToDeactivate = 5f;
    private AudioSource audio;
   
    void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        //Activate gameObject during x seconds and then deactivate it

        //Activate gameObject
        if(audio != null) { audio.Play(); }
        vision.SetActive(true);
        GetComponent<Animator>().SetBool("pressed", true);

        //Deactivate gameObject after x seconds
        Invoke("DeactivateVision", timeToDeactivate);
    }

    void DeactivateVision()
    {
        vision.SetActive(false);
    }

    void OnTriggerExit(Collider other)
    {
        GetComponent<Animator>().SetBool("pressed", false);
    }
}
