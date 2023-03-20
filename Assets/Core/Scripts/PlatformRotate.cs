using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRotate : MonoBehaviour
{
    [Header("Debug ReadOnly Values")]
    [SerializeField] bool _isMouseHolding = false;
    [SerializeField] Vector2 _mousePointerStart;
    [SerializeField] float _timeElapsedOnMouseHold;
    [SerializeField] float _currentRotationSpeed;

    [Header("Configuration")]
    [SerializeField] AnimationCurve _rotationSpeedCurve;
    [SerializeField] Transform _rotationTargetObject;
    [SerializeField] RectTransform _rotationArea;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            _isMouseHolding = false;
            _timeElapsedOnMouseHold = 0;
            return;
        }

        if (Input.GetMouseButtonDown(0) && !_isMouseHolding)
        {
            Vector2 localMousePosition = _rotationArea.InverseTransformPoint(Input.mousePosition);
            if (!_rotationArea.rect.Contains(localMousePosition))
            {
                return;
            }
            _isMouseHolding = true;
            _mousePointerStart = localMousePosition;
        }

        if (!_isMouseHolding)
        {
            return;
        }
        _timeElapsedOnMouseHold += Time.deltaTime;
        RotateObject();
    }

    void RotateObject()
    {
        Vector2 localMousePosition = _rotationArea.InverseTransformPoint(Input.mousePosition);
        // KK: Want to showcase curves but curves can be replaced with this move distance, Curves make a more linear and polished looking animations
        //float _mouseMoveDistance = Vector2.Distance(localMousePosition, _mousePointerStart);
        _currentRotationSpeed = _rotationSpeedCurve.Evaluate(_timeElapsedOnMouseHold);

        if (_mousePointerStart.x > localMousePosition.x)
        {
            _rotationTargetObject.Rotate(new Vector3(0, _currentRotationSpeed * Time.deltaTime));
        } 
        else
        {
            _rotationTargetObject.Rotate(new Vector3(0, -_currentRotationSpeed * Time.deltaTime));
        }
    }
}
