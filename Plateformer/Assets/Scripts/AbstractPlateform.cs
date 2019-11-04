using System.Collections;
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
