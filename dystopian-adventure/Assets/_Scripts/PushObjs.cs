using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PushPullState { None, Pulling, Pushing }

public class PushObjs : MonoBehaviour
{
    [Header("how far away the player can push an Obj from")]
    [SerializeField] private float pushDistance;

    [SerializeField] private Transform pushDistanceCentre;

    private PushPullState playerPPState = PushPullState.None; 
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(playerPPState == PushPullState.None ||
                playerPPState == PushPullState.Pushing)
            {
                playerPPState = PushPullState.Pulling; 
            }
            else
            {
                playerPPState = PushPullState.None;
            }
        } //swap PushPull state

        if (ObjInPushRange()){
            PushObj(); 
        }
    }

    private void PushObj()
    {
        Collider2D[] ObjColliders = Physics2D.OverlapCircleAll(pushDistanceCentre.position, pushDistance);

        foreach (Collider2D collider in ObjColliders)
        {
            if (collider.gameObject.CompareTag("PushableObject"))
            {
                PushableObj pushedObj = collider.gameObject.GetComponent<PushableObj>(); 
                if (playerPPState != PushPullState.Pulling && 
                    playerPPState != PushPullState.Pushing)
                {
                    pushedObj.Pushed();
                }
               
                if(playerPPState == PushPullState.Pulling && 
                    !pushedObj.CheckPulledState())
                {
                    pushedObj.SwitchPulledState();
                }
            }
        }
    }

    private bool ObjInPushRange()
    {
        return Physics2D.OverlapCircle(pushDistanceCentre.position, pushDistance); 
    }

    public PushPullState GetPlayerPPState()
    {
        return playerPPState;
    }

    // Player Gizmos for designers 
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(pushDistanceCentre.position, pushDistance);
    }
}
