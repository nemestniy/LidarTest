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

    private void Awake()
    {
        transform.position = _targetPosition.position;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _targetPosition.position, _speedTracking * Time.deltaTime);
        transform.LookAt(_trackingObject);
    }
}
