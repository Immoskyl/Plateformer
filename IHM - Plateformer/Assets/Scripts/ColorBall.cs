using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBall : MonoBehaviour
{
    [SerializeField]
    private float speedCoef = 0.3f;

    public float SpeedCoef
    {
        get => speedCoef;
        set => speedCoef = value;
    }

    private bool isJumping = false;

    public bool IsJumping
    {
        get => isJumping;
        set => isJumping = value;
    }

    [SerializeField]
    private float jumpHeight = 1;

    public float JumpHeight
    {
        get => jumpHeight;
        set => jumpHeight = value;
    }

    public double timeToJumpHeight = 0.5;

    public double TimeToJumpHeight
    {
        get => timeToJumpHeight;
        set => timeToJumpHeight = value;
    }

    private int jumpStep = 0;

    private float horizontalMovement;

    public float HorizontalMovement
    {
        get => horizontalMovement;
        set => horizontalMovement = value * speedCoef;
    }

    private float verticalMovement;

    public float VerticalMovement
    {
        get => verticalMovement;
        set => verticalMovement = value * speedCoef;
    }

    // Start is called before the first frame update
    void Start()
    {
        horizontalMovement = 0.0f;
        verticalMovement = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isJumping)
        {
            verticalMovement = Jump(jumpStep++);
            Debug.Log(verticalMovement);
        }
        Move();
    }
    
    //////////////////////////////////////////////////////
    ///                OBSERVER
    //////////////////////////////////////////////////////
    
    
    private void OnEnable()
    {
        MyInputManager.OnButtonAIsPushed += ButtonAIsPressed;
        MyInputManager.OnButtonBIsPushed += ButtonBIsPressed;
        MyInputManager.OnButtonXIsPushed += ButtonXIsPressed;
        MyInputManager.OnButtonYIsPushed += ButtonYIsPressed;
        MyInputManager.OnNoButtonIsPushed += NoButtonIsPressed;
        MyInputManager.OnMovementIsTriggered += MovementIsTriggered;
    }

    private void OnDisable()
    {
        MyInputManager.OnButtonAIsPushed -= ButtonAIsPressed;
        MyInputManager.OnButtonBIsPushed -= ButtonBIsPressed;
        MyInputManager.OnButtonXIsPushed -= ButtonXIsPressed;
        MyInputManager.OnButtonYIsPushed -= ButtonYIsPressed;
        MyInputManager.OnNoButtonIsPushed -= NoButtonIsPressed;
        MyInputManager.OnMovementIsTriggered -= MovementIsTriggered;
    }
    
        
    //////////////////////////////////////////////////////
    ///                INPUTS
    //////////////////////////////////////////////////////


    
    void ButtonAIsPressed()
    {
        if (!isJumping)
        {
            isJumping = true;
            jumpStep = 1;
            verticalMovement = 0.1f * Jump(jumpStep);
        }
    }

    void ButtonBIsPressed()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    void ButtonXIsPressed()
    {
        GetComponent<SpriteRenderer>().color = Color.magenta;
    }

    void ButtonYIsPressed()
    {
        GetComponent<SpriteRenderer>().color = Color.black;
    }

    void NoButtonIsPressed()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    void MovementIsTriggered(float horizontalAxis, float verticalAxis)
    {
        transform.Translate(horizontalAxis, verticalAxis + verticalMovement, 0);

    }

    //////////////////////////////////////////////////////
    ///                ACTIONS
    //////////////////////////////////////////////////////

    float Jump(int step)
    {
        return Parabola(step) - Parabola(step - 1);
    }

    float Parabola(int x)
    {
        var a = 0.5;
        var b = 6.5;
        var c = 0;
        var gravity = -1;
        return (float) Math.Max(gravity * a * x * x + b * x + c, gravity);
    }

    void Move()
    {
        //if (horizontalMovement != 0.0 || verticalMovement != 0.0)
        //    transform.Translate(horizontalMovement, verticalMovement, 0);
    }
}
