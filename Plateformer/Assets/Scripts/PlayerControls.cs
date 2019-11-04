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

    public enum Color { Red, Blue, Yellow, Green, Purple }

    public Color color;


    public MovementManager movementManager;


    private void LateUpdate()
    {
        float newPositionX = transform.localPosition.x + speed.x;
        float newPositionY = transform.localPosition.y + speed.y;


        //Vérification si le joueur peut se déplacer
        if (blockMoveUp && blockedPosition.y < newPositionY || blockMoveDown && blockedPosition.y > newPositionY) newPositionY = blockedPosition.y;

        if (blockMoveRight && blockedPosition.x < newPositionX || blockMoveLeft && blockedPosition.x > newPositionX) newPositionX = blockedPosition.x;

        movementManager.MovePlayer();

        //Saut mural
        movementManager.Jump_On_Wall((blockMoveRight || blockMoveLeft) && !blockMoveDown);


        //Modification de la position
        transform.localPosition = new Vector2(newPositionX, newPositionY);
        var pos = transform.localPosition;
        var scale = transform.localScale;

        
        ///// Mise à jour de la vitesse /////
        speed = movementManager.CalcSpeed();
        movementManager.DecayJump();


        //Calcul des positions de la prochaine frame        
        playerLeft = pos.x - scale.x / 2 + speed.x;
        playerRight = pos.x + scale.x / 2 + speed.x;
        playerUp = pos.y + scale.y / 2 + speed.y;
        playerDown = pos.y - scale.y / 2 + speed.y;

        blockMoveRight = false;
        blockMoveLeft = false;
        blockMoveDown = false;
        blockMoveUp = false;

        //  transform.SetParent(null);
    }

    public void Reset_Jumps()
    {
        movementManager.Reset_Jumps();
    }
}
