using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip : MonoBehaviour
{
    private bool _isFacingRight;

    private void Start()
    {
        _isFacingRight = true;
    }

    public void TurnSide(Vector3 newPosition)
    {
        if (newPosition.x < transform.position.x && _isFacingRight)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            _isFacingRight = false;
        }
        else if (newPosition.x > transform.position.x && !_isFacingRight)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            _isFacingRight = true;
        }
    }
}
