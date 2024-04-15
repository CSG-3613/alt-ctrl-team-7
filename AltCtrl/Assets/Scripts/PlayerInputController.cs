using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour, PlayerControlls.IPlayerMovementActions
{
    private PlayerControlls.PlayerMovementActions actions;
    //refrence to the action map we are implementing
    private float _scrollSpeed;
    //scroll speed of the mouse wheel

    // Start is called before the first frame update
    void Start()
    {
        //Get a reference to our action map
        actions = new PlayerControlls().PlayerMovement;
        //Enable our actions
        actions.Enable();
        //Set callbacks to those actions (automatically handled since we derive from IWristMenuActions)
        actions.SetCallbacks(this);

    }

// Update is called once per frame
void Update()
    {
        Debug.Log("Speed: " + (_scrollSpeed / Time.deltaTime));
    }

    public void OnLeft(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Left");
        }

    }

    public void OnRight(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Right");
        }
    }

    public void OnUp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _scrollSpeed += (context.ReadValue<float>() / 120);
            Debug.Log("scroll");
            
        }
    }
}
