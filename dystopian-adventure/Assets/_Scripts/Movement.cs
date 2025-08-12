using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    #region Player_Stats
    [Header("Player Movement Stats")]
    [SerializeField] private float baseMoveSpeed;
    [SerializeField] private float baseJumpHeight;

    [SerializeField] private float pullingSpeed; 

    [SerializeField] private Vector2 groundCheckDistance;
    #endregion

    [Header("Components")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Rigidbody2D rb2d;

    private LayerMask groundLayer;

    private float horizontalMove = 0f;
    private bool isFacingRight = true;

    private float moveSpeed;
    private float jumpHeight;

    private PlayerState playerState = PlayerState.None; 

    private void Start()
    {
        moveSpeed = baseMoveSpeed;
        jumpHeight = baseJumpHeight;
        groundLayer = LayerMask.GetMask("Ground");

        EventManager.Instance.onPlayerStateChange += OnPlayerStateChange; 
    }

    private void Update()
    {
        #region Controls
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            rb2d.AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
        }

        horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
        #endregion

        if(playerState == PlayerState.Pulling)
        {
            moveSpeed = pullingSpeed; 
        }
        else
        {
            moveSpeed = baseMoveSpeed; 
        }

        if(playerState != PlayerState.Pulling)
        {
            Flip();
        }
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
        return Physics2D.OverlapBox(groundCheck.position, groundCheckDistance, 0.0f, groundLayer);
    }

    private void OnPlayerStateChange(PlayerState playerState)
    {
        this.playerState = playerState;
    }

    // Player Gizmos for designers 
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(groundCheck.position, groundCheckDistance);
    }
}
