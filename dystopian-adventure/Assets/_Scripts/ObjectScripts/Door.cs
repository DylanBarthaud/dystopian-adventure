using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private int id; 

    public void Open(int keyId)
    {
        if(keyId == id)
        {
            Destroy(gameObject);
        }
    }
}
