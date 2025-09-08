using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeObjSizeStrip : MonoBehaviour
{
    [SerializeField] private Vector3 startPos, endPos;
    [SerializeField] private bool invert = false;
    [SerializeField] private Vector2 checkForObjBoxSize;
    [SerializeField] private LayerMask objLayer;

    private GameObject currentObj = null; 

    private void Update()
    {
        Collider2D startPosCollision = Physics2D.OverlapBox(startPos, checkForObjBoxSize, 0.0f, objLayer);
        Collider2D endPosCollision = Physics2D.OverlapBox(endPos, checkForObjBoxSize, 0.0f, objLayer);

        if (startPosCollision != null)
        {
            GameObject detectedObj = startPosCollision.gameObject;

            if (currentObj == null)
            {
                currentObj = detectedObj;
            }
        }

        if (endPosCollision != null)
        {
            GameObject detectedObj = endPosCollision.gameObject;

            if (currentObj == null)
            {
                currentObj = detectedObj;
            }
        }

        if (currentObj != null) 
        {
            if (startPos == endPos) 
            {
                Debug.LogError("startPos cannot equal endPos"); 
                return;
            }

            ObjectScaler objScaler = currentObj.GetComponentInParent<ObjectScaler>();

            Vector3 objScaledSize = objScaler.GetScaledSize();

            if (currentObj.transform.position.x < startPos.x)
            {
                if (invert)
                {
                    objScaler.SetToSize(objScaledSize);
                    return;
                }

                objScaler.SetToBaseSize();
                currentObj = null; 
                return;
            }

            if (currentObj.transform.position.x > endPos.x)
            {
                if (invert)
                {
                    objScaler.SetToBaseSize();
                    return;
                }

                objScaler.SetToSize(objScaledSize);
                currentObj = null;
                return; 
            }

            Vector3 difference = endPos - startPos;
            Vector3 objDifference = currentObj.transform.position - startPos;

            float t = Vector3.Dot(objDifference, difference) / difference.sqrMagnitude;

            t = Mathf.Clamp01(t);
            if (invert)
            {
                t = 1f - t;
            }
       
            Vector3 newObjSize = Vector3.Lerp(objScaler.GetBaseSize(), objScaledSize, t);

            objScaler.SetToSize(newObjSize);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(startPos, checkForObjBoxSize);
        Gizmos.DrawWireCube(endPos, checkForObjBoxSize);

        Gizmos.color = Color.red;
    }
}
