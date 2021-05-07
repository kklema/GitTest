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

    private bool _coroutinIsLooped;
    private float _passedTime;

    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _startColor = _spriteRenderer.color;
    }

    private IEnumerator ChangeAlarmColor()
    {
        while (_coroutinIsLooped)
        {
            _passedTime = 0f;

            while (_passedTime < _speedChangings)
            {
                _passedTime += Time.deltaTime;
                float lerpPercentage = _passedTime / _speedChangings;
                var newWaitForSeconds = new WaitForSeconds(0.01f);

                _spriteRenderer.color = Color.Lerp(_startColor, _targetColor, lerpPercentage);

                yield return newWaitForSeconds;
            }

            _spriteRenderer.color = _startColor;
        }
    }

    public void StartChangeColor()
    {
        _coroutinIsLooped = true;
        StartCoroutine(ChangeAlarmColor());
    }

    public void StopChangeColor()
    {
        _coroutinIsLooped = false;
        StopCoroutine(ChangeAlarmColor());
    }
}