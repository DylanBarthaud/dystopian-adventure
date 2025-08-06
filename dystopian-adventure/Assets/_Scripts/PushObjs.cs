using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PushObjs : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float pushDistance;

    [Header("Components")]
    [SerializeField] private Transform pushDistanceCentre;

    private PlayerState playerState = PlayerState.None;

    private void Start()
    {
        EventManager.Instance.onPlayerStateChange += OnPlayerStateChange;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(playerState == PlayerState.None ||
                playerState == PlayerState.Pushing)
            {
                EventManager.Instance.OnPlayerStateChange(PlayerState.Pulling);
            }
            else
            {
                EventManager.Instance.OnPlayerStateChange(PlayerState.None);
            }
        } //swap PushPull state

        if (playerState == PlayerState.Pulling){
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
                if(playerState == PlayerState.Pulling && 
                    !pushedObj.CheckPulledState())
                {
                    pushedObj.SwitchPulledState();
                }
            }
        }
    }

    private void OnPlayerStateChange(PlayerState playerState)
    {
        this.playerState = playerState;
    }

    // Player Gizmos for designers 
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pushDistanceCentre.position, pushDistance);
    }
}
