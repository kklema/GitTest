using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform[] _waypoints;

    private Rigidbody2D _rigidbody2D;
    private int _currentPoint;
    private Transform[] _points;
    private SpriteRenderer _spriterenderer;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriterenderer = GetComponent<SpriteRenderer>();

        _points = new Transform[_waypoints.Length];

        for (int i = 0; i < _waypoints.Length; i++)
        {
            _points[i] = _waypoints[i];
        }
    }


    void Update()
    {
        Transform target = _points[_currentPoint];

        Flip(target);

        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

        if (transform.position == target.position)
        {
            _currentPoint++;

            if (_currentPoint >= _points.Length)
            {
                _currentPoint = 0;
            }
        }

        //_rigidbody2D.velocity = transform.right * _speed;
    }

    private void Flip(Transform target)
    {
        if (transform.position.x < target.position.x)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            //_spriterenderer.flipX = true;
        }
        else if (transform.position.x > target.position.x)
        {
            //_spriterenderer.flipX = false;
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
