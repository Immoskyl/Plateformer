using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newJump : MonoBehaviour
{
    [SerializeField]
    [Range(1, 20)] 
    private float jumpVelocity;

    public float JumpVelocity
    {
        get => jumpVelocity;
        set => jumpVelocity = value;
    }

    [SerializeField]
    private float fallMultiplier = 2.5f;

    public float FallMultiplier
    {
        get => fallMultiplier;
        set => fallMultiplier = value;
    }

    [SerializeField]
    private float lowJumpMultiplier = 2f;

    public float LowJumpMultiplier
    {
        get => lowJumpMultiplier;
        set => lowJumpMultiplier = value;
    }

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("A"))
        {
            Debug.Log("VAR");
        }
        
        if (Input.GetButtonDown("A"))
        {
            rb.AddForce(Vector2.up * jumpVelocity);
        }

        if (rb.velocity.y < 0) {                                                                  //character is falling
            rb.AddForce(Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime);
        } else if (rb.velocity.y > 0 && Input.GetAxis("A") != 1) {                             // jump button is pressed
            rb.AddForce(Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime);
        }    
    }
    
    void Jump()
    {
        if (Input.GetAxis("A") == 1)
        {
            rb.AddForce(Vector2.up * jumpVelocity);
        }
    }


    void BetterJump()
    {
        if (Input.GetAxis("A") == 1)
        {
            Debug.Log("VAR");
        }
        if (rb.velocity.y < 0) //character is falling
        {
            rb.AddForce(Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime);
        } else if (rb.velocity.y > 0 && Input.GetAxis("A") != 1) // jump button is pressed
        {
            rb.AddForce(Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime);
        }
    }
}