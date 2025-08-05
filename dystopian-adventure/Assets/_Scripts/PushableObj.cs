using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableObj : MonoBehaviour
{
    private bool beingPulled = false;

    private void Update()
    {
        PushPullState playerPPState = GameManager.Instance.GetPlayer().GetComponent<PushObjs>().GetPlayerPPState(); 
        if (playerPPState == PushPullState.None ||
            playerPPState == PushPullState.Pushing)
        {
            if (beingPulled)
            {
                SwitchPulledState(); 
            }
        }
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

    public void SwitchPulledState()
    {
        beingPulled = !beingPulled; 
    }

    public bool CheckPulledState()
    {
        return beingPulled;
    }
}
