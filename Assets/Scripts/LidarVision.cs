using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LidarVision : MonoBehaviour
{
    [Range(0, 45)]
    [SerializeField]
    private int _accuracy;

    [SerializeField]
    private float _rayDistance;

    [SerializeField]
    private float _periodImpulse;

    [SerializeField]
    private float _periodUpdate;
    
    [SerializeField]
    private float _deltaAngle;

    [SerializeField]
    private Point _point;

    public List<Vector3> _directions;
    private RaycastHit _hit;
    private int _count = 0;
    private float _timerImpulse = 0f;
    private float _timerUpdate = 0f;

    private void OnValidate()
    {
        _directions = new List<Vector3>();

        float angle = -30;
        while(angle < 60)
        {
            var direction = new Vector3(0, Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));
            _directions.Add(direction);
            angle += 60 / _accuracy;
        }


    }

    private void Rotate(float angle)
    {
        transform.Rotate(Vector3.up * angle);
    }

    private void Rotate()
    {
        transform.Rotate(Vector3.up);
    }

    private void Impulse(Vector3 direction,float distance)
    {
        if(Physics.Raycast(transform.position, direction, out _hit, distance))
        {
            if(_hit.transform.tag == "OpaqueObject")
            {
                Instantiate(_point, _hit.point, Quaternion.identity);
            }
        }
    }

    private void FixedUpdate()
    {
        _timerImpulse += Time.fixedDeltaTime;
        _timerUpdate += Time.fixedDeltaTime;

        if(_count >= _directions.Count)
            _count = 0;

        if(_timerImpulse >= _periodImpulse)
        {
            var currentDirection = transform.TransformDirection(_directions[_count]);
            Impulse(currentDirection, _rayDistance);
            _timerImpulse = 0;
        }

        
        if(_timerUpdate >= _periodUpdate)
        {
            Rotate(_deltaAngle);
            _timerUpdate = 0;
        }

        _count++;

        /*var currentDirection = transform.TransformDirection(_directions[_count]);
        if (Physics.Raycast(transform.position, currentDirection, out _hit, _rayDistance))
        {
            if (_hit.transform.tag == "OpaqueObject")
            {
                var point = Instantiate(_point, _hit.point, Quaternion.identity);
            }
        }*/
    }
}
