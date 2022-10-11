using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class SimpleParabolaMovement : MonoBehaviour
{

    public Transform target;
    [Range(0.1f, 30f)]
    public float speed = 1;

    private Vector3 _offset = new Vector3(0, 10, 0);
    private bool _reachedTarget = false;

    private float _time = 0;
    private Vector3 _startPoint;
    private Vector3 _targetPoint;
    private Vector3 _middlePoint;
    private Vector3 _point1;
    private Vector3 _point2;


    private void Start()
    {
        if (target == null) return;
        _startPoint = transform.position;
        _targetPoint = target.position + (new Vector3(0, transform.localScale.y, 0) / 2);
        _middlePoint = (_startPoint * 2 + _targetPoint) / 3 + _offset;
        _point1 = _startPoint;
        _point2 = _middlePoint;
        _time = 0;
    }

    private void Update()
    {
        if (target == null) return;
        if (_reachedTarget == true) return;

        _time += Time.deltaTime * speed;
        _point1 = Vector3.Lerp(_startPoint, _middlePoint, _time);
        _point2 = Vector3.Lerp(_middlePoint, _targetPoint, _time);
        transform.position = Vector3.Lerp(_point1, _point2, _time);


        if (_point1 == _middlePoint && _point2 == _targetPoint)
        {
            _reachedTarget = true;
        }

    }
}