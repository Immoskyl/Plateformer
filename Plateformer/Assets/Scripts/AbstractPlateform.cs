﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPlateform : MonoBehaviour
{

    public bool movingPlateform;
    public Vector2 speed;

    
    public PlayerControls playerControls;
    public bool isPlayerNear;

    public PlayerControls.Color plateformColor;

    public Sprite redSprite;
    public Sprite blueSprite;
    public Sprite yellowSprite;
    public Sprite greenSprite;
    public Sprite purpleSprite;

    public abstract void Update_Block_Moves();

    public Vector2 test;

    private void OnTriggerStay2D(Collider2D collision)
    {/*
        Debug.Log("hello");
        
        if (collision.gameObject.tag == "Player")
        {

            playerControls = collision.gameObject.GetComponent<PlayerControls>();

            if (plateformColor == playerControls.color)
            {
                Update_Block_Moves();
                //Update_Unblock_Moves();
            }
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
            playerControls = collision.gameObject.GetComponent<PlayerControls>();
            isPlayerNear = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerControls = null;
            isPlayerNear = false;
        }
    }

    /*
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
    */

    public void changeColor(PlayerControls.Color _color)
    {
        if (_color != plateformColor)
        {
            plateformColor = _color;
            switch (_color)
            {
                case PlayerControls.Color.Red :
                    GetComponent<SpriteRenderer>().sprite = redSprite;
                    break;

                case PlayerControls.Color.Blue:
                    GetComponent<SpriteRenderer>().sprite = blueSprite;
                    break;

                case PlayerControls.Color.Yellow:
                    GetComponent<SpriteRenderer>().sprite = yellowSprite;
                    break;

                case PlayerControls.Color.Green:
                    GetComponent<SpriteRenderer>().sprite = greenSprite;
                    break;

                case PlayerControls.Color.Purple:
                    GetComponent<SpriteRenderer>().sprite = purpleSprite;
                    break;
            }
                 
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerControls = null;
        }
    }




}
