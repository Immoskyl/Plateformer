using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{

    [SerializeField]
    [Range(1f, 20f)]
    private float speedCoef;

    public List<GameObject> Buttons;

    public float SpeedCoef
    {
        get => speedCoef;
        set => speedCoef = value;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
           
        }

        GetComponent<RectTransform>().transform.position = new Vector3(Input.GetAxis("Horizontal") * SpeedCoef, Input.GetAxis("Vertical") * SpeedCoef, 0);
        //transform.Translate(Input.GetAxis("Horizontal") * SpeedCoef, Input.GetAxis("Vertical") * SpeedCoef, 0);
    }

    /**
     *  @todo
     */
    void On_Clic()
    {
        
    }
}
