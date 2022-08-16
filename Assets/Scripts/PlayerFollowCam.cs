using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowCam : MonoBehaviour
{

    private Transform toFollow;

    [SerializeField] private TransformInterpolator _transformInterpolater;
    private float sensivity;


    [SerializeField] private float cameraDistanceSensivity;
    private Vector3 _CameraAngle;

    private void Start()
    {
        InvokeRepeating(nameof(FindPlayer), 0.2f, 0.2f);
    }

    private void FindPlayer()
    {
        toFollow = FindObjectOfType<PlayerJoystickMovement>()?.transform;

        if (!toFollow)
            return;

        SetCameraAngleVector();
        GameStateHandler.GameState = GameStateHandler.State.InLoop;
        CancelInvoke(nameof(FindPlayer));
    }

    private void SetCameraAngleVector()
    {
        _CameraAngle = Camera.main.transform.position.normalized;
        _CameraAngle.x = 0f;
    }

    private void LateUpdate()
    {
        if (!toFollow)
            return;

        var targetPos = toFollow.position + (_CameraAngle * cameraDistanceSensivity);

        transform.position = Vector3.Lerp(_transformInterpolater.oldVector, targetPos,
           _transformInterpolater.vectorLerpCoefficient);

        _transformInterpolater.oldVector = transform.position;
    }
}