using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmLamp : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    [SerializeField] [Range(0, 1)] private float _lerpTime;

    [SerializeField] private Color _targetColor;

    private Color _startColor;
    private float _time;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _startColor = _spriteRenderer.color;
    }

    private IEnumerator ChangeAlarmColor()
    {
        if (_time <= _lerpTime)
        {
            var newWaitForSeconds = new WaitForSeconds(0.5f);
            _time += Time.deltaTime;
            float normalizeLerpTime = _time / _lerpTime;

            _spriteRenderer.color = Color.Lerp(_spriteRenderer.color, _targetColor, normalizeLerpTime);

            yield return null;
        }
    }

    public void StartChangeColor()
    {
        StartCoroutine(ChangeAlarmColor());
    }

    public void StopChangeColor()
    {
        StopCoroutine(ChangeAlarmColor());
        _spriteRenderer.color = _startColor;
    }
}