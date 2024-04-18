using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour, PlayerControlls.IPlayerMovementActions
{
    private PlayerControlls.PlayerMovementActions actions;
    //refrence to the action map we are implementing
    private Rigidbody _rigidBody;
    //refrence to the action map we are implementing
    private Animator _animator;
    //refrence to the action map we are implementing

    [SerializeField]
    private float _speedModifier;
    //modify how much the player inputs are being multiplied by

    private bool _rightPressed;
    private bool _leftPressed;


    private float _verticalForce;
    //strength of vertical direction influence
    private float _horizontalForce;
    //strength of horizontal direction influence

    //animator values
    private float _horizMovement;
    private float _vertMovement;
    private float _dampTime;


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
        //Store user input as a movement vector
        Vector3 m_Input = new Vector3(_horizontalForce * _speedModifier, _verticalForce * _speedModifier, 0);

        //Apply the movement vector to the current position, which is
        //multiplied by deltaTime and speed for a smooth MovePosition
        _rigidBody.MovePosition(transform.position + m_Input * Time.deltaTime);

        //set animator values to match movement values
        _animator.SetFloat("HorizMovement", _horizMovement, _dampTime, Time.fixedDeltaTime);
        _animator.SetFloat("VertMovement", _vertMovement, _dampTime, Time.fixedDeltaTime);

        //slowly reset vertical momentum
        if (_verticalForce > -2) { _verticalForce -= 0.2f; }
        if (_verticalForce < -2) { _verticalForce += 0.2f; }


    }

    //on right input in action map
    public void OnRight(InputAction.CallbackContext context)
    {
        //when the key is pressed
        if (context.performed)
        {
            //set the key as pressed
            _rightPressed = true;
            //check if the other direction is pressed
            if (_leftPressed) 
            {
                //if it is, zero out the movement
                Neutral();
                return; 
            }
            //if not, move desired direction
            MoveRight();

        }
        //when the key is released
        else if (context.canceled)
        {
            //set the key as released
            _rightPressed = false;
            //check if the other direction is pressed
            if (_leftPressed)
            {
                //if it is, move desired direction
                MoveLeft();
                return;
            }
            //if not, zero out the movement
            Neutral();
        }

    }

    //on left input in action map
    public void OnLeft(InputAction.CallbackContext context)
    {
        //when the key is pressed
        if (context.performed)
        {
            //set the key as pressed
            _leftPressed = true;
            //check if the other direction is pressed
            if (_rightPressed)
            {
                //if it is, zero out the movement
                Neutral();
                return;
            }
            //if not, move desired direction
            MoveLeft();
        }
        //when the key is released
        else if (context.canceled)
        {
            //set the key as released
            _leftPressed = false;
            //check if the other direction is pressed
            if (_rightPressed)
            {
                //if it is, move desired direction
                MoveRight();
                return;
            }
            //if not, zero out the movement
            Neutral();
        }
    }

    //on up input in action map
    public void OnUp(InputAction.CallbackContext context)
    {
        //when the scroll is detected
        if (context.performed)
        {
            //if scroll is positive
            if(context.ReadValue<float>() > 0)
            {
                //add vertical force up, and set animation values for up
                _verticalForce = 3;
                _vertMovement = 10;
                _dampTime = 0.01f;
            }
            //if scroll is negitive
            else if (context.ReadValue<float>() < 0)
            {
                //add vertical force down, and set animation values for down
                _verticalForce = -3;
                _vertMovement = -10;
                _dampTime = 0.01f;
            }            
        }
        //when the scroll stops
        else if (context.canceled)
        {
            _vertMovement = 0;
            _dampTime = 0.2f;
        }
    }

    //move left functionality
    private void MoveLeft()
    {
        _horizontalForce = -2;
        _horizMovement = -2;
        _dampTime = 0.2f;
    }
    //move right functionality
    private void MoveRight()
    {
        _horizontalForce = 2;
        _horizMovement = 2;
        _dampTime = 0.2f;
    }
    //neutral functionality
    private void Neutral()
    {
        _horizontalForce = 0;
        _horizMovement = 0;
        _dampTime = 0.05f;
    }
}
