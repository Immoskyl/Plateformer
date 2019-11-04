using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlateform : MonoBehaviour
{
    public AbstractPlateform abstractPlateform;
    public Vector2 originalPosition;

    public enum MoveType { Linear, Rectangle, Ovale }

    public MoveType moveType;

    public Vector2 newPosition;

    //Linear Movement
    public float xSpeedL;
    public float ySpeedL;
    public float distanceL;
    public float rightTimeStopL;
    public float leftTimeStopL;
    
    public int distanceFactor;
    public bool justChanged;


    //Rectangle Movement
    public bool horizontalFirst;
    public float xSpeedR;
    public float ySpeedR;
    public float yDistanceR;
    public float xDistanceR;
    public float upRightTimeStopR;
    public float upLeftTimeStopR;
    public float downLeftTimeStopR;
    public float downRightTimeStopR;

    public int xDistanceFactor;
    public int yDistanceFactor;

    
    private bool wait;
    private float waitingTime;
    private float _waitingTime;

    private void Start()
    {
        abstractPlateform = GetComponent<AbstractPlateform>();
        abstractPlateform.movingPlateform = true;

        newPosition = new Vector2(transform.position.x, transform.position.y);

        if (moveType == MoveType.Linear)
        {
            distanceFactor = 1;
            justChanged = false;
            originalPosition.x = transform.position.x;
            originalPosition.y = transform.position.y;
            abstractPlateform.speed = new Vector2(xSpeedL,ySpeedL);
        }
        
        if (moveType == MoveType.Rectangle)
        {
            if (horizontalFirst)
            {
                xDistanceFactor = 1;
                yDistanceFactor = 0;
            }
            else
            {
                xDistanceFactor = 0;
                yDistanceFactor = 1;
            }
            originalPosition.x = transform.position.x + xDistanceR;
            originalPosition.y = transform.position.y + yDistanceR;
            abstractPlateform.speed = new Vector2(xSpeedR, ySpeedR);
        }
    }

    private void Update()
    {
        if (!wait)
        {
            transform.position = new Vector2(newPosition.x, newPosition.y);

            Vector2 positionFromOriginalPosition = new Vector2(transform.position.x - originalPosition.x, transform.position.y - originalPosition.y);

            if (moveType == MoveType.Linear)
            {    
                if (positionFromOriginalPosition.magnitude > distanceL && !justChanged)
                {
                    distanceFactor *= -1;
                    wait = true;

                    if (transform.position.x < originalPosition.x || transform.position.x == originalPosition.x && transform.position.y > originalPosition.y) waitingTime = leftTimeStopL;
                    else waitingTime = rightTimeStopL;
                    justChanged = true;
                }
                if (justChanged && positionFromOriginalPosition.magnitude <= distanceL) justChanged = false;

                newPosition = new Vector2(transform.position.x + xSpeedL * Time.deltaTime * distanceFactor, transform.position.y + ySpeedL * Time.deltaTime * distanceFactor);
            }

            if (moveType == MoveType.Rectangle)
            {
                if (xDistanceFactor == 0 && ( positionFromOriginalPosition.y > yDistanceR || -positionFromOriginalPosition.y > yDistanceR ) )
                {
                    if (yDistanceFactor == 1) xDistanceFactor = -1;
                    if (yDistanceFactor == -1) xDistanceFactor = 1; 
                    yDistanceFactor = 0;
                    if (positionFromOriginalPosition.y > yDistanceR) transform.position = new Vector2(transform.position.x, originalPosition.y + yDistanceR);
                    if (-positionFromOriginalPosition.y > yDistanceR) transform.position = new Vector2(transform.position.x, originalPosition.y - yDistanceR);
                }

                else if (yDistanceFactor == 0 && (positionFromOriginalPosition.x > xDistanceR || -positionFromOriginalPosition.x > xDistanceR ) )
                {
                    if (xDistanceFactor == 1) yDistanceFactor = 1;
                    if (xDistanceFactor == -1) yDistanceFactor = -1;
                    xDistanceFactor = 0;
                    if (positionFromOriginalPosition.x > xDistanceR) transform.position = new Vector2(originalPosition.x + xDistanceR, transform.position.y);
                    if (-positionFromOriginalPosition.x > xDistanceR) transform.position = new Vector2(originalPosition.x - xDistanceR, transform.position.y);
                }

                newPosition = new Vector2(transform.position.x + xSpeedR * Time.deltaTime * xDistanceFactor, transform.position.y + ySpeedR * Time.deltaTime * yDistanceFactor);
            }

        }

        else
        {
            _waitingTime += Time.deltaTime;
            if (_waitingTime > waitingTime)
            {
                _waitingTime = 0;
                wait = false;
            }
        }
    }
}
