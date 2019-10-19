using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformCollider : MonoBehaviour
{

    public bool playerIsIn;
//    public PlayerControls playerControls;

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerSafeCollider")
        {
            playerIsIn = true;
            //playerControls = collision.gameObject.GetComponent<PlayerControls>();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerIsIn = false;
            //playerControls = null;
        }
    }
}
