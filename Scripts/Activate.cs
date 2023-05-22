using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate : MonoBehaviour
{

    public GameObject toActivate;

    public void activate()
    {
        toActivate.SetActive(true);
    }
}
