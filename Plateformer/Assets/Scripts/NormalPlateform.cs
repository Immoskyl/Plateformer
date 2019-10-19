using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPlateform : AbstractPlateform
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Update_Block_Moves()
    {
        //Positions du joueur à la frame d'après
        float plateformLeft = transform.position.x - transform.localScale.x / 2;
        float plateformRight = transform.position.x + transform.localScale.x / 2;
        float plateformUp = transform.position.y + transform.localScale.y / 2;
        float plateformDown = transform.position.y - transform.localScale.y / 2;


        //quand le joueur est sur le coté de la plateform
        if (plateformDown < (playerControls.transform.position.y - playerControls.transform.localScale.y / 2) && (playerControls.transform.position.y - playerControls.transform.localScale.y / 2) < plateformUp
            || plateformDown < (playerControls.transform.position.y + playerControls.transform.localScale.y / 2) && (playerControls.transform.position.y + playerControls.transform.localScale.y / 2) < plateformUp)
        {
            //quand le joueur arrive sur le côté gauche de la plateform
            if (playerControls.playerLeft < plateformLeft && playerControls.playerRight > plateformLeft)
            {
                playerControls.blockMoveRight = true;
                playerControls.position.x = plateformLeft - playerControls.transform.localScale.x / 2 - 0.008f;
                rightAlreadyBlocked = true;
            }

            //quand le joueur arrive sur le côté droit de la plateform
            if (playerControls.playerRight > plateformRight && playerControls.playerLeft < plateformRight)
            {
                playerControls.blockMoveLeft = true;
                playerControls.position.x = plateformRight + 0.008f + playerControls.transform.localScale.x / 2;
                leftAlreadyBlocked = true;
            }
        }

        //Quand le joueur est au dessus ou en dessous de la plateform
        if (plateformLeft < (playerControls.transform.position.x - playerControls.transform.localScale.x / 2) && (playerControls.transform.position.x - playerControls.transform.localScale.x / 2) < plateformRight
            || plateformLeft < (playerControls.transform.position.x + playerControls.transform.localScale.x / 2) && (playerControls.transform.position.x + playerControls.transform.localScale.x / 2) < plateformRight)
        {
            //print("banane");
            //Quand le joueur arrive au dessus de la plateform
            if (playerControls.playerUp > plateformUp && playerControls.playerDown < plateformUp)
            {
                playerControls.blockMoveDown = true;
                playerControls.position.y = plateformUp + playerControls.transform.localScale.y / 2 + 0.008f;
                downAlreadyBlocked = true;
            }

            //Quand le joueur arrive en dessous de la plateform
            if (playerControls.playerDown < plateformDown && playerControls.playerUp > plateformDown)
            {
                //print("grougour");
                playerControls.blockMoveUp = true;
                playerControls.position.y = plateformDown - playerControls.transform.localScale.y / 2 - 0.008f;
                upAlreadyBlocked = true;
            }
        }
    }
}
