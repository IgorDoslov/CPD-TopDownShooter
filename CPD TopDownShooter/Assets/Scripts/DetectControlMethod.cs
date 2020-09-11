using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectControlMethod : MonoBehaviour
{
    public PlayerController thePlayer;
    public VirtualJoystick moveJoystick;
    public VirtualJoystick rotationJoystick;

    void Update()
    {
        //detect mouse button input
        if (moveJoystick.InputDirection == Vector3.zero && rotationJoystick.InputDirection == Vector3.zero && 
            (Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2)))
        {
            thePlayer.useMouseController = true;
            thePlayer.usePS4Controller = false;
            thePlayer.useTouchController = false;
        }
        //detect mouse movement
        //TODO: change mouse pos to new input system
        if (moveJoystick.InputDirection == Vector3.zero && rotationJoystick.InputDirection == Vector3.zero && 
            (Input.GetAxisRaw("Mouse X") != 0.0f || Input.GetAxisRaw("Mouse Y") != 0.0f))
        {
            thePlayer.useMouseController = true;
            thePlayer.usePS4Controller = false;
            thePlayer.useTouchController = false;
        }

       

        if (moveJoystick.InputDirection != Vector3.zero || rotationJoystick.InputDirection != Vector3.zero)
        {
            thePlayer.useTouchController = true;
            thePlayer.useMouseController = false;
            thePlayer.usePS4Controller = false;
        }

        var dirRotLeft = thePlayer.controls.Player.Move.ReadValue<Vector2>();
        Vector3 playerDirectionMove = Vector3.right * dirRotLeft.x + Vector3.forward * dirRotLeft.y;

        //Get the current rotation from input and stores in a vector 3
        var dirRot = thePlayer.controls.Player.Rotation.ReadValue<Vector2>();
        Vector3 playerDirection = Vector3.right * dirRot.x + Vector3.forward * dirRot.y;
        //returns 1 if any rotation is being inputed from right joystick
        if (playerDirection.sqrMagnitude > 0.0f && moveJoystick.InputDirection == Vector3.zero
            && playerDirectionMove.sqrMagnitude > 0.0f && moveJoystick.InputDirection == Vector3.zero)
        { 
            thePlayer.usePS4Controller = true;
            thePlayer.useMouseController = false;
            thePlayer.useTouchController = false;
        }         
    }
}
