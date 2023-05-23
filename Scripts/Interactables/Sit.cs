using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sit : MonoBehaviour,  IInteractable
{
    
    private GameObject _player; 
    private CharacterAnimation _animator;
    
    void Start()
    {
        _player = GameObject.FindGameObjectsWithTag("Player")[0];
        _animator = _player.GetComponent<CharacterAnimation>();
    }

    public AnimationsEnum Interact()
    {
        return AnimationsEnum.NONE;
    }

    public void Power()
    {
        throw new System.NotImplementedException();
    }

    public void activate(bool flag)
    {
        gameObject.SetActive(flag);
    }

    public bool isActive()
    {
        return gameObject.activeSelf;
    }

    public bool isCompleted()
    {
        return true;
    }

    public GameObject getGameObject()
    {
        return gameObject;
    }

    void IInteractable.Update()
    {
        
    }
}
