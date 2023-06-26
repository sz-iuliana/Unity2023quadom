using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public event EventHandler OnPauseAction;
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    private PlayerInputActions playerInputActions;
    private PlayerInputActions pause;

    private void Awake()
    {
        Instance = this;
       playerInputActions = new PlayerInputActions();
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
}
