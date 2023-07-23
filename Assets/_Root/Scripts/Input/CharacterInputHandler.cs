using System;
using UnityEngine;

public class CharacterInputHandler : MonoBehaviour
{
    private Vector2 _moveInputVector = Vector2.zero;
    private Vector2 _viewInputVector = Vector2.zero;

    private CharacterMovementHandler _characterMovementHandler;
    private bool _isJumpButtonPressed;

    private void Awake()
    {
        _characterMovementHandler = GetComponent<CharacterMovementHandler>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        _characterMovementHandler.SetViewInputVector(_viewInputVector);
        
        _moveInputVector.x = Input.GetAxis("Horizontal");
        _moveInputVector.y = Input.GetAxis("Vertical");
        
        _viewInputVector.x = Input.GetAxis("Mouse X");
        _viewInputVector.y = Input.GetAxis("Mouse Y") * -1;
        
        _isJumpButtonPressed = Input.GetButtonDown("Jump");
    }

    public NetworkInputData GetNetworkInput()
    {
        NetworkInputData networkInputData = new NetworkInputData
        {
            MovementInput = _moveInputVector,
            RotationInput = _viewInputVector.x,
            IsJumpButtonPressed = _isJumpButtonPressed
        };
        return networkInputData;
    }
}
