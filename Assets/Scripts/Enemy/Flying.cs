using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Flying : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody2D _rigidody2D;

    private void Start()
    {
        _rigidody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidody2D.velocity = new Vector2(_speed, 0);
    }
}
