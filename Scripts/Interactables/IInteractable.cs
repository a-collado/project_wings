using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public interface IInteractable
{
    void Update();
    AnimationsEnum Interact();

    public void Power();

    public void activate(bool flag);

    public bool isActive();

   public bool isCompleted();

   public GameObject getGameObject();


}
