using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScaler : MonoBehaviour
{
    private Vector3 baseSize; 
    [SerializeField] private Vector3 scaledSize;

    private void Start()
    {
        baseSize = transform.localScale;
    }

    public void SetToBaseSize()
    {
        transform.localScale = baseSize; 
    }

    public void SetToScaledSize()
    {
        transform.localScale = scaledSize;
    }

    /// <summary>
    /// Sets object to given size if size given is within valid parameters
    /// </summary>
    /// <param name="size"> size to set object to </param>
    /// <returns> true if given size is within parameters, false if given size is outside of parameters </returns>
    public bool SetToSize(Vector3 size)
    {
        if(size.x > baseSize.x &&  size.y > baseSize.y && size.z > baseSize.z &&
           size.x < scaledSize.x && size.y < scaledSize.y && size.z < scaledSize.z)
        {
            transform.localScale = size; 
            return true;
        }

        return false;
    }
}
