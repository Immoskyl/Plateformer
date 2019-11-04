using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plateform : MonoBehaviour
{
/*    public static bool rightAlreadyBlocked, leftAlreadyBlocked, downAlreadyBlocked, upAlreadyBlocked;

    public PlayerControls playerControls;


    //public Vector2 test;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerControls = collision.gameObject.GetComponent<PlayerControls>();
            Update_Block_Moves();
            Update_Unblock_Moves();
        }
    }


    private void Update_Block_Moves()
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
                playerControls.blockedPosition.x = plateformLeft - playerControls.transform.localScale.x / 2 - 0.008f;
                rightAlreadyBlocked = true;
            }

            //quand le joueur arrive sur le côté droit de la plateform
            if (playerControls.playerRight > plateformRight && playerControls.playerLeft < plateformRight)
            {
                playerControls.blockMoveLeft = true;
                playerControls.blockedPosition.x = plateformRight + 0.008f + playerControls.transform.localScale.x / 2;
                leftAlreadyBlocked = true;
            }
        }

        //Quand le joueur est au dessus ou en dessous de la plateform
        if (plateformLeft < (playerControls.transform.position.x - playerControls.transform.localScale.x / 2) && (playerControls.transform.position.x - playerControls.transform.localScale.x / 2) < plateformRight
            || plateformLeft < (playerControls.transform.position.x + playerControls.transform.localScale.x / 2) && (playerControls.transform.position.x + playerControls.transform.localScale.x / 2) < plateformRight)
        {
            print("banane");
            //Quand le joueur arrive au dessus de la plateform
            if (playerControls.playerUp > plateformUp && playerControls.playerDown < plateformUp)
            {
                playerControls.blockMoveDown = true;
                playerControls.blockedPosition.y = plateformUp + playerControls.transform.localScale.y / 2 + 0.008f;
                downAlreadyBlocked = true;
            }

            //Quand le joueur arrive en dessous de la plateform
            if (playerControls.playerDown < plateformDown && playerControls.playerUp > plateformDown)
            {
                print("grougour");
                playerControls.blockMoveUp = true;
                playerControls.blockedPosition.y = plateformDown - playerControls.transform.localScale.y / 2 - 0.008f;
                upAlreadyBlocked = true;
            }
        }
    }

    private void Update_Unblock_Moves()
    {
        //Positions du joueur à la frame d'après
        float plateformLeft = transform.position.x - transform.localScale.x / 2;
        float plateformRight = transform.position.x + transform.localScale.x / 2;
        float plateformUp = transform.position.y + transform.localScale.y / 2;
        float plateformDown = transform.position.y - transform.localScale.y / 2;


        //Quand le joueur arrive au dessus de la plateform
        if (!(playerControls.playerDown > plateformUp) && !downAlreadyBlocked)
        {
            playerControls.blockMoveDown = false;
        }

        //quand le joueur arrive sur le côté gauche de la plateform
        if (!(playerControls.playerLeft > plateformRight) && !leftAlreadyBlocked)
        {
            playerControls.blockMoveLeft = false;
        }

        //quand le joueur arrive sur le côté droit de la plateform
        if (!(playerControls.playerRight < plateformLeft) && !rightAlreadyBlocked)
        {
            playerControls.blockMoveRight = false;
        }

        //Quand le joueur arrive au dessus de la plateform
        if (!(playerControls.playerUp < plateformDown) && !upAlreadyBlocked)
        {
            playerControls.blockMoveUp = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerControls = null;
        }
    }

    private void Update()
    {
        if (playerControls != null)
        {
            GameObject player = playerControls.gameObject;
        }
    }*/
}
