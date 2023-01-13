using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
    Usado como evento en las animaciones para dropear un item en cuanto se llame dentro de la animacion
*/
public class DropItem : MonoBehaviour
{
    [SerializeField] private Animator itemToDrop;
    

    private void dropItem() {
        itemToDrop.SetTrigger("drop");
    }
}
