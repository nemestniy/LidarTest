﻿using System.Collections;
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
    private float _speedUpdate;

    [SerializeField]
    private Point _point;

    public List<Vector3> _directions;
    private RaycastHit _hit;
    private int _count;

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

    private void FixedUpdate()
    {
        foreach(Vector3 direction in _directions)
        {
            var currentDirection = transform.TransformDirection(direction);
            if (Physics.Raycast(transform.position, currentDirection, out _hit, _rayDistance))
            {
                if (_hit.transform.tag == "OpaqueObject")
                {
                    var point = Instantiate(_point, _hit.point, Quaternion.identity);
                }
            }
        }

        transform.Rotate(Vector3.up * _speedUpdate);

        /*var currentDirection = transform.TransformDirection(_directions[_count]);
        if (Physics.Raycast(transform.position, currentDirection, out _hit, _rayDistance))
        {
            if (_hit.transform.tag == "OpaqueObject")
            {
                var point = Instantiate(_point, _hit.point, Quaternion.identity);
            }
        }

        _count++;
        if (_count == _directions.Count)
        {
            transform.Rotate(Vector3.up * _speedUpdate * Time.deltaTime);
            _count = 0;
        }*/
    }
}
