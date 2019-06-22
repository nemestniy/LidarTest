using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WheelCollider))]
public class Wheel : MonoBehaviour
{
    [SerializeField]
    private GameObject _selfMesh;

    private WheelCollider _wheelCollider;

    private void Awake()
    {
        _wheelCollider = GetComponent<WheelCollider>();
    }

    public void Move(float value)
    {
        _wheelCollider.motorTorque = -value;
        _wheelCollider.brakeTorque = 0;
    }

    public void Break(float value)
    {
        _wheelCollider.brakeTorque = value;
    }

    public void Steer(float value)
    {
        _wheelCollider.steerAngle = value;
    }

    public WheelCollider GetWheelCollider()
    {
        return _wheelCollider;
    }

    public void SetWorldPose()
    {
        Quaternion quaternion;
        Vector3 position;
        _wheelCollider.GetWorldPose(out position, out quaternion);
        _selfMesh.transform.position = position;
        _selfMesh.transform.rotation = quaternion;
    }
}
