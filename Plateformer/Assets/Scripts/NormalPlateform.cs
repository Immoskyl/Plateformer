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
            if (plateformColor == playerControls.color) Update_Block_Moves();
        }
    }

    public override void Update_Block_Moves()
    {
        //Positions de la plateform à la frame courante
        float currentPlateformLeft = transform.position.x - transform.localScale.x / 2;
        float currentPlateformRight = transform.position.x + transform.localScale.x / 2;
        float currentPlateformUp = transform.position.y + transform.localScale.y / 2;
        float currentPlateformDown = transform.position.y - transform.localScale.y / 2;

        //position de la plateform à la frame d'après
        float plateformLeft = currentPlateformLeft - Time.deltaTime * speed.x;
        float plateformRight = currentPlateformRight + Time.deltaTime * speed.x;
        float plateformUp = currentPlateformUp + Time.deltaTime * speed.y;
        float plateformDown = currentPlateformDown - Time.deltaTime * speed.y;

        //position du joueur à la frame courante
        float currentPlayerLeft = playerControls.transform.localPosition.x - playerControls.transform.localScale.x / 2;
        float currentPlayerRight = playerControls.transform.localPosition.x + playerControls.transform.localScale.x / 2;
        float currentPlayerDown = playerControls.transform.localPosition.y - playerControls.transform.localScale.y / 2;
        float currentPlayerUP = playerControls.transform.localPosition.y + playerControls.transform.localScale.y / 2;

        //quand le joueur est dans la zone à gauche ou à droite de la plateforme à la frame courante
        if (currentPlayerRight <= currentPlateformLeft || currentPlayerLeft >= currentPlateformRight)
        {
            //si le joueur est sur un des côtés et qu'il est à un y susceptible de collider avec la plateforme
            if ((currentPlateformDown < currentPlayerDown && currentPlayerDown < currentPlateformUp) || (currentPlateformDown < currentPlayerUP && currentPlayerDown < currentPlateformUp)
            || (((plateformDown < playerControls.playerDown && playerControls.playerDown < plateformUp) || (plateformDown < playerControls.playerUp && playerControls.playerUp < plateformUp)) && playerControls.isJumping))

            {
                //quand le joueur arrive sur trop loin sur le côté gauche de la plateform à la frame d'après
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
                    }
                }

                //quand le joueur arrive trop loin sur le côté droit de la plateform à la frame d'après
                if (currentPlayerLeft >= currentPlateformRight && playerControls.playerLeft < plateformRight)
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
                    }
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
                        }
                    }
                }
            }
        }
    }
}
