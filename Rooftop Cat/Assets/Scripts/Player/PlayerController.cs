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

    public int rayCount = 3; // number of raycasts
    public float rayLength = 1f; // length of raycast
    public float angle; // angle between raycasts
    public LayerMask groundLayerMask;


    public GroundCollisionController groundCollisionController;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCollisionController.SetIsGroundedDelegate += CheckIfPlayerCanJump;
    }

    public void Update()
    {
        MoveContinously();
        //CheckIfIsGrounded();
    }
    public void MoveContinously()
    {
        transform.position += new Vector3(moveSpeed, 0) * Time.deltaTime;
    }

    public bool CheckIfIsGrounded()
    {
        var currentLegth = rayLength;
        var newLength = currentLegth - 0.15f;
        for (int i = 0; i < rayCount; i++)
        {
            float rayAngle = angle * (i - (rayCount - 1) / 2f);

            // calculate angle for this raycast
            Vector2 direction = Quaternion.Euler(0, 0, rayAngle) * Vector2.down;
            if(i == 1)
            {
                rayLength = newLength;
            }
            else
            {
                rayLength = currentLegth;
            }
            // calculate direction for this raycast
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rayLength, groundLayerMask); // cast raycast

            if (hit.collider != null)
            {
                isGrounded = true;
                // handle raycast hit
                Debug.DrawLine(transform.position, hit.point, Color.green);
            }
            else
            {
                isGrounded = false;
                // handle raycast miss
                Debug.DrawRay(transform.position, direction * rayLength, Color.red);
            }
        }
        currentLegth = 0.0f;
        newLength = 0.0f;
        return isGrounded;
    }

    public void Jump()
    {
        if (isGrounded == true)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public void CheckIfPlayerCanJump(bool condition)
    {
        if (condition == true)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
