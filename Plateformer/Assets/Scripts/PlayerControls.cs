using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public Vector2 blockedPosition;
    public Vector2 speed;

    public bool isOnPlateform;
    public bool isJumping;

    public double playerLeft, playerRight, playerUp, playerDown;

    public bool blockMoveDown, blockMoveUp, blockMoveLeft, blockMoveRight;

    public Vector2 test;
    public enum Color { Red, Blue, Yellow, Green, Purple }

    public Color color;


    public MovementManager movementManager;


    // Start is called before the first frame update
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        //Calcul des nouvelles positions
        float newPositionX = transform.position.x + speed.x;
        float newPositionY = transform.position.y + speed.y;


        //Vérification si le joueur peut se déplacer
        if (blockMoveUp && blockedPosition.y < newPositionY || blockMoveDown && blockedPosition.y > newPositionY) newPositionY = blockedPosition.y;

        if (blockMoveRight && blockedPosition.x < newPositionX || blockMoveLeft && blockedPosition.x > newPositionX) newPositionX = blockedPosition.x;


        //Modification de la position
        transform.position = new Vector2(newPositionX, newPositionY);



        ///// Mise à jour de la vitesse /////
        speed = movementManager.CalcSpeed();
        movementManager.DecayJump();

        //Mise à jour du blocage
        if (speed.x < 0 && blockMoveRight) blockMoveRight = false;
        if (speed.x > 0 && blockMoveLeft) blockMoveLeft = false;
        if (speed.y > 0 && blockMoveDown) blockMoveDown = false;
        if (speed.y < 0 && blockMoveUp) blockMoveUp = false;


        //Calcul des positions de la prochaine frame        
        playerLeft = transform.position.x - transform.localScale.x / 2 + speed.x;
        playerRight = transform.position.x + transform.localScale.x / 2 + speed.x;
        playerUp = transform.position.y + transform.localScale.y / 2 + speed.y;
        playerDown = transform.position.y - transform.localScale.y / 2 + speed.y;


        //Mise à jour des mouvements déjà bloqués
        AbstractPlateform.upAlreadyBlocked = false;
        AbstractPlateform.downAlreadyBlocked = false;
        AbstractPlateform.leftAlreadyBlocked = false;
        AbstractPlateform.rightAlreadyBlocked = false;
    }

    public void Reset_Jumps()
    {
        movementManager.Reset_Jumps();
    }

    public void Jump_On_Wall()
    {
        movementManager.Jump_On_Wall();
    }

    ////ARCHIVES////
    /*
    public bool isBlocked;

    public int jumpingCount;

    public GameObject plateformPlayerIs;

    public int raycastPrecision;

    public float maxMoveRight;
    public float maxMoveLeft;
    public float maxMoveTop;
    public float maxMoveDown;

    public Vector2 acceleration;

    public Vector2 frameMovement;

    public float speedMove;
    public float speedFall;
    public float speedMoveJump;

    public float maxSpeedJump;
    public float accelerationJump;
    public int heightJump;
    */
}
