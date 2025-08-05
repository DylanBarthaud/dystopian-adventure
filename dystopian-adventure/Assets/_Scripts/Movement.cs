using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    #region Player_Stats
    [Header("Player Movement Stats")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpHeight;

    [SerializeField] private float groundCheckDistance;
    #endregion

    [Header("Components")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Rigidbody2D rb2d;

    private LayerMask groundLayer;

    private float horizontalMove = 0f;
    private bool isFacingRight = true;

    private void Start()
    {
        groundLayer = LayerMask.GetMask("Ground"); 
    }

    private void Update()
    {
        #region Controls
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            print("here");
            rb2d.AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
        }

        horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
        #endregion

        Flip();
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3(horizontalMove * Time.deltaTime, 0f, 0f);
    }

    private void Flip()
    {
        if(isFacingRight && horizontalMove < 0f || !isFacingRight && horizontalMove > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckDistance, groundLayer);
    }

    // Player Gizmos for designers ??
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckDistance);
    }
}
