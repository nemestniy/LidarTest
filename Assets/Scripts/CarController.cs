using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField]
    private Wheel[] _driveWheels;

    [SerializeField]
    private Wheel[] _steeringWheels;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _maxAngle;

    [SerializeField]
    private float _maxValueBreak;



    private Rigidbody _rigidbody;
    private float _torque;
    private float _steer;
    private float _handbreak;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = new Vector3(0, -transform.localScale.y / 2, 0);
    }

    private void Update()
    {
        _torque = Input.GetAxis("Vertical") * _speed;
        _steer = Input.GetAxis("Horizontal") * _maxAngle;
        _handbreak = Input.GetAxis("Jump") * _maxValueBreak;
    }

    private void FixedUpdate()
    {
        for(int i = 0; i < _driveWheels.Length && i < _steeringWheels.Length; i++)
        {
            _driveWheels[i].SetWorldPose();
            _steeringWheels[i].SetWorldPose();
        }

        foreach (Wheel wheel in _driveWheels)
            wheel.Move(_torque);

        foreach (Wheel wheel in _steeringWheels)
        {
            wheel.Steer(_steer);
        }

        if(_handbreak > 0f)
        {
            for (int i = 0; i < _driveWheels.Length && i < _steeringWheels.Length; i++)
            {
                _driveWheels[i].Break(_handbreak);
                //_steeringWheels[i].Break(handbreak);
            }
        }
    }
}
