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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        GetComponent<SpriteRenderer>().color = Color.yellow;
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
        transform.Translate(horizontalAxis * speedCoef, verticalAxis * speedCoef, 0);
    }

}
