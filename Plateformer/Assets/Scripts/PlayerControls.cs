using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public enum Color { Red, Blue, Yellow, Green, Neutre }

    //couleur de la plateform
    private Color _color;

    //vitesse du joueur
    private Vector2 _speed;


    //true ssi le joueur est en train de sauter
    private bool _isJumping;

    
    public Vector2 speed
    {
        get { return _speed; }
        set { _speed = value; }
    }    
    
    public bool isJumping
    {
        get { return _isJumping; }
        set { _isJumping = value; }
    }

    public Color color
    {
        get { return _color; }
        set { _color = value; }
    }

    //extrémités gauche droite haute et basse du joueur à la prochaine frame
    [HideInInspector]
    public float playerLeft, playerRight, playerUp, playerDown;

    //Le joueur est bloqué et ne peut plus aller plus loin vers le bas, le haut, la gauche, la droite
    [HideInInspector]
    public bool blockMoveDown, blockMoveUp, blockMoveLeft, blockMoveRight;

    //script qui gère les mouvements du joueur
    [HideInInspector]
    public MovementManager movementManager;

    //Valeur des positions que le joueur ne peut dépasser lorsqu'il est bloqué
    [HideInInspector]
    public Vector2 blockedPosition;
    
    private bool isRedUnlocked;
    
    private bool isBlueUnlocked;

    private bool isYellowUnlocked;

    public bool IsRedUnlocked
    {
        get => isRedUnlocked;
        set => isRedUnlocked = value;
    }

    public bool IsBlueUnlocked
    {
        get => isBlueUnlocked;
        set => isBlueUnlocked = value;
    }

    public bool IsYellowUnlocked
    {
        get => isYellowUnlocked;
        set => isYellowUnlocked = value;
    }

    private void Awake()
    {
        color = Color.Red;
    }

    private void LateUpdate()
    {
        float newPositionX = transform.localPosition.x + speed.x;
        float newPositionY = transform.localPosition.y + speed.y;


        //Vérification si le joueur peut se déplacer
        if (blockMoveUp && blockedPosition.y < newPositionY || blockMoveDown && blockedPosition.y > newPositionY) newPositionY = blockedPosition.y;

        if (blockMoveRight && blockedPosition.x < newPositionX || blockMoveLeft && blockedPosition.x > newPositionX) newPositionX = blockedPosition.x;

        movementManager.MovePlayer();

        //Saut mural
        //movementManager.Jump_On_Wall((blockMoveRight || blockMoveLeft) && !blockMoveDown);


        //Modification de la position
        transform.localPosition = new Vector2(newPositionX, newPositionY);
        var pos = transform.localPosition;
        var scale = transform.localScale;

        
        ///// Mise à jour de la vitesse /////
        speed = movementManager.CalcSpeed();
        movementManager.DecayJump();

        //Permet de savoir si le joueur saute
        if (blockMoveDown) isJumping = false;
        else isJumping = true;

        //Calcul des positions de la prochaine frame        
        playerLeft = pos.x - scale.x / 2 + speed.x;
        playerRight = pos.x + scale.x / 2 + speed.x;
        playerUp = pos.y + scale.y / 2 + speed.y;
        playerDown = pos.y - scale.y / 2 + speed.y;


        //Mise à jour du blocage
        blockMoveRight = false;
        blockMoveLeft = false;
        blockMoveDown = false;
        blockMoveUp = false;
    }

    void Update()
    {
        if (Input.GetButton("SwitchRed") && isRedUnlocked)
        {
            GetComponent<SpriteRenderer>().color = UnityEngine.Color.red;
            color = Color.Red;
        }
        else if (Input.GetButton("SwitchBlue") && isBlueUnlocked)
        {
            GetComponent<SpriteRenderer>().color = UnityEngine.Color.blue;
            color = Color.Blue;
        }

        else if (Input.GetButton("SwitchYellow") && isYellowUnlocked)
        {
            GetComponent<SpriteRenderer>().color = UnityEngine.Color.yellow;
            color = Color.Yellow;
        }
    }

    public void Reset_Jumps()
    {
        movementManager.ResetJumps();
    }
}
