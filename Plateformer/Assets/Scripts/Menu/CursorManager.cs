using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{

    [SerializeField]
    [Range(1f, 20f)]
    private float speedCoef;

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
            Clic();
        }

        transform.Translate(Input.GetAxis("Horizontal") * SpeedCoef, Input.GetAxis("Vertical") * SpeedCoef, 0);
    }

    /**
     *  @todo
     */
    void Clic()
    {
        /*
        if (transform.position = true)
        {
        }
        */
    }
}
