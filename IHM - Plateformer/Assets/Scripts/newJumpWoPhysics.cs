using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newJumpWoPhysics : MonoBehaviour
{
    /**
     * Value of the character running in the left of right direction
     */
    [SerializeField]
    [Range(0.1f, 10)]
    private float baseSpeed;
    
    
    /**
     * Number of frames before the character reaches its baseSpeed from idle position
     */
    [SerializeField]
    [Range(1, 10)]
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
    [Range(0.1f, 20)]
    private float verticalMaxSpeed;
    
    /**
     * Maximum horizontal speed possible attributed to the character at any time
    */
    [SerializeField]
    [Range(0.1f, 20)]
    private float horizontalMaxSpeed;
    
    private Vector2 maxSpeed;
    private Vector2 lastFrameSpeed;
    
    /**
     * Dict where all the forces determining the final speed of the characters
     */
    private Dictionary<int, Vector2> forceSummary;

    public float Mass { get; set; }

    public float VerticalMaxSpeed {get; set;}

    public float HorizontalMaxSpeed {get; set;}

    public Vector2 MaxSpeed {get; set;}

    public Vector2 LastFrameSpeed {get; set;}

    public void AddForce(Forces key, Vector2 value)
    {
        int dictKey = (int) key;

        if (forceSummary.ContainsKey(dictKey))
        {
            Debug.Log("adding force to forceSummary: " + value + " Old Force =" + forceSummary[dictKey] + " and new force = " + (forceSummary[dictKey] + value));
            forceSummary[dictKey] += value;
        }
        else
        {
            forceSummary[dictKey] = value;
        }
    }

    public void RemoveForce(Forces key)
    {
        int dictKey = (int) key;
        if (forceSummary.ContainsKey(dictKey))
        {
            forceSummary.Remove(dictKey);
        }
    }

    public Vector2 GetForce(Forces key)
    {
        int dictKey = (int) key;
        if (forceSummary.ContainsKey(dictKey))
        {
            return forceSummary[dictKey];
        }
        return Vector2.zero;
    }
    
    public enum Forces
    {
        GoLeft = 1,
        GoRight = 2,
        Jumping = 3,
        Falling = 4
    }

    private Vector2 VectorMax(Vector2 v1, Vector2 v2)
    {
        var res = new Vector2();
        res.x = Math.Max(v1.x, v2.x);
        res.y = Math.Max(v1.y, v2.y);
        return res;
    }
    
    private void setLeftForceGood()
    {
        Vector2 leftForce = GetForce(Forces.GoLeft);
        leftForce.x = - leftForce.x;
        RemoveForce(Forces.GoLeft);
        AddForce(Forces.GoLeft, leftForce);
    }


    private void Init()
    {
        maxSpeed = new Vector2(HorizontalMaxSpeed, VerticalMaxSpeed);
        lastFrameSpeed = Vector2.zero;
        
        forceSummary = new Dictionary<int, Vector2>();
    }
    
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("A") == 1)
        {
            Jump();
        }
        Run();
    }

    private void LateUpdate()
    {
        Vector2 frameMovement = CalcSpeed();
        transform.Translate(frameMovement.x, frameMovement.y, 0);
        lastFrameSpeed = frameMovement;
    }

    private Vector2 CalcSpeed()
    {
        setLeftForceGood();

        Debug.Log("forcesSummary:" + forceSummary);
        
        var forceSum = Vector2.zero;
        foreach (KeyValuePair<int, Vector2> entry in forceSummary)
        {
            forceSum += entry.Value;
        }

        var thisFrameSpeed = forceSum * (1 / mass) * Time.deltaTime;
        var totalSpeed = lastFrameSpeed + thisFrameSpeed;
        totalSpeed = VectorMax(totalSpeed, maxSpeed);    
        
        lastFrameSpeed = totalSpeed;
        
        return totalSpeed;
    }
    
    public void Jump()
    {
        lastFrameSpeed.y = 30;
    }

    public void Run()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        Forces direction;

        if (horizontalAxis < 0)
        {
            Debug.Log("going left");
            direction = Forces.GoLeft;
            
            if (GetForce(Forces.GoRight).x > 0)
            {
                BackToZero(Forces.GoRight);
            }
        }
        else if (horizontalAxis > 0)
        {
            Debug.Log("going right");

            direction = Forces.GoRight;
            
            if (GetForce(Forces.GoLeft).x > 0)
            {
                BackToZero(Forces.GoLeft);
            }
        }
        else {
            Debug.Log("stopped");
            BackToZero(Forces.GoRight);
            BackToZero(Forces.GoLeft);
            return;
        }
        
        float forceToApply = CalculateHorizontalForce(horizontalAxis);
        AddForce(direction, new Vector2(forceToApply, 0));
    }
    
    private void BackToZero(Forces direction)
    {
        float forceToApply = baseSpeed / baseInertia;
        float currentForce = GetForce(direction).x;
        if (currentForce < forceToApply)
        {
            forceToApply = currentForce;
        }
        AddForce(direction, new Vector2(- forceToApply, 0));
    }
    
    
    public float CalculateHorizontalForce(float horizontalAxis)
    {
        float forceToApply;
        float potentialSpeed = baseSpeed * Math.Abs(horizontalAxis);
        Debug.Log("potentialSpeed: " + potentialSpeed);
        float relativeMaxSpeed = baseSpeed / baseInertia;
        Debug.Log("relativeMaxSpeed: " + relativeMaxSpeed);
        if (potentialSpeed > relativeMaxSpeed)
            forceToApply = relativeMaxSpeed;
        else
            forceToApply = potentialSpeed;

        Debug.Log("forceToApply: "+ forceToApply);
        return forceToApply;
    }
    
}
