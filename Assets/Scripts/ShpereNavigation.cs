using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class ShpereNavigation : MonoBehaviour, IManipulationHandler
{
    [SerializeField] private Vector3 _startLocalPos = Vector3.zero;


    [SerializeField] private float _dragScale = 1.0f;
    [SerializeField] private float _dragSpeed = 10.0f;
    [SerializeField] private float _maxDragDistance = 1.0f;

    private bool _isBeingMoved = false;
    private Vector3 _moveVec = Vector3.zero;

    void Start ()
	{
	    _startLocalPos = transform.localPosition;
	}

    public void OnManipulationStarted(ManipulationEventData eventData)
    {
        _isBeingMoved = true;
        InputManager.Instance.PushModalInputHandler(gameObject);
    }

    public void OnManipulationUpdated(ManipulationEventData eventData)
    {
        _moveVec = eventData.CumulativeDelta;
        _moveVec.z = 0;
        Drag(_moveVec);
    }

    public void OnManipulationCompleted(ManipulationEventData eventData)
    {
        _isBeingMoved = false;
        transform.localPosition = _startLocalPos;
        InputManager.Instance.PopModalInputHandler();
    }

    public void OnManipulationCanceled(ManipulationEventData eventData)
    {
        _isBeingMoved = false;
        transform.localPosition = _startLocalPos;
        InputManager.Instance.PopModalInputHandler();
    }

    private void Drag(Vector3 position)
    {
        Vector3 targetPos = transform.position + position * _dragScale;
        if (Vector3.Distance(transform.position, targetPos) <= _maxDragDistance)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, _dragSpeed);
        }
    }

    public bool GetIsBeingMoved()
    {
        return _isBeingMoved;
    }

    public Vector3 GetMoveVector()
    {
        return _moveVec;
    }
}
