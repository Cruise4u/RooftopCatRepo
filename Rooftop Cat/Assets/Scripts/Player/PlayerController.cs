using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;         // the rigidbody component of the player
    private bool isGrounded;        // whether the player is currently touching the ground
    public float jumpForce = 5f;    // the force with which the player jumps
    public float moveSpeed = 5f;    // the speed at which the player moves left and right
    public float jumpCooldown = 1f;
    public LayerMask groundLayerMask;

    public void MoveContinously()
    {
        transform.position += new Vector3(moveSpeed, 0) * Time.deltaTime;
    }
    public bool CheckIfIsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.8f, groundLayerMask);
        if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            if (hit.collider != null)
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
        }
        else
        {
            isGrounded = false;
        }
        return isGrounded;
    }
    public void Jump()
    {
        if (isGrounded == true)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        CheckIfIsGrounded();
        MoveContinously();
    }


}
