using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class PlayerInputController : MonoBehaviour, PlayerControlls.IPlayerMovementActions
{
    private PlayerControlls.PlayerMovementActions actions;
    //refrence to the action map we are implementing
    private Rigidbody _rigidBody;
    //refrence to the action map we are implementing


    [SerializeField]
    private float _verticalForce;
    //scroll speed of the mouse wheel
    
    private float targetH = 0f;
    private float targetV = 0f;
    private float tempAngleH;
    private float tempAngleV;
    private bool input;

    private float maxHorizontalTurn = 45;
    private float maxVerticalTurn = 45;
    private float rollSpeed = 20;
    private float pitchSpeed = 20;

    // Start is called before the first frame update
    void Start()
    {
        //Get a refrence to our rigidbody
        _rigidBody = GetComponent<Rigidbody>();

        //Get a reference to our action map
        actions = new PlayerControlls().PlayerMovement;
        //Enable our actions
        actions.Enable();
        //Set callbacks to those actions (automatically handled since we derive from IWristMenuActions)
        actions.SetCallbacks(this);

    }

    void FixedUpdate()
    {
        float threshold = 0.0005f;

        //Get angle between current and targetH
        float angleDelta = targetH-tempAngleH;

        //if we are outside a threshold
        if (Mathf.Abs(angleDelta) > threshold)
        {
            //Use angle to determine direction to turn
            float direction = 1 * Mathf.Sign(angleDelta);

            //Turn towards angle at given speed
            tempAngleH += Time.deltaTime * rollSpeed * direction;
        }
        //else, set angle to targetH
        else
        {
            tempAngleH = targetH;
        }

        angleDelta = targetV - tempAngleV;

        //if we are outside a threshold
        if (Mathf.Abs(angleDelta) > threshold)
        {
            //Use angle to determine direction to turn
            float direction = 1 * Mathf.Sign(angleDelta);

            //Turn towards angle at given speed
            tempAngleV += Time.deltaTime * pitchSpeed * direction;
        }
        //else, set angle to targetH
        else
        {
            tempAngleV = targetV;
        }

        transform.rotation = Quaternion.Euler(tempAngleV, 0, tempAngleH);
    }



    public void OnRight(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _rigidBody.AddForce(new Vector2(_verticalForce, 0), ForceMode.Force);
            input = true;
            targetH -= maxHorizontalTurn;

        }
        else if (context.canceled)
        {
            //targetH += maxHorizontalTurn;
            input = false;
        }

    }

    public void OnLeft(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _rigidBody.AddForce(new Vector2(-_verticalForce, 0), ForceMode.Force);
            input = true;
            targetH += maxHorizontalTurn;

        }
        else if (context.canceled)
        {
            targetH -= maxHorizontalTurn;
            input = false;
        }
    }

    public void OnUp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _rigidBody.AddForce(new Vector2(0, _verticalForce), ForceMode.Force);
            targetV -= maxVerticalTurn;
            Debug.Log(targetV);
        }
        else if (context.canceled)
        {
            //targetV += maxVerticalTurn;
            input = false;
        }
    }
}
