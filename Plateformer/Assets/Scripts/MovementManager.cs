using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;


public class MovementManager : MonoBehaviour
{
    
    /**
    * Allows a character to Move and Jump according to settable parameters by summing different forces (on both X and Y)
    * This way you can define properly the movements and feelings you cant to give to your character.
    **/
    
    
    //////////////////////////////////////////////////////
    ///                ATTRIBUTES
    //////////////////////////////////////////////////////

    /**
    * Number of Jumps possible in a row unlocked by the character
    */
    [SerializeField]
    private int numberOfJumps;
    
    /**
    * Jumps done in a row by the character
    */
    [SerializeField]
    private int jumpsInARow;

    /**
    * Jumps after touched a wall
    */
    [SerializeField]
    private int wallJumps;

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
    private float jumpStrength;
    
    [SerializeField]
    [Range(1, 10)]
    private float jumpRound;
    
    [SerializeField]
    [Range(0.1f, 20)]
    private float gravity;

    [SerializeField]
    private GameObject checkpoint;
    
    private Vector2 checkpointPos;
    
    private Vector2 maxSpeed;


    //AUDIO ASSETS
    [SerializeField]
    public AudioSource firstJumpSound;
    
    [SerializeField]
    public AudioSource secondJumpSound;
    
    [SerializeField]
    public AudioSource thirdJumpSound;
    
    [SerializeField]
    public AudioSource fourthJumpSound;
    
    [SerializeField]
    public AudioSource deathSound;


    /**
     * Dict where all the forces determining the final speed of the characters
     */
    private Dictionary<int, Vector2> forceSummary;
    
    
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

    //////////////////////////////////////////////////////
    ///                GETTERS & SETTERS
    //////////////////////////////////////////////////////

    
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

    public float JumpStrength
    {
        get => jumpStrength;
        set => jumpStrength = value;
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

    public Vector2 CheckpointPos
    {
        get => checkpointPos;
        set => checkpointPos = value;
    }

    //////////////////////////////////////////////////////
    ///                UTILS
    //////////////////////////////////////////////////////

    
    /**
     * Add the value to a force corresponding to a key
     */
    public void AddForce(Forces key, Vector2 value)
    {
        int dictKey = (int) key;

        if (forceSummary.ContainsKey(dictKey))
            forceSummary[dictKey] += value;
        else
            forceSummary[dictKey] = value;
    }

    
    /**
     * Remove totally the value of a force corresponding to a key
     */
    public void RemoveForce(Forces key)
    {
        int dictKey = (int) key;
        if (forceSummary.ContainsKey(dictKey))
            forceSummary.Remove(dictKey);
    }
    
    
    /**
     * Return the value of a force corresponding to a key
     */
    public Vector2 GetForce(Forces key)
    {
        int dictKey = (int) key;
        if (forceSummary.ContainsKey(dictKey))
            return forceSummary[dictKey];
        return Vector2.zero;
    }

    /**
     * Assure that a Vector2 values do not go above boundaries values or below -boundaries values
     */
    private Vector2 KeepVectorInBoundaries(Vector2 v, Vector2 boundaries)
    {
        var res = new Vector2();
        
        var x = Math.Max(v.x, -boundaries.x);
        res.x = Math.Min(x, boundaries.x);

        var y = Math.Max(v.y, -boundaries.y);
        res.y = Math.Min(y, boundaries.y);
        
        return res;
    }
    
    /**
     * Round values of a Vector2 to a precision
     */
    private Vector2 VectorRound(Vector2 v, int precision)
    {
        return new Vector2((float) Math.Round(v.x, precision), (float) Math.Round(v.y, precision));
    }

    //////////////////////////////////////////////////////
    ///                INIT & GAME LOOP 
    //////////////////////////////////////////////////////

    void Start()
    {
        Init();
    }
    
    
    /**
    * Initialize various parameters
    */
    private void Init()
    {
        MaxSpeed = new Vector2(HorizontalMaxSpeed / 10, VerticalMaxSpeed / 10);
        acceleration = acceleration / 10;
        
        forceSummary = new Dictionary<int, Vector2>();

        CheckpointPos = checkpoint.transform.position;
        
        Fall();
    }
    
    public void MovePlayer() 
    {
        if (Input.GetKeyDown("space") && JumpsInARow < NumberOfJumps)
            Jump();
        Move();
    }

    
    //////////////////////////////////////////////////////
    ///                MAIN ACTIONS
    //////////////////////////////////////////////////////

    /**
     * Make the character jump
     * Apply a upward force to the character, decaying over time
     */
    public void Jump()
    {
        JumpsInARow++;
        PlayJumpSound();
        AddForce(Forces.Jumping, new Vector2(0, jumpStrength*10));
    }

    public void ResetJumps()
    {
        JumpsInARow = 0;
    }

    public int Jump_On_Wall(bool booleanExpr)
    {
        return booleanExpr ? wallJumps : 0;
    }

    public void PlayJumpSound()
    {
        switch (JumpsInARow)
        {
            case 1:
                firstJumpSound.Play();
                break;
            case 2:
                secondJumpSound.Play();
                break;
            case 3:
                thirdJumpSound.Play();
                break;
            case 4:
                fourthJumpSound.Play();
                break;
            default:
                break;
        }
    }

    /**
     * Make the character fall
     * Apply a downward force similar to gravity
     */
    public void Fall()
    {
        AddForce(Forces.Falling, new Vector2(0, gravity * 2));
    }

    /**
     * Transforms controller horizontal inputs to character movements
     */
    public void Move()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        Forces direction;

        if (horizontalAxis < 0)
        {
            direction = Forces.GoLeft;
            if (GetForce(Forces.GoRight).x > 0)
                DecayMovement(Forces.GoRight);
        }
        else if (horizontalAxis > 0)
        {
            direction = Forces.GoRight;
            if (GetForce(Forces.GoLeft).x > 0)
                DecayMovement(Forces.GoLeft);
        }
        else {
            DecayMovement(Forces.GoRight);
            DecayMovement(Forces.GoLeft);
            return;
        }
        
        float forceToApply = CalculateHorizontalForce(horizontalAxis);
        AddForce(direction, new Vector2(forceToApply, 0));
    }

