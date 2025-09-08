using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Key : Item
{
    [SerializeField] private int id;

    public int GetId()
    {
        return id;
    }
}
