using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public MovementManager mm;
    public Text forcesText;

    [SerializeField] public bool isDebugModeOn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDebugModeOn)
        {
            forcesText.enabled = false;
        }
        else
        {
            forcesText.enabled = true;
            float leftForce = mm.GetForce(MovementManager.Forces.GoLeft).x;
            float rightForce = mm.GetForce(MovementManager.Forces.GoRight).x;
            string axisStr = (rightForce - leftForce).ToString("0.0");
        
            float upForce = mm.GetForce(MovementManager.Forces.Jumping).y;
            float downForce = mm.GetForce(MovementManager.Forces.Falling).y;
            string ordinatesStr = (upForce - downForce).ToString("0.0");
        
            forcesText.text = axisStr + "\n" + ordinatesStr;
        }
    }
}
