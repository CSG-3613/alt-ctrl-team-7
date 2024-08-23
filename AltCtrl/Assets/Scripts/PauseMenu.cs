using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PauseMenu : MonoBehaviour, PlayerControlls.IPauseMenuActions
{
    [SerializeField] GameObject pauseUI;

    private PlayerControlls.PauseMenuActions actions;

    private float defaultTimeScale;

    public void Start()
    {
        //Get a reference to our action map
        actions = new PlayerControlls().PauseMenu;
        //Enable our actions
        actions.Enable();
        //Set callbacks to those actions (automatically handled since we derive from IPauseMenuActions)
        actions.SetCallbacks(this);
        defaultTimeScale = Time.timeScale;
    }
    public void OnTogglePause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            pauseUI.SetActive(!pauseUI.activeSelf);
            if (pauseUI.activeSelf) { Time.timeScale = 0; }
            else { Time.timeScale = defaultTimeScale; }
        }
        
    }

}