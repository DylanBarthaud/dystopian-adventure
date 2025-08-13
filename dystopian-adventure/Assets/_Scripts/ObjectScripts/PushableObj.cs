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
        Collider2D playerCollider = GameManager.Instance.GetPlayer().gameObject.GetComponent<Collider2D>();
        Vector3 newParentPos;

        Physics2D.SyncTransforms();
        Vector2 playerSize = playerCollider.bounds.size;
        Vector2 boxSize = GetComponent<Collider2D>().bounds.size;

        Vector3 parentPos =  transform.parent.position;

        if (playerCollider.transform.position.x > transform.position.x)
        {
            newParentPos = new Vector3(playerCollider.transform.position.x - ((playerSize.x / 2f) + (boxSize.x / 2f) + 0.1f), parentPos.y, parentPos.z);
        }
        else
        {
            newParentPos = new Vector3(playerCollider.transform.position.x + ((playerSize.x / 2f) + (boxSize.x / 2f) + 0.1f), parentPos.y, parentPos.z);
        }

        Vector3 newPos = new Vector3(playerCollider.transform.position.x + ((playerSize.x / 2f) + (boxSize.x / 2f) + 0.1f), transform.position.y, transform.position.z);

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
            transform.parent.position = newParentPos;
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
