using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newJumpWoPhysics : MonoBehaviour
{
    /**
    * Number of Jumps possible in a row unlocked by the character
    */
    [SerializeField]
    private int numberOfJumps;
    
    /**
    * Jumps done in a row by the character
    */
    private int jumpsInARow;
    
    /**
     * Value of the character running in the left of right direction
     */
    [SerializeField]
    [Range(0.1f, 50)]
    private float acceleration;
    
    
    /**
     * Number of frames before the character reaches its baseSpeed from idle position
     */
    [SerializeField]
    [Range(1, 20)]
    private int baseInertia;
    
    /**
     * mass of the character
     */
    [SerializeField]
    [Range(1, 10)]
    private float mass;
    
    /**
     * Maximum vertical speed possible attributed to the character at any time
     */
    [SerializeField]
    [Range(0.1f, 30)]
    private float verticalMaxSpeed;
    
    /**
     * Maximum horizontal speed possible attributed to the character at any time
    */
    [SerializeField]
    [Range(0.1f, 30)]
    private float horizontalMaxSpeed;

    [SerializeField]
    [Range(0.1f, 20)]
    private float jumpStrenght;
    
    [SerializeField]
    [Range(1, 10)]
    private float jumpRound;
    
    [SerializeField]
    [Range(0.1f, 20)]
    private float gravity;
    
    
    private Vector2 maxSpeed;
    
    /**
     * Dict where all the forces determining the final speed of the characters
     */
    private Dictionary<int, Vector2> forceSummary;

    public int NumberOfJumps
    {
        get => numberOfJumps;
        set => numberOfJumps = value;
    }

    public int JumpsInARow
    {
        get => jumpsInARow;
        set => jumpsInARow = value;
    }


    public float VerticalMaxSpeed
    {
        get => verticalMaxSpeed;
        set => verticalMaxSpeed = value;
    }

    public float HorizontalMaxSpeed
    {
        get => horizontalMaxSpeed;
        set => horizontalMaxSpeed = value;
    }

    public float Mass
    {
        get => mass;
        set => mass = value;
    }

    public float JumpStrenght
    {
        get => jumpStrenght;
        set => jumpStrenght = value;
    }

    public float JumpRound
    {
        get => jumpRound;
        set => jumpRound = value;
    }

    public float Gravity
    {
        get => gravity;
        set => gravity = value;
    }
    
    public Vector2 MaxSpeed
    {
        get => maxSpeed;
        set => maxSpeed = value;
    }


    /**
     * Add the value to a force corresponding to a key
     */
    public void AddForce(Forces key, Vector2 value)
    {
        int dictKey = (int) key;

        if (forceSummary.ContainsKey(dictKey))
        {
            forceSummary[dictKey] += value;
        }
        else
        {
            forceSummary[dictKey] = value;
        }
    }

    /**
     * Remove totally the value of a force corresponding to a key
     */
    public void RemoveForce(Forces key)
    {
        int dictKey = (int) key;
        if (forceSummary.ContainsKey(dictKey))
        {
            forceSummary.Remove(dictKey);
        }
    }
    
    /**
     * Return the value of a force corresponding to a key
     */
    public Vector2 GetForce(Forces key)
    {
        int dictKey = (int) key;
        if (forceSummary.ContainsKey(dictKey))
        {
            return forceSummary[dictKey];
        }
        return Vector2.zero;
    }
    
    /**
     * 
     * Different forces to apply to the character
     * If you want the character to go leftwards, put some value to GoLeft, and equivalent for right.
     */
    public enum Forces
    {
        GoLeft = 1,
        GoRight = 2,
        Jumping = 3,
        Falling = 4
    }

    private Vector2 KeepVectorInBoundaries(Vector2 v, Vector2 boundaries)
    {
        var res = new Vector2();
        
        var x = Math.Max(v.x, -boundaries.x);
        res.x = Math.Min(x, boundaries.x);

        var y = Math.Max(v.y, -boundaries.y);
        res.y = Math.Min(y, boundaries.y);
        
        return res;
    }

    private Vector2 VectorRound(Vector2 v, int precision)
    {
        return new Vector2((float) Math.Round(v.x, precision), (float) Math.Round(v.y, precision));
    }

    /**
     * Init various parameters
     */
    private void Init()
    {
        MaxSpeed = new Vector2(HorizontalMaxSpeed / 10, VerticalMaxSpeed / 10);
        acceleration = acceleration / 10;
        
        forceSummary = new Dictionary<int, Vector2>();
        
        Fall();
    }
    
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("A") && JumpsInARow < NumberOfJumps)
            Jump();
        Run();
    }

    private void LateUpdate()
    {
        Vector2 frameMovement = CalcSpeed();
        transform.Translate(frameMovement.x, frameMovement.y, 0);
        DecayJump();
    }

    private Vector2 CalcSpeed()
    {
        var forceSum = Vector2.zero;
        foreach (KeyValuePair<int, Vector2> entry in forceSummary)
        {
            if (entry.Key == (int)Forces.GoLeft || entry.Key == (int)Forces.Falling)
                forceSum -= entry.Value;
            else
                forceSum += entry.Value;
        }

        var forceSpeed = forceSum * (1 / mass) * Time.deltaTime;
        var totalSpeed = VectorRound(KeepVectorInBoundaries(forceSpeed, maxSpeed), 3);
        return totalSpeed;
    }
    
    public void Jump()
    {
        JumpsInARow++;
        AddForce(Forces.Jumping, new Vector2(0, jumpStrenght*10));
    }

    public void Run()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        Forces direction;

        if (horizontalAxis < 0)
        {
            direction = Forces.GoLeft;
            if (GetForce(Forces.GoRight).x > 0)
                BackToZero(Forces.GoRight);
        }
        else if (horizontalAxis > 0)
        {
            direction = Forces.GoRight;
            if (GetForce(Forces.GoLeft).x > 0)
                BackToZero(Forces.GoLeft);
        }
        else {
            BackToZero(Forces.GoRight);
            BackToZero(Forces.GoLeft);
            return;
        }
        
        float forceToApply = CalculateHorizontalForce(horizontalAxis);
        AddForce(direction, new Vector2(forceToApply, 0));
    }
    
    private void BackToZero(Forces direction)
    {
        float currentForce = GetForce(direction).x;
        float forceToApply = currentForce / baseInertia;
        AddForce(direction, new Vector2(- forceToApply, 0));
    }

    private void DecayJump()
    {
        float jumpForce = GetForce(Forces.Jumping).y;
        if (jumpForce > 0)
        {
            float forceToApply = jumpStrenght / JumpRound;
            if (jumpForce - forceToApply < 0)
                forceToApply = jumpForce;
            AddForce(Forces.Jumping, new Vector2(0, - forceToApply));
        }
    }

    public float CalculateHorizontalForce(float horizontalAxis)
    {
        return acceleration * Math.Abs(horizontalAxis);
    }

    public void Fall()
    {
        AddForce(Forces.Falling, new Vector2(0, gravity * 2));
    }
}
