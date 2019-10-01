using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyInputManager : MonoBehaviour
{
    
    
    public delegate void ButtonAIsPushed();
    public static event ButtonAIsPushed OnButtonAIsPushed;
    
    public delegate void ButtonBIsPushed();
    public static event ButtonBIsPushed OnButtonBIsPushed;
    
    public delegate void ButtonXIsPushed();
    public static event ButtonXIsPushed OnButtonXIsPushed;
    
    public delegate void ButtonYIsPushed();
    public static event ButtonYIsPushed OnButtonYIsPushed;
    
    public delegate void NoButtonIsPushed();
    public static event NoButtonIsPushed OnNoButtonIsPushed;
    
    public delegate void MovementIsTriggered(float horizontalAxis, float verticalAxis);
    public static event MovementIsTriggered OnMovementIsTriggered;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("A") == 1)
        {
            if (OnButtonAIsPushed != null)
                OnButtonAIsPushed();
        }
        else if (Input.GetAxis("B") == 1)
        {
            if (OnButtonBIsPushed != null)
                OnButtonBIsPushed();
        }

        else if (Input.GetAxis("X") == 1)
        {
            if (OnButtonXIsPushed != null)
                OnButtonXIsPushed();
        }

        else if (Input.GetAxis("Y") == 1)
        {
            if (OnButtonYIsPushed != null)
                OnButtonYIsPushed();
        }
        else
        {
            if (OnNoButtonIsPushed != null)
                OnNoButtonIsPushed();
        }
        

        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");


        if (horizontalAxis != 0 || verticalAxis != 0)
        {
            if (OnMovementIsTriggered != null)
                OnMovementIsTriggered(horizontalAxis, verticalAxis);
        }
    }
    
    
}
