using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private int id; 

    public bool Open(int keyId)
    {
        if(keyId == id)
        {
            Destroy(gameObject);
            return true;
        }

        return false;
    }
}
