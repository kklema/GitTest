using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Flip : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb2D;

    private float _speed;

    private void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        TurnSide();
    }

    private void TurnSide()
    {
        if (_rb2D.velocity.x > 0f)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (_rb2D.velocity.x < 0f)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
