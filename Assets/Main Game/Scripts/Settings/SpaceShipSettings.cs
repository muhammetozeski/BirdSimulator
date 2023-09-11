using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Rendering;
using UnityEngine;

//TODO: ADD minimum fly speed

[CreateAssetMenu(fileName = "Space Ship Settings Data", menuName = "Single Scriptable Objects/Game Settings Data/Player Settings/Space Ship Settings Data")]
public class SpaceShipSettings : ScriptableObject
{
    [Header("Ship Speed Settings")]

    [Tooltip("Moving Speed of Player's ship for \"forward\" and \"back\"")]
    [SerializeField] private float _SpeedForward;
    public float SpeedForward { get { return _SpeedForward; } private set { _SpeedForward = value; } }

    [Tooltip("Moving Speed of Player's ship for \"up\" - \"down\" and \"right\" - \"left\"")]
    [SerializeField] private float _Speed2d;
    public float Speed2d { get { return _Speed2d; } private set { _Speed2d = value; } }
    
    [Tooltip("Maximum speed of Player's ship for up-down and left-right")]
    [Min(1)]
    [SerializeField] private float _Max2DSpeed;
    public float Max2DSpeed { get { return _Max2DSpeed; } private set { _Max2DSpeed = MainTools.Min(1, value); } }

    [Tooltip("Maximum speed of Player's ship for forward-back")]
    [Min(1)]
    [SerializeField] private float _Max3DSpeed;
    public float Max3DSpeed { get { return _Max3DSpeed; } private set { _Max3DSpeed = MainTools.Min(1, value); } }


    ////////////////////////////////////////////////////////////////////////////////////////
    [Header("Ship Rotation Settings")]

    [Tooltip("Z axis speed of rotation of Player's ship")]
    [SerializeField] private float _ShipZAxisRotationClamp;
    public float ShipZAxisRotationClamp { get { return _ShipZAxisRotationClamp; } private set { _ShipZAxisRotationClamp = value; } }

    [Tooltip("X axis speed of rotation of Player's ship")]
    [SerializeField] private float _ShipXAxisRotationClamp;
    public float ShipXAxisRotationClamp { get { return _ShipXAxisRotationClamp; } private set { _ShipXAxisRotationClamp = value; } }

    [Tooltip("Angular speed in y axis of the player bird")]
    [SerializeField] private float _YAngularSpeed = 1f;
    public float YAngularSpeed { get { return _YAngularSpeed; } private set { _YAngularSpeed = value; } }

    [Tooltip("Angular Lerping Speed of Player's ship")]
    public float ShipAngularSpeed = 1f;
    
    ////////////////////////////////////////////////////////////////////////////////////////
    [Header("Target Speed Settings")]

    [Tooltip("Speed of The Target icon that is on the canvas, it effects angular speed of the player's ship")]
    public float ShipRotationMultiplier = 10f;

    [Tooltip("Circle border area for target icon")]
    [SerializeField] private float _TargetClamp = 100f;
    public float TargetClamp { get { return _TargetClamp; } private set { _TargetClamp = value; } }

    [Tooltip("Lerp rate for target icon (keep it big)")]
    [Min(2)]
    [SerializeField] private float _TargetIconLerper = 10f;
    public float TargetIconLerper { get { return _TargetIconLerper; } private set { _TargetIconLerper = value < 2 ? 2 : value; } }

    [Tooltip("Target icon don't affect ship rotation if its scale smaller than TargetDeadZone. \nTargetDeadZone * 100 = real magnitude scale. for example if you make TargetDeadZone = 0,5 target icon don't affect untill it comes to (0,50) or (50,0) etc")]
    [SerializeField] private float _TargetDeadZone = 10f;
    public float TargetDeadZone { get { return _TargetDeadZone; } private set { _TargetDeadZone = value; } }

    [Space]
    [Tooltip("TODO: move this to \"general settings\" ")]
    public float MouseSpeedMultiplier = 10f;
}
