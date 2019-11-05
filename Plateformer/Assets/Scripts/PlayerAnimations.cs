using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public Vector2 defaultScale;

    public float maxReposFactor;
    public float minReposFactor;
    public float reposDeltaFactor;
    int reposFactorZ;

    public float maxWalkFactor;
    public float minWalkFactor;
    public float walkDeltaFactor;
    int walkFactorZ;

    public float maxJumpFactor;
    public float jumpDeltaFactor;

    public float minFallFactor;
    public float fallDeltaFactor;

    public float minLandFactor;
    public float landDeltaFactor;
    public float raiseDeltaFactor;
    int landFactorZ;

    bool isWalking;
    bool isStatic;
    bool isJumping;
    bool isFalling;
    bool isLanding;

    float factor;

    PlayerControls playerControls;

    [HideInInspector]
    public float newScaleX;
    [HideInInspector]
    public float newScaleY;

    // Start is called before the first frame update
    void Start()
    {
        playerControls = GetComponent<PlayerControls>();
        reposFactorZ = 1;
        walkFactorZ = 1;
        landFactorZ = 1;
        defaultScale = new Vector2(1, 1);
    }

    // Update is called once per frame
    public void Animate()
    {
        transform.localScale = new Vector3(newScaleX, newScaleY,1);

        Update_Player_State();

        if (isStatic) Update_Static_Animation();

        else if (isWalking) Update_Walk_Animation();

        else if (isJumping) Update_Jump_Animation();

        else if (isFalling) Update_Falling_Animation();

        else if (isLanding) Update_Landing_Animation();

        else
        {
            factor = 0;
        }


        newScaleX = 1 - factor;
        newScaleY = 1 + factor;
    }

    public void Update_Walk_Animation()
    {
        factor += walkFactorZ * walkDeltaFactor;
        if (factor >= maxWalkFactor)
        {
            factor = maxWalkFactor;
            walkFactorZ = -1;
        }
        if (factor <= minWalkFactor)
        {
            factor = minWalkFactor;
            walkFactorZ = 1;
        }
    }

    public void Update_Static_Animation()
    {
        factor += reposFactorZ * reposDeltaFactor;
        if (factor >= maxReposFactor)
        {
            factor = maxReposFactor;
            reposFactorZ = -1;
        }
        if (factor <= minReposFactor)
        {
            factor = minReposFactor;
            reposFactorZ = 1;
        }
    }

    public void Update_Jump_Animation()
    {
        factor += jumpDeltaFactor;
        
        if (factor >= maxJumpFactor)
        {
            factor = maxJumpFactor;
        }
    }

    public void Update_Falling_Animation()
    {
        factor -= fallDeltaFactor;
        if (factor <= minFallFactor)
        {
            factor = minFallFactor;
        }
    }

    public void Update_Landing_Animation()
    {
        if (landFactorZ == 1)
        {
            factor -= landDeltaFactor;

            if (factor <= minLandFactor)
            {
                factor = minLandFactor;
                landFactorZ = -1;
            }
        }

        else 
        {
            factor += raiseDeltaFactor;
            if (factor >= 0)
            {
                isLanding = false;
                landFactorZ = 1;
            }

        }
    }

    public void Update_Player_State()
    {

        if (playerControls.isJumping)
        {
            isJumping = true;
            isStatic = false;
            isWalking = false;
            isFalling = false;
        }

        else if (playerControls.blockMoveDown == false)
        {
            isFalling = true;
            isStatic = false;
            isJumping = false;
            isWalking = false;
            isLanding = false;
        }

        else if (isFalling || isLanding)
        {
            isJumping = false;
            isStatic = false;
            isWalking = false;
            isFalling = false;
            isLanding = true;
        }

        else if (playerControls.speed.x == 0 && playerControls.isOnPlateform)
        {
            isStatic = true;
            isWalking = false;
            isJumping = false;
            isFalling = false;
            isLanding = false;
        }

        else if (playerControls.speed.x != 0 && playerControls.isOnPlateform)
        {
            isWalking = true;
            isStatic = false;
            isJumping = false;
            isFalling = false;
            isLanding = false;
        }

        else
        {
            isWalking = false;
            isStatic = false;
            isJumping = false;
            isFalling = false;
            isLanding = false;
        }
    }

}
