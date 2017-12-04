using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Holographic_MoveAlong : MonoBehaviour {
    Camera _mainCamera;
    [SerializeField] private float _distance = 0.1f;

    void Start()
    {
        _mainCamera = Camera.main;
        _distance = Vector3.Distance(Vector3.zero, transform.position);
    }
	void Update () {
	    gameObject.transform.position = _mainCamera.transform.position + _mainCamera.transform.forward * _distance;
	}
}
