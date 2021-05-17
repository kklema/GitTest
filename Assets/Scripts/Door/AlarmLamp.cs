using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AlarmLamp : MonoBehaviour
{
    [SerializeField] private Color _startColor;
    [SerializeField] private Color _targetColor;
    [SerializeField] private Color _currentColor;
    [SerializeField] private float _speedChangings;

    private float _passedTime;
    private float _cooldown = 0.01f;

    private Coroutine _colorchangerWork;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _startColor = _spriteRenderer.color;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent<Enemy>(out Enemy enemy))
        {
            _colorchangerWork = StartCoroutine(ChangeAlarmColor());
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.TryGetComponent<Enemy>(out Enemy enemy))
        {
            StopCoroutine(_colorchangerWork);
            _spriteRenderer.color = _startColor;
        }
    }

    private IEnumerator ChangeAlarmColor()
    {
        while (true)
        {
            _passedTime = 0f;

            while (_passedTime < _speedChangings)
            {
                _passedTime += Time.deltaTime;
                float lerpPercentage = _passedTime / _speedChangings;
                var newWaitForSeconds = new WaitForSeconds(_cooldown);

                _spriteRenderer.color = Color.Lerp(_startColor, _targetColor, lerpPercentage);

                yield return newWaitForSeconds;
            }

            _spriteRenderer.color = _startColor;
        }
    }
}