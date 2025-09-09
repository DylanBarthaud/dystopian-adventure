using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum CurrentObjStripStatus { none, pastEnd, pastStart, inStrip }

public class ChangeObjSizeStrip : MonoBehaviour
{
    [SerializeField] private Vector3 startPos, endPos;
    [SerializeField] private bool invert = false;
    [SerializeField] private Vector2 checkForObjBoxSize;
    [SerializeField] private LayerMask objLayer;

    public GameObject currentObj = null;

    private void Update()
    {
        Collider2D startPosCollision = Physics2D.OverlapBox(startPos, checkForObjBoxSize, 0.0f, objLayer);
        Collider2D endPosCollision = Physics2D.OverlapBox(endPos, checkForObjBoxSize, 0.0f, objLayer);

        if (startPosCollision != null)
        {
            GameObject detectedObj = startPosCollision.gameObject;

            if (currentObj == null)
            {
                if (!invert && 
                    detectedObj.transform.parent.localScale == 
                    detectedObj.transform.GetComponentInParent<ObjectScaler>().GetScaledSize())
                {
                    return; 
                }

                if (invert && 
                    detectedObj.transform.parent.localScale == 
                    detectedObj.transform.GetComponentInParent<ObjectScaler>().GetBaseSize())
                {
                    return; 
                }

                currentObj = detectedObj;
            }
        }

        if (endPosCollision != null)
        {
            GameObject detectedObj = endPosCollision.gameObject;

            if (currentObj == null)
            {
                if (invert &&
                    detectedObj.transform.parent.localScale ==
                    detectedObj.transform.GetComponentInParent<ObjectScaler>().GetScaledSize())
                {
                    return;
                }

                if (!invert &&
                    detectedObj.transform.parent.localScale ==
                    detectedObj.transform.GetComponentInParent<ObjectScaler>().GetBaseSize())
                {
                    return;
                }
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

            CurrentObjStripStatus objStripStat = CheckObjInStrip();
            switch (objStripStat)
            {
                case CurrentObjStripStatus.none:
                    Debug.LogWarning("no obj strip status");
                    break;
                case CurrentObjStripStatus.inStrip:
                    break;
                case CurrentObjStripStatus.pastStart:
                    objScaler.SetToBaseSize();
                    currentObj = null;
                    break;
                case CurrentObjStripStatus.pastEnd:
                    objScaler.SetToScaledSize();
                    currentObj = null;
                    break;
            }
        }
    }

    private CurrentObjStripStatus CheckObjInStrip()
    {
        float objXPos = currentObj.transform.position.x;

        if (objXPos > endPos.x)
        {
            return !invert ? CurrentObjStripStatus.pastEnd :
                             CurrentObjStripStatus.pastStart;
        }

        if (objXPos < startPos.x)
        {
            return !invert ? CurrentObjStripStatus.pastStart:
                             CurrentObjStripStatus.pastEnd;
        }

        return CurrentObjStripStatus.inStrip;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(startPos, checkForObjBoxSize);
        Gizmos.DrawWireCube(endPos, checkForObjBoxSize);
    }
}
