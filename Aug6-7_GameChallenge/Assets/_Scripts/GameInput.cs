using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance;

    private PlayerInputActions inputActions;


    public event EventHandler MenuToggleAction;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;

        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();
    }

    private void Start()
    {
        //inputActions.Player.Movement.
        inputActions.Player.PauseMenuToggle.performed += PauseMenuToggle_performed;
        
    }

    private void PauseMenuToggle_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {

            MenuToggleAction?.Invoke(this, EventArgs.Empty);
        
    }


    public float GetMovementInput()
    {
        float ret;
        try
        { ret = inputActions.Player.Movement.ReadValue<float>(); }
        catch{
        ret = 0f;
        }

        return ret;
    }

    private void OnDestroy()
    {
        inputActions.Player.Disable();
    }
}
