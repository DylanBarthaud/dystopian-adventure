using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, Interactable
{
    [SerializeField] private int id; 

    public void Interact(Interactor interactor)
    {
        Item_Key keyItem = interactor.gameObject.GetComponent<Item_Holder>().getItem().gameObject.GetComponent<Item_Key>();
        if (keyItem != null
            && keyItem.GetId() == id) 
        {
            Destroy(gameObject);
        }
    }
}
