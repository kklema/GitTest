using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmZone : MonoBehaviour
{
    private AudioSource _audioAlarm;
    private AlarmLamp _colorAlarm;
    private float _alarmTimer;

    private void Start()
    {
        _audioAlarm = GetComponent<AudioSource>();
        _colorAlarm = GetComponentInChildren<AlarmLamp>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            _audioAlarm.Play();
            _colorAlarm.StartChangeColor();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            _audioAlarm.Stop();
            _audioAlarm.volume = 0;
            _colorAlarm.StopChangeColor();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            if (_audioAlarm.isPlaying)
            {
                _alarmTimer += Time.deltaTime / 1000;
                _audioAlarm.volume += _alarmTimer;
            }
        }
    }
}