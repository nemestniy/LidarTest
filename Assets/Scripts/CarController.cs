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

    [SerializeField]
    private Transform _steeringWheel;



    private Rigidbody _rigidbody;
    private float _torque;
    private float _steer;
    private float _handbreak;

    public static CarController Instance{get; private set;}

    private void Awake()
    {
        Instance = this;
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = new Vector3(0, -transform.localScale.y / 2, 0);
    }

    public int GetSpeed(){
        float speedL = _steeringWheels[0].GetWheelCollider().rpm;
        float speedR = _steeringWheels[0].GetWheelCollider().rpm;
        return (int)(speedL + speedR)/(-2);
    }

    private void Update()
    {
        _torque = Input.GetAxis("Vertical") * _speed;
        _steer = Input.GetAxis("Horizontal") * _maxAngle;
        _handbreak = Input.GetAxis("Jump") * _maxValueBreak;

        if(_steeringWheel != null)
            _steeringWheel.eulerAngles = new Vector3(_steeringWheel.eulerAngles.x, _steeringWheel.eulerAngles.y, _steer * 2);
    }

    public void OnButtonLeftClickDown(){
        _steer = -1*_maxAngle;
    }

    public void OnButtonRightClickDown(){
        _steer = _maxAngle;
    }

    public void OnButtonLeftClickUp(){
        _steer = 0;
    }

    public void OnButtonRightClickUp(){
        _steer = 0;
    }

    public void OnButtonUpClickDown(){
        _torque = _speed;
    }

    public void OnButtonDownClickDown(){
        if(_driveWheels[0].GetWheelCollider().rpm < 1 && _driveWheels[1].GetWheelCollider().rpm < 1)
            _torque = -1 * _speed;
        else    
            _handbreak = _maxValueBreak;
        
    }

    public void OnButtonUpClickUp(){
        _torque = 0;
    }

    public void OnButtonDownClickUp(){
        _handbreak = 0;
        _torque = 0;
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
                //_steeringWheels[i].Break(_handbreak);
            }
        }
    }
}
