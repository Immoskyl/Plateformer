using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPlateform : AbstractPlateform
{
    //Savoir si la plateform est traversable depuis le bas
    public bool traversable;

 
    
    // Update is called once per frame
    void Update()
    {
        if (isPlayerNear)
        {
            if (plateformColor == playerControls.color) 
                Update_Block_Moves();
        }
    }
    
    public void On_Wall_Jump()
    {
        /*
        playerControls.isOnWall = true;

        if (playerControls.canStayOnWall)
        { 
            playerControls.blockMoveDown = true;
            playerControls.blockedPosition.y = playerControls.transform.localPosition.y;
            if (playerControls.movementManager.GetForce(MovementManager.Forces.Jumping) != Vector2.zero) playerControls.movementManager.RemoveForce(MovementManager.Forces.Jumping);
        }
        */
        playerControls.blockMoveDown = true;
        playerControls.blockedPosition.y = playerControls.transform.localPosition.y;
    }

    public override void Update_Block_Moves()
    {
        var pos = transform.position;
        var scale = transform.localScale;
        
        //Positions de la plateform à la frame d'après
        float currentPlateformLeft = pos.x - scale.x / 2;
        float currentPlateformRight = pos.x + scale.x / 2;
        float currentPlateformUp = pos.y + scale.y / 2;
        float currentPlateformDown = pos.y - scale.y / 2;

        //position de la plateform à la frame d'après
        float plateformLeft = currentPlateformLeft - Time.deltaTime * speed.x;
        float plateformRight = currentPlateformRight + Time.deltaTime * speed.x;
        float plateformUp = currentPlateformUp + Time.deltaTime * speed.y;
        float plateformDown = currentPlateformDown - Time.deltaTime * speed.y;

        var playerPos = playerControls.transform.localPosition;
        var playerScale = playerControls.transform.localScale;
        
        float currentPlayerLeft = playerPos.x - playerScale.x / 2;
        float currentPlayerRight = playerPos.x + playerScale.x / 2;
        float currentPlayerDown = playerPos.y - playerScale.y / 2;
        float currentPlayerUP = playerPos.y + playerScale.y / 2;

        //quand le joueur est dans la zone à gauche ou à droite de la plateforme à la frame courante
        if (currentPlayerRight <= currentPlateformLeft || currentPlayerLeft >= currentPlateformRight)
        {
            //si le joueur est sur un des côtés et qu'il est à un y susceptible de collider avec la plateforme
            if ((currentPlateformDown < currentPlayerDown && currentPlayerDown < currentPlateformUp) || (currentPlateformDown < currentPlayerUP && currentPlayerDown < currentPlateformUp)
            || (((plateformDown < playerControls.playerDown && playerControls.playerDown < plateformUp) || (plateformDown < playerControls.playerUp && playerControls.playerUp < plateformUp)) && !playerControls.isOnPlateform))
            {
                //quand le joueur arrive sur trop loin sur le côté gauche de la plateform à la frame d'après
                /////// LEFT COLLIDER ENTER ///////
                if (currentPlayerRight <= currentPlateformLeft && playerControls.playerRight > plateformLeft)
                {
                    //Permet de savoir si le joueur est déjà bloqué par une plateforme légèrement plus à gauche
                    if (playerControls.blockMoveRight)
                    {
                        if (playerControls.blockedPosition.x > plateformLeft - playerControls.transform.localScale.x / 2 - 0.008f) 
                            
                            playerControls.blockedPosition.x = plateformLeft - playerControls.transform.localScale.x / 2 - 0.008f;
                    }
                    else
                    {
                        playerControls.blockedPosition.x = plateformLeft - playerControls.transform.localScale.x / 2 - 0.008f;
                        playerControls.blockMoveRight = true;
                        playerControls.Reset_Jumps();
                    }
                    On_Wall_Jump();
                }

                //quand le joueur arrive trop loin sur le côté droit de la plateform à la frame d'après
                /////// RIGHT COLLIDER ENTER ///////
                else if (currentPlayerLeft >= currentPlateformRight && playerControls.playerLeft < plateformRight)
                {
                    //Permet de savoir si le joueur est déjà bloqué par une plateforme légèrement plus à droite
                    if (playerControls.blockMoveLeft)
                    {
                        if (playerControls.blockedPosition.x < plateformRight + playerControls.transform.localScale.x / 2 + 0.008f)
                    
                            playerControls.blockedPosition.x = plateformRight + playerControls.transform.localScale.x / 2 + 0.008f;
                    }
                    else
                    {
                        playerControls.blockMoveLeft = true;
                        playerControls.blockedPosition.x = plateformRight + playerControls.transform.localScale.x / 2 + 0.008f;
                        playerControls.Reset_Jumps();
                    }
                    On_Wall_Jump();
                }

                //quand le joueur est la mais pas trop loin
                else
                {
                    //playerControls.isOnWall = false;
                }
            }

        }

        //quand le joueur est dans la zone au dessus ou en dessous de la plateforme à la frame courante
        else if (currentPlayerDown >= currentPlateformUp || currentPlayerUP <= currentPlateformDown)
        {
            //Quand le joueur est au dessus ou en dessous de la plateform et qu'il est susceptible de collider avec
            if ((currentPlateformLeft < currentPlayerLeft && currentPlayerLeft < currentPlateformRight)
                || (currentPlateformLeft < currentPlayerRight && currentPlayerRight < currentPlateformRight))
            {

                //Quand le joueur arrive trop bas au dessus de la plateform à la frame d'après
                /////// UP COLLIDER ENTER ///////
                if (currentPlayerDown >= currentPlateformUp && playerControls.playerDown < plateformUp)
                {
                    //Permet de savoir si le joueur est déjà bloqué par une plateforme légèrement plus haute
                    if (playerControls.blockMoveDown)
                    {
                        if (playerControls.blockedPosition.y < plateformUp + playerControls.transform.localScale.y / 2 + 0.008f)
                        {
                            playerControls.blockedPosition.y = plateformUp + playerControls.transform.localScale.y / 2 + 0.008f;
                        }
                    }
                    else
                    {
                        playerControls.blockMoveDown = true;
                        playerControls.blockedPosition.y = plateformUp + playerControls.transform.localScale.y / 2 + 0.008f;
                        playerControls.Reset_Jumps();
                    }
                }

                //Quand le joueur arrive trop haut en dessous de la plateform à la frame d'après
                if (!traversable)
                {
                    /////// DOWN COLLIDER ENTER ///////
                    if (currentPlayerUP <= currentPlateformDown && playerControls.playerUp > plateformDown)
                    {
                        //Permet de savoir si le joueur est déjà bloqué par une plateforme légèrement plus basse
                        if (playerControls.blockMoveUp)
                        {
                            if (playerControls.blockedPosition.y > plateformDown - playerControls.transform.localScale.y / 2 - 0.008f)
                        
                                playerControls.blockedPosition.y = plateformDown - playerControls.transform.localScale.y / 2 - 0.008f;
                        }
                        else
                        {
                            playerControls.blockMoveUp = true;
                            playerControls.blockedPosition.y = plateformDown - playerControls.transform.localScale.y / 2 - 0.008f;
                            playerControls.movementManager.RemoveForce(MovementManager.Forces.Jumping);
                        }
                    }
                }
            }
        }
    }
}
