using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameInput : MonoBehaviour
{    public event EventHandler OnInteractAction;
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
       playerInputActions = new PlayerInputActions();
       playerInputActions.Caracter.Enable();
       playerInputActions.Caracter.Interact.performed += Interact_performed;
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
