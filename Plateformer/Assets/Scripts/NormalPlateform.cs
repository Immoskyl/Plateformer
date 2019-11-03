using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPlateform : AbstractPlateform
{
    public bool traversable;

    // Start is called before the first frame update
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
       // Debug.Log("bon");
        if (isPlayerNear)
        {
            if (plateformColor == playerControls.color) Update_Block_Moves();
        }
    }

    public override void Update_Block_Moves()
    {
        //Positions de la plateform à la frame d'après
        float currentPlateformLeft = transform.position.x - transform.localScale.x / 2;
        float currentPlateformRight = transform.position.x + transform.localScale.x / 2;
        float currentPlateformUp = transform.position.y + transform.localScale.y / 2;
        float currentPlateformDown = transform.position.y - transform.localScale.y / 2;

        float plateformLeft = currentPlateformLeft - Time.deltaTime * speed.x;
        float plateformRight = currentPlateformRight + Time.deltaTime * speed.x;
        float plateformUp = currentPlateformUp + Time.deltaTime * speed.y;
        float plateformDown = currentPlateformDown - Time.deltaTime * speed.y;


        test.x = plateformLeft;
        test.y = plateformDown;


        
        float currentPlayerLeft = playerControls.transform.localPosition.x - playerControls.transform.localScale.x / 2;
        float currentPlayerRight = playerControls.transform.localPosition.x + playerControls.transform.localScale.x / 2;
        float currentPlayerDown = playerControls.transform.localPosition.y - playerControls.transform.localScale.y / 2;
        float currentPlayerUP = playerControls.transform.localPosition.y + playerControls.transform.localScale.y / 2;

        //quand le joueur est sur le coté de la plateform

        if (currentPlayerRight <= currentPlateformLeft || currentPlayerLeft >= currentPlateformRight)
        {
           // print("non");
            if ((currentPlateformDown < currentPlayerDown && currentPlayerDown < currentPlateformUp)
                || (currentPlateformDown < currentPlayerUP && currentPlayerDown < currentPlateformUp))
            {
                //quand le joueur arrive sur le côté gauche de la plateform
                if (currentPlayerRight <= currentPlateformLeft && playerControls.playerRight > plateformLeft)
                {
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

                    //quand le joueur arrive sur le côté droit de la plateform
                if (currentPlayerLeft >= currentPlateformRight && playerControls.playerLeft < plateformRight)
                {
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

        else if (currentPlayerDown >= currentPlateformUp || currentPlayerUP <= currentPlateformDown)
        {
            //Quand le joueur est au dessus ou en dessous de la plateform
            if ((plateformLeft < currentPlayerLeft && currentPlayerLeft < plateformRight)
                || (plateformLeft < currentPlayerRight && currentPlayerRight < plateformRight))
            {
                //Quand le joueur arrive au dessus de la plateform
                if (currentPlayerDown >= currentPlateformUp && playerControls.playerDown < plateformUp)
                {
                   // print("ok");
                    if (playerControls.blockMoveDown)
                    {
                        if (playerControls.blockedPosition.y < plateformUp + playerControls.transform.localScale.y / 2 + 0.008f)
                        {
                            //if (movingPlateform) playerControls.gameObject.transform.SetParent(transform);
                            playerControls.blockedPosition.y = plateformUp + playerControls.transform.localScale.y / 2 + 0.008f;
                        }
                    }
                    else
                    {
                        //if (movingPlateform) playerControls.gameObject.transform.SetParent(transform);
                        playerControls.blockMoveDown = true;
                        playerControls.blockedPosition.y = plateformUp + playerControls.transform.localScale.y / 2 + 0.008f;
                        playerControls.Reset_Jumps();
                    }
                }

                //Quand le joueur arrive en dessous de la plateform
                if (!traversable)
                {
                    if (currentPlayerUP <= currentPlateformDown && playerControls.playerUp > plateformDown)
                    {
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
