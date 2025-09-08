using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour, Interactable
{
    public abstract void Interact(Interactor interactor);
    public abstract void UseItem();
}
    