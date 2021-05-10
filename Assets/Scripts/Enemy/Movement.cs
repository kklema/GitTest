using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform[] _waypoints;

    private Rigidbody2D _rigidbody2D;
    private int _currentPoint;
    private Transform[] _points;
    private SpriteRenderer _spriteRenderer;
    private Transform _target;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _points = new Transform[_waypoints.Length];

        for (int i = 0; i < _waypoints.Length; i++)
        {
            _points[i] = _waypoints[i];
        }
    }

    private void Update()
    {
        _target = _points[_currentPoint];

        Flip();

        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);

        if (transform.position == _target.position)
        {
            _currentPoint++;

            if (_currentPoint >= _points.Length)
            {
                _currentPoint = 0;
            }
        }
    }

    private void Flip()
    {
        if (transform.position.x < _target.position.x)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (transform.position.x > _target.position.x)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
