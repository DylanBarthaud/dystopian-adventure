using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, Interactable
{
    [SerializeField] private int id; 

    public void Interact(Interactor interactor)
    {
        var item = (Item_Key)interactor.gameObject.GetComponent<Item_Holder>().getItem(); 

        if (item != null
            && item.GetId() == id) 
        {
            Destroy(gameObject);
        }
    }
}
