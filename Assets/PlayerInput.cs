using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //public VirtualJoyStick virtualJoyStick;
    

    public string hAxis = "Horizontal";


    public float move {get; private set;}

    void Update()
    {
        //move = virtualJoyStick.touchPosition.x;
        move = Input.GetAxis(hAxis);
    }
}
