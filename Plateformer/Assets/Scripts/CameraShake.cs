using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    
    /**
     * Duration of the camera shaking when the player dies
     */
    [SerializeField]
    [Range(0f, 5f)]
    private float shakeDuration = 1f;
    
    /**
     * Amplitude of the camera shaking when the player dies
     */
    [SerializeField]
    [Range(0f, 2f)]
    private float shakeMagnitude = 0.7f;
    

    // Transform of the GameObject you want to shake
    private Transform _transform;

    // Desired duration of the shake effect
    private float actualShakeDuration = 0f;

    // A measure of how quickly the shake effect should evaporate
    private float dampingSpeed = 1.0f;

    // The initial position of the GameObject
    Vector3 initialPosition;

    void Awake()
    {
        if (_transform == null)
        {
            _transform = Camera.main.gameObject.transform;
        }
    }

    void OnEnable()
    {
        initialPosition = _transform.localPosition;
    }

    /**
     *  If shakeDuration is positive, adjust the game object’s transform by a random factor (that’s our shake).
     *  Decrement shakeDuration so that your camera does not shake forever.
     *  Once shakeDuration reaches 0, return your camera’s GameObject to its initial position
     */
    void Update()
    {
        if (actualShakeDuration > 0)
        {
            _transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            actualShakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            actualShakeDuration = -1f;
            _transform.localPosition = initialPosition;
        }
    }

    /**
     * Call this function to trigger the camera shake!
     */
    public void TriggerShake()
    {
        actualShakeDuration = shakeDuration;
    }
  }