using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    enum Axis {X, Y, Z };

    //=============== VARIABLES ================

    [SerializeField] private float speed = default;
    [SerializeField] private Axis axis = default;

    //==========================================

    void FixedUpdate()
    {
        switch (axis)
        {
            case Axis.X:
                transform.Rotate(speed, 0, 0, Space.Self);
                break;
            case Axis.Y:
                transform.Rotate(0, speed, 0, Space.Self);
                break;
            case Axis.Z:
                transform.Rotate(0, 0, speed, Space.Self);
                break;
            default:
                Debug.Log("Incorrect Axis.");
                break;
        }
    }
}
