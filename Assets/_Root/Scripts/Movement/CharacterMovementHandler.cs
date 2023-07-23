using System;
using Fusion;
using UnityEngine;

public class CharacterMovementHandler : NetworkBehaviour
{
    private NetworkCharacterController _networkController;
    private Camera _localCamera;
    
    private Vector2 _viewInput;

    private float _cameraRotationX = 0;

    private void Awake()
    {
        _networkController = GetComponent<NetworkCharacterController>();
        _localCamera = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        _cameraRotationX += _viewInput.y * Time.deltaTime * _networkController.viewUpDownRotationSpeed;
        _cameraRotationX = Mathf.Clamp(_cameraRotationX, -90f, 90f);
        
        _localCamera.transform.localRotation = Quaternion.Euler(_cameraRotationX, 0, 0);
    }

    public override void FixedUpdateNetwork()
    {
        if (GetInput(out NetworkInputData networkInputData))
        {
            _networkController.Rotate(networkInputData.RotationInput);
            
            Vector3 moveDirection = transform.forward * networkInputData.MovementInput.y + 
                                    transform.right * networkInputData.MovementInput.x;
            _networkController.Move(moveDirection);
            
            if(networkInputData.IsJumpButtonPressed)
                _networkController.Jump();
        }
    }

    public void SetViewInputVector(Vector2 viewInput)
    {
        _viewInput = viewInput;
    }
}
