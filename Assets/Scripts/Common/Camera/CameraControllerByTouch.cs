using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraControllerByTouch : MonoBehaviour
{
    [Range(0,90)]
    public float MinAngle = 0;
    [Range(0, 90)]
    public float MaxAngle = 60;
    public float Height = 70;
    public float MaxHeight = 90;
    public float MinHeight = 50;

    [Header("CameraFunctions:")]
    public bool CanZoom = true;
    public bool CanRotateX = true;

    [Header("Settings:")]
    [Range(1, 100)]
    [SerializeField] private float Sensitivity = 14;
    [SerializeField] private float ShakingMagnitude = 5;
    [SerializeField] private float ShakingDration = 2;
    
    
    private Transform _centerOfView;
    private float _screenResolution;
    private bool _touchWasMoved = false;

    private void Awake() 
    {
        _screenResolution = Mathf.Sqrt(Mathf.Pow(Screen.height,2) + Mathf.Pow(Screen.width,2));    
    }

    public void SetTargetPosition(Vector3 position)
    {
        _centerOfView.position = position;
    }

    private void Start() 
    {
        _centerOfView = new GameObject("CenterOfView").transform;
        _centerOfView.position = GameObject.FindGameObjectWithTag("Player").transform.position;
        transform.parent = _centerOfView;
        transform.eulerAngles = Vector3.right*90 + Vector3.forward*180;
        transform.localPosition = Vector3.up * Height;

        _centerOfView.eulerAngles = Vector3.right * MinAngle;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount>0)
        {
            CheckTouches();
        }
    }

    private void CheckTouches()
    {
        int touchCount = Input.touchCount;

        if(touchCount == 1)
        {
            var touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                MoveCamera(touch);
                _touchWasMoved = true;       
            }
            if(touch.phase == TouchPhase.Ended) 
            {
                if(!_touchWasMoved)
                    StartCoroutine(Shake(ShakingDration, ShakingMagnitude));
                _touchWasMoved = false;                   
            }
        }
        else if(touchCount == 2)
        {
            CheckSigns(Input.touches);
        }
    }

    private void MoveCamera(Touch touch)
    {
        if(touch.phase != TouchPhase.Moved) return;

        var distance = Vector2.Distance(Camera.main.transform.position, _centerOfView.position);
        var angle = _centerOfView.eulerAngles.y * Mathf.Deg2Rad;
        var delta = touch.deltaPosition * Mathf.Sqrt(distance) * Time.deltaTime;
        var deltaPosition = _centerOfView.TransformDirection(new Vector3(delta.x, 0, delta.y));
        _centerOfView.position += new Vector3(deltaPosition.x, 0, deltaPosition.z);
    }

    private void CheckSigns(Touch[] touches)
    {
        var firstFinger = touches[0];
        var secondFinger = touches[1];

        if(Zoom(firstFinger, secondFinger)) return; 
        if(RotateX(firstFinger, secondFinger)) return;
        //if(RotateY(firstFinger, secondFinger)) return;
                
    }

    private bool Zoom(Touch firstFinger, Touch secondFinger)
    {
        if(!CanZoom) return false;

        var firstDirection = firstFinger.deltaPosition.normalized;
        var secondDirection = secondFinger.deltaPosition.normalized;

        var lastFirstPosition = firstFinger.position + firstFinger.deltaPosition;
        var lastSecondPosition = secondFinger.position + secondFinger.deltaPosition;

        var resultDirection = firstDirection + secondDirection;

        if(Mathf.Abs(resultDirection.x) < 1 && Mathf.Abs(resultDirection.y) < 1)
        {
            var zoomScale = Mathf.Sqrt(firstFinger.deltaPosition.magnitude * secondFinger.deltaPosition.magnitude);
            var difference = zoomScale * Time.deltaTime * _screenResolution/Sensitivity;
            if(Vector2.Distance(firstFinger.position, secondFinger.position) < Vector2.Distance(lastFirstPosition, lastSecondPosition))
                difference *= (-1);
            Height = Mathf.Clamp(Height + difference, MinHeight, MaxHeight);
            transform.localPosition = new Vector3(0, Height, 0);
            return true;
        }

        return false;
    }

    private bool RotateY(Touch firstFinger, Touch secondFinger)
    {

        var firstDelta = firstFinger.deltaPosition;
        var secondDelta = secondFinger.deltaPosition;

        var firstPosition = firstFinger.position;
        var secondPosition = secondFinger.position;
        var pointBetweenFingers = (firstPosition+secondPosition)/2;

        if((firstDelta+secondDelta).magnitude < _screenResolution/Sensitivity && firstDelta.magnitude > 0 && secondDelta.magnitude > 0)
        {
            float rotateDirection = _screenResolution /Sensitivity;
            if((firstDelta.x * firstDelta.y > 0 || secondDelta.x * secondDelta.y > 0))
                rotateDirection *= -1;
            _centerOfView.Rotate(Vector3.down * rotateDirection * Time.deltaTime);
            return true;
        }
        
        return false;


    }

    private bool RotateX(Touch firstFinger, Touch secondFinger)
    {
        if(!CanRotateX) return false;

        var firstDelta = firstFinger.deltaPosition.y;
        var secondDelta = secondFinger.deltaPosition.y;

        var firstY = firstFinger.position.y;
        var secondY = secondFinger.position.y;

        if(Mathf.Abs(firstDelta-secondDelta) < _screenResolution/Sensitivity && Mathf.Abs(firstY-secondY) < _screenResolution/Sensitivity)
        {
            var curretnAngle = _centerOfView.eulerAngles.x + firstDelta;
            var newAngle = Mathf.Clamp(curretnAngle, MinAngle, MaxAngle);
            _centerOfView.eulerAngles = new Vector3(newAngle, _centerOfView.eulerAngles.y, _centerOfView.eulerAngles.z);
            return true;
        }

        return false;
    }

    private IEnumerator Shake(float duration, float magnitude)
    {
        var originalPosition = transform.localPosition;

        float elapsed = 0;

        while(elapsed < duration)
        {
            float x = Random.Range(-1f,1f) * magnitude + originalPosition.x;
            float y = Random.Range(-1f,1f) * magnitude + originalPosition.y;

            var newPosition = new Vector3(x, y, originalPosition.z);
            transform.localPosition = newPosition;

            elapsed += Time.deltaTime;
            magnitude -= magnitude/10;

            yield return null;
        }

        transform.localPosition = originalPosition;
    }
}
