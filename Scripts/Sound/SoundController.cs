using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void playSound(){
        source.Play();
    }


}
