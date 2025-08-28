using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class Interactor : MonoBehaviour
{
    [SerializeField] private float interactRange;
    [SerializeField] private GameObject interactPrompt;
    private GameObject displayedPrompt;
    private LayerMask interactLayer;

    private void Start()
    {
        interactLayer = LayerMask.GetMask("Interact_Layer");
    }

    private void Update()
    {
        if (CheckForInteractable())
        {
            // Display interaction prompt when player is inside of the interaction range
            Collider2D interactableCollider = Physics2D.OverlapCircle(gameObject.transform.position, interactRange, interactLayer);

            if (displayedPrompt == null)
            {
                displayedPrompt = Instantiate(interactPrompt, interactableCollider.transform.position, quaternion.identity);
                displayedPrompt.transform.parent = transform;
                displayedPrompt.name = "InteractPrompt";
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                interactableCollider.gameObject.GetComponent<Interactable>().Interact(this);
            }
        }
        else
        {
            // Hide interaction prompt when player is outside of the interaction range
            if (displayedPrompt != null)
            {
                Destroy(displayedPrompt.gameObject);
                displayedPrompt = null;
            }
        }
    }

    private bool CheckForInteractable()
    {
        return Physics2D.OverlapCircle(gameObject.transform.position, interactRange, interactLayer);
    }
}
