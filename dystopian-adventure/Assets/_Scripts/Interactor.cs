using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float interactRange;
    private LayerMask interactLayer;

    private void Start()
    {
        interactLayer = LayerMask.GetMask("Interact_Layer");
    }

    private void Update()
    {
        if (CheckForInteractable() && Input.GetKeyDown(KeyCode.E))
        {
            Collider2D interactableCollider = Physics2D.OverlapCircle(gameObject.transform.position, interactRange, interactLayer);
            interactableCollider.gameObject.GetComponent<Interactable>().Interact(this);
        }
    }

    private bool CheckForInteractable()
    {
        return Physics2D.OverlapCircle(gameObject.transform.position, interactRange, interactLayer);
    }
}
