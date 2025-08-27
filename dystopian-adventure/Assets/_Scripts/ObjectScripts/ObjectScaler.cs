using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScaler : MonoBehaviour
{
    private Vector3 baseSize; 
    [SerializeField] private Vector3 scaleSize;

    private void Start()
    {
        baseSize = transform.localScale;
    }

    #region Setters
    public void SetToBaseSize()
    {
        transform.localScale = baseSize; 
    }

    public void SetToScaledSize()
    {
        transform.localScale = scaleSize;
    }

    #endregion
    #region Getters
    public Vector3 GetSize()
    {
        return transform.localScale;
    }

    public Vector3 GetBaseSize()
    {
        return baseSize;
    }

    public Vector3 GetScaledSize()
    {
        return scaleSize;
    }

    #endregion

    /// <summary>
    /// Sets object to given size if size given is within valid parameters
    /// </summary>
    /// <param name="size"> size to set object to </param>
    /// <returns> true if given size is within parameters, false if given size is outside of parameters </returns>
    public bool SetToSize(Vector3 size)
    {
        if(size.x > baseSize.x &&  size.y > baseSize.y && size.z > baseSize.z &&
           size.x < scaleSize.x && size.y < scaleSize.y && size.z < scaleSize.z)
        {
            transform.localScale = size; 
            return true;
        }

        return false;
    }
}
