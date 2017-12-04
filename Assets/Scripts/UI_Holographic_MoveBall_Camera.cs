using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Holographic_MoveBall_Camera : MonoBehaviour {
    [SerializeField] private float _lerpSpeed = 0.5f;
    private GameObject _movementBall = null;
    private ShpereNavigation _movementBallScriptNavigation = null;
    private GameObject _uiCenterPointMoveBall = null;
    [SerializeField] private float _maxBallDist = 0.9f;

    void Start()
    {
        _movementBall = ManagerObjects.Instance().GetObject(EnumObjects.MovementShereCamera);
        _uiCenterPointMoveBall = ManagerObjects.Instance().GetObject(EnumObjects.UICenterPointMoveBallCamera);
        _movementBallScriptNavigation = _movementBall.GetComponent<ShpereNavigation>();

    }

    void Update()
    {
        bool isMoving = _movementBallScriptNavigation.GetIsBeingMoved();

        if (isMoving)
        {
            Vector3 moveVec = _movementBallScriptNavigation.GetMoveVector();
            Drag(moveVec);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, _lerpSpeed);
        }
    }

    [SerializeField] private float _dragScale = 1.0f;
    [SerializeField] private float _dragSpeed = 5.0f;
    [SerializeField] private float _maxDragDistance = 10.0f;

    private void Drag(Vector3 position)
    {
        Vector3 targetPos = transform.position + position * _dragScale;
        if (Vector3.Distance(transform.position, targetPos) <= _maxDragDistance)
        {
            Vector3 lerpTarget = Vector3.Lerp(transform.position, targetPos, _dragSpeed);
            if (Vector3.Distance(_uiCenterPointMoveBall.transform.position, lerpTarget) <= _maxBallDist)
            {
                transform.position = lerpTarget;
            }
        }
    }
}
