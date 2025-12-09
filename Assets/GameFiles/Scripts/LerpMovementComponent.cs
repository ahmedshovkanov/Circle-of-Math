using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LerpMovementComponent : MonoBehaviour
{
    
    public void InitLerpMovement(bool isMoving, Vector3 targetPos)
    {
        _isMoving = isMoving;
        _targetPos = targetPos;
    }

    private void Start()
    {
        _startPos = transform.localPosition;
    }

    private bool _isMoving;

    private Vector3 _startPos;
    private Vector3 _targetPos;
    [SerializeField] private float _speed;
    private float _progress;

    private void FixedUpdate()
    {
        if (_isMoving)
        {
            transform.localPosition = Vector3.Lerp(_startPos, _targetPos, _progress * Time.fixedDeltaTime);
            _progress += _speed;
        }
    }
    
    public void SetIsMoving(bool isMoving)
    {
        _isMoving = isMoving;
    }
}
