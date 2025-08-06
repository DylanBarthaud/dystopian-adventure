using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableObj : MonoBehaviour
{
    private bool beingPulled = false;
    PlayerState playerState;

    private void Start()
    {
        EventManager.Instance.onPlayerStateChange += OnPlayerStateChange; 
    }

    private void FixedUpdate()
    {
        if (beingPulled)
        {
            Pushed();
        }
    }

    public void Pushed()
    {
        Vector2 playerPos = GameManager.Instance.GetPlayer().transform.position;
        Vector2 newPos;

        if(playerPos.x > transform.position.x)
        {
            newPos = new Vector3(playerPos.x - 1.3f, transform.position.y);
        }
        else
        {
            newPos = new Vector3(playerPos.x + 1.3f, transform.position.y);
        }



        transform.position = newPos;
    }

    private void OnPlayerStateChange(PlayerState playerState)
    {
        if(playerState == PlayerState.None && beingPulled)
        {
            SwitchPulledState();
        }
    }

    public void SwitchPulledState()
    {
        beingPulled = !beingPulled; 
    }

    public bool CheckPulledState()
    {
        return beingPulled;
    }
}
