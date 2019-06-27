using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform _targetPosition;

    [SerializeField]
    private Transform _trackingObject;

    [SerializeField]
    private float _speedTracking;

    [SerializeField]
    private Camera _lidarCamera;

    private void Awake()
    {
        if(_targetPosition != null) 
            transform.position = _targetPosition.position;
        _lidarCamera.rect = new Rect(Camera.main.rect.xMax / 1.5f, Camera.main.rect.yMax / 1.5f, Camera.main.rect.xMax, Camera.main.rect.yMax);
    }

    private void Update()
    {
        if(_targetPosition != null)
            transform.position = Vector3.Lerp(transform.position, _targetPosition.position, _speedTracking * Time.deltaTime);
        if(_trackingObject != null)
            transform.LookAt(_trackingObject);
    }
}
