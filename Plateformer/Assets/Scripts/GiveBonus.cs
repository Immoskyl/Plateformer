using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveBonus : MonoBehaviour
{
    /**
     * The player to give bonuses to
     */
    [SerializeField]
    private GameObject player;
    
    /**
     * The player to give bonuses to
     */
    [SerializeField]
    private GameObject checkpoint;

    [SerializeField] 
    private bool doWeGiveJump;
    
    [SerializeField] 
    private bool doWeGiveBlue;
    
    [SerializeField] 
    private bool doWeGiveYellow;
    
    public GameObject Player
    {
        get => player;
        set => player = value;
    }

    public GameObject Checkpoint
    {
        get => checkpoint;
        set => checkpoint = value;
    }

    private void UpdateCheckpoint()
    {
        checkpoint.transform.position = transform.position;
    }

    /**
     * Give the player the ability to jump once more in mid-air
     */
    private void GiveJumps(int nbOfJumps)
    {
        player.GetComponent<MovementManager>().NumberOfJumps += nbOfJumps;
    }

    /**
     * Give the player the ability to switch to the Blue Color
     */
    private void GiveBlue()
    {
        player.GetComponent<PlayerControls>().IsBlueUnlocked = true;
    }
    
    /**
     * Give the player the ability to switch to the Yellow Color
     */
    private void GiveYellow()
    {
        player.GetComponent<PlayerControls>().IsYellowUnlocked = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (checkpoint != null)
            UpdateCheckpoint();
        
        if (doWeGiveJump)
            GiveJumps(1);
        
        if (doWeGiveBlue)
            GiveBlue();
        
        if (doWeGiveYellow)
            GiveYellow();
        
        Destroy(this.gameObject);
    }

}
