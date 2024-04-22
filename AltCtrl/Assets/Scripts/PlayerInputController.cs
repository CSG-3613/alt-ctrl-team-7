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
    private Animator _animator;
    //refrence to the action map we are implementing


    [SerializeField]
    private float _verticalForce;
    //scroll speed of the mouse wheel
    [SerializeField]
    private float _horizontalForce;
    //scroll speed of the mouse wheel

    private float _tempHoriz;
    private float _tempVert;
    private float _dampTime;

    private bool _leftPressed;
    private bool _rightPressed;

    // Start is called before the first frame update
    void Start()
    {
        //Get a refrence to our rigidbody
        _rigidBody = GetComponent<Rigidbody>();

        //Get a refrence to our rigidbody
        _animator = GetComponent<Animator>();

        //Get a reference to our action map
        actions = new PlayerControlls().PlayerMovement;
        //Enable our actions
        actions.Enable();
        //Set callbacks to those actions (automatically handled since we derive from IWristMenuActions)
        actions.SetCallbacks(this);

    }

    public void FixedUpdate()
    {
        _animator.SetFloat("HorizMovement", _tempHoriz, _dampTime, Time.fixedDeltaTime);
        _animator.SetFloat("VertMovement", _tempVert, _dampTime, Time.fixedDeltaTime);
    }

    public void OnRight(InputAction.CallbackContext context)
    {
        //Debug.Log("Right");
        if (context.performed)
        {
            if (_leftPressed) 
            {
                _tempHoriz = 0; 
                _rigidBody.velocity = new Vector3(0, _rigidBody.velocity.y, 0);
                return; 
            }
            _rightPressed = true;
            _rigidBody.AddForce(new Vector2(_horizontalForce, 0), ForceMode.Force);
            _tempHoriz = 2;
            _dampTime = 0.2f;
        }
        else if (context.canceled)
        {
            _rightPressed = false;
            _rigidBody.AddForce(new Vector2(-_horizontalForce, 0), ForceMode.Force);
            _tempHoriz = 0;
            _dampTime = 0.05f;
        }

    }

    public void OnLeft(InputAction.CallbackContext context)
    {
        //Debug.Log("Left");
        if (context.performed)
        {
            
            if (_rightPressed)
            {
                _tempHoriz = 0;
                _rigidBody.velocity = new Vector3(0, _rigidBody.velocity.y, 0);
                return;
            }
            _leftPressed = true;
            _rigidBody.AddForce(new Vector2(-_horizontalForce, 0), ForceMode.Force);
            _tempHoriz = -2;
            _dampTime = 0.2f;
        }
        else if (context.canceled)
        {
            _leftPressed = false;
            _rigidBody.AddForce(new Vector2(_horizontalForce, 0), ForceMode.Force);
            _tempHoriz = 0;
            _dampTime = 0.05f;
        }
    }

    public void OnUp(InputAction.CallbackContext context)
    {
        //Debug.Log("Up");
        if (context.performed)
        {

            _rigidBody.AddForce(new Vector2(0, _verticalForce), ForceMode.Force);
            _tempVert = 10;
            _dampTime = 0.05f;
        }
        else if (context.canceled)
        {
            _tempVert = 0;
            _dampTime = 0.2f;
        }
    }
}
