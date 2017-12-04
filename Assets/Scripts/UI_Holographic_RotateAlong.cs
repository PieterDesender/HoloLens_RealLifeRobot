using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Holographic_RotateAlong : MonoBehaviour {
    Camera _mainCamera;

    void Start()
    {
        _mainCamera = Camera.main;
    }
    void Update()
    {
        gameObject.transform.rotation = new Quaternion(_mainCamera.transform.rotation.x, _mainCamera.transform.rotation.y, _mainCamera.transform.rotation.z, _mainCamera.transform.rotation.w);
    }
}
