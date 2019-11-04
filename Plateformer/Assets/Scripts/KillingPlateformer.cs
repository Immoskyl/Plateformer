using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillingPlateformer : AbstractPlateform
{
    // Update is called once per frame
    void Update()
    {
        if (isPlayerNear)
        {
            if (plateformColor == playerControls.color || plateformColor == PlayerControls.Color.Neutre) Update_Block_Moves();
        }
    }

    public override void Update_Block_Moves()
    {
        //Positions de la plateform à la frame d'après
        float plateformLeft = transform.position.x - transform.localScale.x / 2 - Time.deltaTime * speed.x;
        float plateformRight = transform.position.x + transform.localScale.x / 2 + Time.deltaTime * speed.x;
        float plateformUp = transform.position.y + transform.localScale.y / 2 + Time.deltaTime * speed.y;
        float plateformDown = transform.position.y - transform.localScale.y / 2 - Time.deltaTime * speed.y;

        float playerLeft = playerControls.playerLeft;
        float playerRight = playerControls.playerRight;
        float playerDown = playerControls.playerDown;
        float playerUP = playerControls.playerUp;

        //quand le joueur est sur le coté de la plateform
        
        if 
        ( 
            ((plateformDown < playerDown && playerDown < plateformUp) || (plateformDown < playerUP && playerUP < plateformUp))
            && 
            ((plateformLeft < playerRight && playerRight < plateformRight) || (plateformLeft < playerLeft && playerLeft < plateformRight))
        )
        {
            playerControls.movementManager.Die();
        }

    }
}
