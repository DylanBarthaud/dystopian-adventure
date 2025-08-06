using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, Interactable
{
    public void Interact(Interactor interactor)
    {
        Item_Holder playerHolder = interactor.gameObject.GetComponent<Item_Holder>();
        if (playerHolder != null) { 
            playerHolder.setItem(this);
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Player item holder is null"); 
        }
    }

    public virtual void UseItem()
    {
        Debug.Log(this + "has null use");
    }
}