    public void Die()
    {
        transform.localPosition = checkpointPos;
        //GetComponent<Transform>().localPosition = checkpointPos;
        PlayDeathSound();
        GetComponent<CameraShake>().TriggerShake();
    }

    public void PlayDeathSound()
    {
        deathSound.Play();
    }
    
    //////////////////////////////////////////////////////
    ///                CUSTOM PHYSICS
    //////////////////////////////////////////////////////

    /**
     * Computes the sum of all the forces applied on the character, and returns the resulting speed.
     * Takes care of all directions in 2D, plus deals with the mass of the character
     */
    public Vector2 CalcSpeed()
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

    /**
     * Create a force out of an horizontal input axis
     */
    private float CalculateHorizontalForce(float horizontalAxis)
    {
        return acceleration * Math.Abs(horizontalAxis);
    }
    
    /**
     * Simulate an end of movement inertia by decaying the movement force over time
     */
    private void DecayMovement(Forces direction)
    {
        //quickfix
        AddForce(direction, new Vector2(- GetForce(direction).x, 0));
        return;
        /*
        float currentForce = GetForce(direction).x;
        float forceToApply = currentForce / baseInertia;
        AddForce(direction, new Vector2(- forceToApply, 0));
        */
    }

    /**
    * Simulate a jump inertia by decaying the movement force over time
    */
    public void DecayJump()
    {
        float jumpForce = GetForce(Forces.Jumping).y;
        if (jumpForce > 0)
        {
            float forceToApply = jumpStrength / JumpRound;
            if (jumpForce - forceToApply < 0)
                forceToApply = jumpForce;
            AddForce(Forces.Jumping, new Vector2(0, - forceToApply));
        }
    }

    
    //////////////////////////////////////////////////////
    ///                JSON SERIALIZATION
    //////////////////////////////////////////////////////
    
    /**
     * Save the parameters of this script to a JSON file
     */
    public void SaveToJSON(string path)
    {
        string lines = JsonUtility.ToJson(this, true);
        System.IO.File.WriteAllText(path, lines);
    }
    
    /**
     * Load the parameters of this script from a JSON file
     */
    public static MovementManager CreateFromJSON(string path)
    {
        return JsonUtility.FromJson<MovementManager>(System.IO.File.ReadAllText(path));
    }
    
}
