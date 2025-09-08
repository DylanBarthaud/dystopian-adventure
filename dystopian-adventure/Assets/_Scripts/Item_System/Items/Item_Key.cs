using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Key : Item
{
    [SerializeField] private int id;
    [SerializeField] private Vector2 useKeySize; 

    private bool followPlayer;

    public int GetId()
    {
        return id;
    }

    public override void Interact(Interactor interactor)
    {
        Item_Holder playerHolder = interactor.gameObject.GetComponent<Item_Holder>();
        if (playerHolder != null)
        {
            playerHolder.setItem(this);
            followPlayer = true;
        }
        else
        {
            Debug.LogWarning("Player item holder is null");
        }
    }

    public override void UseItem()
    {
        Collider2D hit = Physics2D.OverlapBox(transform.position, useKeySize, 0f, LayerMask.GetMask("Keyable"));

        if (hit != null) 
        {
            if (hit.GetComponent<Door>().Open(id))
            {
                Destroy(transform.parent.gameObject);
            } 
        }
    }

    private void Update()
    {
        if (followPlayer)
        {
            Collider2D playerCollider = GameManager.Instance.GetPlayer().gameObject.GetComponent<Collider2D>();
            Vector3 newParentPos;

            Physics2D.SyncTransforms();
            Vector2 playerSize = playerCollider.bounds.size;
            Vector2 boxSize = GetComponent<Collider2D>().bounds.size;

            Vector3 parentPos = transform.parent.position;

            Movement playerMovementScript = GameManager.Instance.GetPlayer().gameObject.GetComponent<Movement>();

            if (!playerMovementScript.GetIsFacingRight())
            {
                newParentPos = new Vector3(playerCollider.transform.position.x - ((playerSize.x / 2f) + (boxSize.x / 2f) + 0.01f), playerCollider.transform.position.y, parentPos.z);
            }
            else
            {
                newParentPos = new Vector3(playerCollider.transform.position.x + ((playerSize.x / 2f) + (boxSize.x / 2f) + 0.01f), playerCollider.transform.position.y, parentPos.z);
            }

            transform.parent.position = newParentPos;

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                UseItem(); 
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                followPlayer = false; 

                Item_Holder playerHolder = playerCollider.gameObject.GetComponent<Item_Holder>();
                playerHolder.setItem(null);

                transform.parent.position = playerCollider.transform.position;
            }
        }
    }
}
