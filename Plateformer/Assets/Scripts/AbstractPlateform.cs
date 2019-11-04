using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPlateform : MonoBehaviour
{
    //vrai ssi la plateforme bouge
    private bool _movingPlateform;

    //vitesse de la plateforme
    private Vector2 _speed;

    //vrai ssi le joueur est dans le collider de la plateforme (qui est plus grand que la plateforme)
    private bool _isPlayerNear;

    public bool movingPlateform
    {
        get { return _movingPlateform; }
        set { _movingPlateform = value; }
    }

    public Vector2 speed 
    {
        get { return _speed; }
        set { _speed = value; }
    }

    public bool isPlayerNear 
    {
        get { return _isPlayerNear; }
        set { _isPlayerNear = value; }
    }


    [HideInInspector]
    public PlayerControls playerControls;

    //Couleure de la plateform et les sprites associées
    public PlayerControls.Color plateformColor;
    public Sprite redSprite;
    public Sprite blueSprite;
    public Sprite yellowSprite;
    public Sprite greenSprite;
    public Sprite neutreSprite;

    //fonction qui gére la collision entre le joueur et la plateforme
    public abstract void Update_Block_Moves();

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

    private void Awake()
    {
        changeColor(plateformColor);
    }

    //fonction pour changer la couleur de la plateforme
    public void changeColor(PlayerControls.Color _color)
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

            case PlayerControls.Color.Neutre:
            default:
                //GetComponent<SpriteRenderer>().sprite = neutreSprite;
                break;
        }
    }
}
