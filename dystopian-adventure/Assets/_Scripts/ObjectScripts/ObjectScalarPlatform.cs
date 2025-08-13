using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScalarPlatform : MonoBehaviour
{
    [SerializeField] private Vector2 CheckBoxSize; 
    [SerializeField] private LayerMask ObjectLayer;

    private void Update()
    {
        Collider2D objCollieder = Physics2D.OverlapBox(transform.position, CheckBoxSize, 0.0f, ObjectLayer); 

        if (objCollieder != null)
        {
            print(objCollieder.name);
            objCollieder.gameObject.GetComponentInParent<ObjectScaler>().SetToScaledSize(); 
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, CheckBoxSize);
    }
}
