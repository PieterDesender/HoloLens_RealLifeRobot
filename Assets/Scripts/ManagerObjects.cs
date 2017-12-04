using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnumObjects
{
    MovementShere,
    controlMove,
    controlCamera,
    UIHolographicMove,
    UIHolographicCamera,
    UICenterPointMoveBall,
    UITxtSpeed,
    UICamPlaneBig,
    UICamPlaneSmall,
    UICenterPointMoveBallCamera,
    MovementShereCamera
}

public class ManagerObjects : MySingleton<ManagerObjects>
{


    List<KeyValuePair<EnumObjects, GameObject>> _listObjects;

    [SerializeField] private GameObject _movementSphere = null;
    [SerializeField] private GameObject _controlMove = null;
    [SerializeField] private GameObject _controllCamera = null;
    [SerializeField] private GameObject _uiHolographicMove = null;
    [SerializeField] private GameObject _uiHolographicCamera = null;
    [SerializeField] private GameObject _uiCenterPointMoveBall = null;
    [SerializeField] private GameObject _uiTxtSpeed = null;
    [SerializeField] private GameObject _uiCamTexPlaneBig = null;
    [SerializeField] private GameObject _uiCamTexPlaneSmall = null;
    [SerializeField] private GameObject _uiCenterPointMoveBallCamera = null;
    [SerializeField] private GameObject _movementSphereCamera = null;

    // Use this for initialization
    void Awake() {
        _listObjects = new List<KeyValuePair<EnumObjects, GameObject>>();

        AddObjectToDictionary(EnumObjects.MovementShere,_movementSphere);
        AddObjectToDictionary(EnumObjects.controlMove, _controlMove);
        AddObjectToDictionary(EnumObjects.controlCamera, _controllCamera);
        AddObjectToDictionary(EnumObjects.UIHolographicMove, _uiHolographicMove);
        AddObjectToDictionary(EnumObjects.UIHolographicCamera, _uiHolographicCamera);
        AddObjectToDictionary(EnumObjects.UICenterPointMoveBall, _uiCenterPointMoveBall);
        AddObjectToDictionary(EnumObjects.UITxtSpeed, _uiTxtSpeed);
        AddObjectToDictionary(EnumObjects.UICamPlaneBig, _uiCamTexPlaneBig);
        AddObjectToDictionary(EnumObjects.UICamPlaneSmall, _uiCamTexPlaneSmall);
        AddObjectToDictionary(EnumObjects.UICenterPointMoveBallCamera, _uiCenterPointMoveBallCamera);
        AddObjectToDictionary(EnumObjects.MovementShereCamera, _movementSphereCamera);
    }

    private void AddObjectToDictionary(EnumObjects type, GameObject obj)
    {
        if (obj == null)
            Debug.LogError(type.ToString() + " is not set in manager");
        else
            _listObjects.Add(new KeyValuePair<EnumObjects, GameObject>(type,obj));
    }
    public GameObject GetObject(EnumObjects type)
    {
        return _listObjects[(int)type].Value;
    }
}
