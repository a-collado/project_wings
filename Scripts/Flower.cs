using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{

    //TODO Aqui hay que crear un sistema de Object Pulling para no reventar el rendiminto del juego.

    [SerializeField]
    private float _lifeTime = 2.0f;

    private float _mark;
    private Vector3 _origSize;

    void Start()
    {
        _mark =  Time.time;
        _origSize = this.transform.localScale;
    }

    void Update()
    {
        float _elapsedTime = Time.time - _mark;
        if (_elapsedTime != 0)
        {
            float _percentTimeLeft = (_lifeTime - _elapsedTime) / _lifeTime;

            transform.localScale = new Vector3(_origSize.x * _percentTimeLeft, _origSize.y * _percentTimeLeft, _origSize.z * _percentTimeLeft);
            if (_elapsedTime > _lifeTime){
                Destroy(gameObject);
            }
        }
    }
}
