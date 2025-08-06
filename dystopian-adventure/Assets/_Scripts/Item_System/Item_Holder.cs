using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Holder : MonoBehaviour
{
    public Item item;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) 
            && item != null)
        {
            item.UseItem();
        }
    }

    public Item getItem()
    {
        return item;
    }

    public void setItem(Item item)
    {
        this.item = item;
    }
}
