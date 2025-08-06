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

        Vector2 boxSize = GetComponent<Collider2D>().bounds.size;
        Collider2D[] hits = Physics2D.OverlapBoxAll(newPos, boxSize, 0f, LayerMask.GetMask("Ground"));

        bool isBlocked = false;
        foreach (var hit in hits)
        {
            if (hit != GetComponent<Collider2D>())
            {
                isBlocked = true;
                break;
            }
        }

        if (!isBlocked)
        {
            transform.position = newPos;
        }
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
