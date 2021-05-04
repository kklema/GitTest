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

    private void Update()
    {
        _time += Time.deltaTime;
    }

    private IEnumerator ChangeAlarmColor()
    {
        var newWaitForSeconds = new WaitForSeconds(1f);

        _spriteRenderer.color = Color.Lerp(_startColor, _targetColor, _lerpTime * _time);

        yield return newWaitForSeconds;
    }

    public void StartChangeColor()
    {
        StartCoroutine(ChangeAlarmColor());
    }

    public void StopChangeColor()
    {
        StopCoroutine(ChangeAlarmColor());
    }
}
