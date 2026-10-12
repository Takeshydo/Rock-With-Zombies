using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : MonoBehaviour
{
    //---------VARIABLES DES ACTION----------//
    private InputAction _moveAction;
    private InputAction _lookAction;
    private InputAction _runAction;

    //---------VARIABLES DES DONNEES DES INPUTS-----------//
    public Vector2 Move => _moveAction.ReadValue<Vector2>();
    public Vector2 Look => _lookAction.ReadValue<Vector2>();
    public bool runActionPressed => _runAction.IsPressed();
    

    //---------ATTRIBUTION DES VARIABLES D'ACTION AU NOM DE L'ACTION-------------//
    private void Awake()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
        _lookAction = InputSystem.actions.FindAction("Look");
        _runAction = InputSystem.actions.FindAction("Run");
    }

}
