using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    
    public float speedMove;
    public float speedFall;
    public float speedMoveJump;

    //public float maxSpeedJump;
    //public float accelerationJump;
    public int heightJump;

    public Vector2 position;
    public Vector2 speed;
    public Vector2 acceleration;


    public bool isOnPlateform;
    public bool isJumping;
    public bool isBlocked;

    public int jumpingCount;

    public GameObject plateformPlayerIs;

    public int raycastPrecision;

    public float maxMoveRight;
    public float maxMoveLeft;
    public float maxMoveTop;
    public float maxMoveDown;

    public double playerLeft, playerRight, playerUp, playerDown;

    public bool blockMoveDown, blockMoveUp, blockMoveLeft, blockMoveRight;

    public Vector2 test;
    public enum Color { Red, Blue, Yellow, Green, Purple }

    public Color color;

    private MovementManager movementManager;
    // Start is called before the first frame update
    void Start()
    {
        MovementManager movementManager = GetComponent<MovementManager>();
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("lol");
        if (collision.gameObject.tag == "Plateform")
        {
            isOnPlateform = true;
            plateformPlayerIs = collision.gameObject;
        }

        if (collision.gameObject.tag == "BlockMove")
        {
            isOnPlateform = false;
            plateformPlayerIs = null;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Plateform")
        {
            isOnPlateform = false;
            plateformPlayerIs = null;
        }

        
    }

    //return [CanMoveRight, CanMoveLeft, CanMoveTop, CanMoveDown]
    List<bool> UpdateRaycast()
    {
        List<bool> resu = new List<bool>(4);
        int i = 0;
        while (i < GetComponent<RectTransform>().sizeDelta.x)
        {
            i += raycastPrecision;

        }

        return resu;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 frameMovement = movementManager.CalcSpeed();
        movementManager.DecayJump();

        //Calcul des nouvelles positions
        float newPositionX = transform.position.x + frameMovement.x;
        float newPositionY = transform.position.y + frameMovement.y;


        //Vérification si le joueur peut se déplacer
        if (blockMoveUp && position.y < newPositionY || blockMoveDown && position.y > newPositionY) newPositionY = position.y;

        if (blockMoveRight && position.x < newPositionX || blockMoveLeft && position.x > newPositionX) newPositionX = position.x;


        //Modification de la position
        transform.position = new Vector2( newPositionX, newPositionY );


        ///// Mise à jour du blocage //////

        if (Input.GetAxis("Horizontal") < 0)
        {
            if (blockMoveRight) blockMoveRight = false;
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            if (blockMoveLeft) blockMoveLeft = false;
        }

        if (Input.GetKeyDown("space"))
        {
            if (!isJumping)
            {
                blockMoveDown = false;
                isJumping = true;
            }
        }

        if (!isOnPlateform && !isJumping && !blockMoveDown)
        {
            speed.y = -speedFall;
        }

        //Calcul des positions de la prochaine frame        
        playerLeft = transform.position.x - transform.localScale.x / 2 + frameMovement.x;
        playerRight = transform.position.x + transform.localScale.x / 2 + frameMovement.x;
        playerUp = transform.position.y + transform.localScale.y / 2 + frameMovement.y;
        playerDown = transform.position.y - transform.localScale.y / 2 + frameMovement.y;

        //Mise à jour des mouvements déjà bloqués
        AbstractPlateform.upAlreadyBlocked = false;
        AbstractPlateform.downAlreadyBlocked = false;
        AbstractPlateform.leftAlreadyBlocked = false;
        AbstractPlateform.rightAlreadyBlocked = false;
    }
}
