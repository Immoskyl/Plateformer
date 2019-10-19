using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPlateform : MonoBehaviour
{
    public static bool rightAlreadyBlocked, leftAlreadyBlocked, downAlreadyBlocked, upAlreadyBlocked;

    public PlayerControls playerControls;

    public enum PlateformColor { Red, Blue, Yellow, Green, Purple}

    public PlateformColor plateformColor;

    public abstract void Update_Block_Moves();

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


    private void Update_Unblock_Moves()
    {
        //Positions du joueur à la frame d'après
        float plateformLeft = transform.position.x - transform.localScale.x / 2;
        float plateformRight = transform.position.x + transform.localScale.x / 2;
        float plateformUp = transform.position.y + transform.localScale.y / 2;
        float plateformDown = transform.position.y - transform.localScale.y / 2;

        print("spaguet");

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
    }
}
