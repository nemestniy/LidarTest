using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LidarVision : MonoBehaviour
{
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

    private void Start()
    {
        _directions = new List<Vector3>();

        float angle = 0;
        while(angle < 60)
        {
            var direction = new Vector3(0, Mathf.Sin(angle * Mathf.Deg2Rad), Mathf.Cos(angle * Mathf.Deg2Rad));
            _directions.Add(direction);
            angle += 60 / _accuracy;
        }
    }

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * _speedUpdate);

        foreach(Vector3 direction in _directions)
        {
            var currentDirection = transform.TransformDirection(direction);
            if (Physics.Raycast(transform.position, currentDirection, out _hit, _rayDistance))
            {
                if (_hit.transform.tag == "OpaqueObject")
                {
                    Instantiate(_point, _hit.point, Quaternion.identity);
                }
            }
        }
    }
}
