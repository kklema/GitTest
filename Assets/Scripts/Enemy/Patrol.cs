using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Flip))]
public class Patrol : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform[] _waypoints;

    private int _currentPoint;
    private Transform[] _points;
    private Transform _target;
    private Flip _flip;

    private void Start()
    {
        _flip = GetComponent<Flip>();

        _points = new Transform[_waypoints.Length];

        for (int i = 0; i < _waypoints.Length; i++)
        {
            _points[i] = _waypoints[i];
        }
    }

    private void FixedUpdate()
    {
        _target = _points[_currentPoint];

        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);

        _flip.TurnSide(_target.position);

        if (transform.position == _target.position)
        {
            _currentPoint++;

            if (_currentPoint >= _points.Length)
            {
                _currentPoint = 0;
            }
        }
    }
}