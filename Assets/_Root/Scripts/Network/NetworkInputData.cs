using Fusion;
using UnityEngine;

public struct NetworkInputData : INetworkInput
{
    public Vector2 MovementInput;
    public float RotationInput;
    public NetworkBool IsJumpButtonPressed;
}