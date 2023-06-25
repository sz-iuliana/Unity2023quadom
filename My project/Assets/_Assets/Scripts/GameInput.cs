using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;


public class GameInput : MonoBehaviour
{
    private const string PLAYER_PREFS_BINDINGS = "InputBindings";

    public static GameInput Instance { get; private set; }
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;
    private PlayerInputActions playerInputActions;

    public enum Binding
    {
            Move_Up,
            Move_Down,
            Move_Left,
            Move_Right,
            Interact,
            InteractAlternate,
            Pause,



    }

    private void Awake()
    { 
        Instance = this;
        playerInputActions = new PlayerInputActions();

        if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
        {
            playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
        }


        playerInputActions.Caracter.Enable();

        playerInputActions.Caracter.Interact.performed += Interact_performed;
        playerInputActions.Caracter.InteractAlternate.performed += InteractAlternate_performed;
        playerInputActions.Caracter.Pause.performed += Pause_performed;


      

    }
    private void OnDestroy()
    {
        playerInputActions.Caracter.Interact.performed -= Interact_performed;
        playerInputActions.Caracter.InteractAlternate.performed -= InteractAlternate_performed;
        playerInputActions.Caracter.Pause.performed -= Pause_performed;

        playerInputActions.Dispose();
       
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);

    }
    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        
        
            OnInteractAction?.Invoke(this, EventArgs.Empty);
        
    }



    public Vector2 GetMovementVectorNormalized()
    {


        Vector2 inputVector = playerInputActions.Caracter.Move.ReadValue<Vector2>();

       inputVector = inputVector.normalized;
       // Debug.Log(inputVector);

        return inputVector;
    }

    public string GetBindingText(Binding binding)
    {
        switch (binding)
        {
            default:
            case Binding.Move_Up:
                return playerInputActions.Caracter.Move.bindings[1].ToDisplayString();
            case Binding.Move_Down:
                return playerInputActions.Caracter.Move.bindings[2].ToDisplayString();
            case Binding.Move_Left:
                return playerInputActions.Caracter.Move.bindings[3].ToDisplayString();
            case Binding.Move_Right:
                return playerInputActions.Caracter.Move.bindings[4].ToDisplayString();
            case Binding.Interact:
                return playerInputActions.Caracter.Interact.bindings[0].ToDisplayString();
            case Binding.InteractAlternate:
                return playerInputActions.Caracter.InteractAlternate.bindings[0].ToDisplayString();
            case Binding.Pause:
                return playerInputActions.Caracter.Pause.bindings[0].ToDisplayString();
        }
    }

    public void RebindBinding(Binding binding,Action onActionRebound )
    {
        playerInputActions.Caracter.Disable();
        InputAction inputAction;
        int bindigIndex;
        switch (binding)
        {
            default:
            case Binding.Move_Up:
                inputAction=playerInputActions.Caracter.Move;
                bindigIndex = 1;
                break;
            case Binding.Move_Down:
                inputAction = playerInputActions.Caracter.Move;
                bindigIndex = 2;
                break;
            case Binding.Move_Left:
                inputAction = playerInputActions.Caracter.Move;
                bindigIndex = 3;
                break;
            case Binding.Move_Right:
                inputAction = playerInputActions.Caracter.Move;
                bindigIndex = 4;
                break;

            case Binding.Interact:
                inputAction = playerInputActions.Caracter.Interact;
                bindigIndex = 0;
                break;
            case Binding.InteractAlternate:
                inputAction = playerInputActions.Caracter.InteractAlternate;
                bindigIndex = 0;
                break;
            case Binding.Pause:
                inputAction = playerInputActions.Caracter.Pause;
                bindigIndex = 0;
                break;


        }

        inputAction.PerformInteractiveRebinding(bindigIndex)
            .OnComplete(callback =>
            {
                callback.Dispose();
                playerInputActions.Caracter.Enable();
                onActionRebound();

                PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, playerInputActions.SaveBindingOverridesAsJson());
                PlayerPrefs.Save();
            })
        .Start();

    }
}
