using UnityEngine;

/// <summary>
/// Configuration data for camera movement, zoom, and positioning.
/// </summary>
[CreateAssetMenu(fileName = "CameraConfig", menuName = "Game/Config/Camera Config")]
public class CameraConfig : BaseConfig
{
    [Header("Movement Settings")]
    [SerializeField] private float minZoomMoveTime = 0.5f;
    [SerializeField] private float maxZoomMoveTime = 1.5f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private int mouseEdgeMoveRangeRatio = 10;
    
    [Header("Position Limits")]
    [SerializeField] private float maxXPos = 50f;
    [SerializeField] private float minXPos = -50f;
    [SerializeField] private float maxZPos = 50f;
    [SerializeField] private float minZPos = -50f;
    
    [Header("Zoom Settings")]
    [SerializeField] private float targetFieldOfView = 60f;
    [SerializeField] private float fieldOfViewMax = 90f;
    [SerializeField] private float fieldOfViewMin = 30f;
    [SerializeField] private float startingZoomYOffset = 10f;
    [SerializeField] private float endingZoomXOffset = 5f;

    // Public properties for backward compatibility
    public float MinZoomMoveTime => minZoomMoveTime;
    public float MaxZoomMoveTime => maxZoomMoveTime;
    public float RotationSpeed => rotationSpeed;
    public int MouseEdgeMoveRangeRatio => mouseEdgeMoveRangeRatio;
    public float MaxXPos => maxXPos;
    public float MinXPos => minXPos;
    public float MaxZPos => maxZPos;
    public float MinZPos => minZPos;
    public float TargetFieldOfView
    {
        get => targetFieldOfView;
        set => targetFieldOfView = value;
    }

    public float FieldOfViewMax => fieldOfViewMax;
    public float FieldOfViewMin => fieldOfViewMin;
    public float StartingZoomYOffset => startingZoomYOffset;
    public float EndingZoomXOffset => endingZoomXOffset;

    protected override string ConfigFileName => "CameraConfig";
} 