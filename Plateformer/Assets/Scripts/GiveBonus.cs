﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GiveBonus : MonoBehaviour
{
    
    [SerializeField]
    public string textToDisplay = "";
    
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
    private bool doWeGiveRed;
    
    [SerializeField] 
    private bool doWeGiveBlue;
    
    [SerializeField] 
    private bool doWeGiveYellow;
    
    [SerializeField]
    public AudioSource bonusSound;
    
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
        player.GetComponent<MovementManager>().CheckpointPos = checkpoint.transform.position;
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
    * Give the player the ability to switch to the Blue Color
    */
    private void GiveRed()
    {
        player.GetComponent<PlayerControls>().IsRedUnlocked = true;
    }
    
    /**
     * Give the player the ability to switch to the Yellow Color
     */
    private void GiveYellow()
    {
        player.GetComponent<PlayerControls>().IsYellowUnlocked = true;
    }

    private void DisplayText()
    {
        if (textToDisplay != "")
            player.GetComponent<PlayerControls>().DisplayText(textToDisplay);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            DisplayText();
            
            bonusSound.Play(); 
            
            if (checkpoint != null)
                UpdateCheckpoint();

            if (doWeGiveJump)
                GiveJumps(1);
            
            if (doWeGiveRed)
                GiveRed();
            
            if (doWeGiveBlue)
                GiveBlue();

            if (doWeGiveYellow)
                GiveYellow();

            Destroy(this.gameObject);
        }
    }
}
