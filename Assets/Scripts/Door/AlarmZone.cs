using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmZone : MonoBehaviour
{
    private AudioSource _audioAlarm;
    private AlarmLamp _colorAlarm;
    private float _alarmTimer;
    private bool _isVolumeIncreased;

    private void Start()
    {
        _audioAlarm = GetComponent<AudioSource>();
        _colorAlarm = GetComponentInChildren<AlarmLamp>();
        _audioAlarm.volume = 0;
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
            _alarmTimer = 0;
            _colorAlarm.StopChangeColor();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            float maxVolume = 1f;
            float minVolume = 0f;
            _alarmTimer += Time.deltaTime / 5;

            if (_audioAlarm.isPlaying)
            {
                _audioAlarm.volume += _alarmTimer;
                if (_audioAlarm.volume >= maxVolume && _isVolumeIncreased == true)
                {
                    _isVolumeIncreased = false;
                }
                else if (_isVolumeIncreased == false)
                {
                    _audioAlarm.volume += -_alarmTimer;
                    if (_audioAlarm.volume <= minVolume)
                    {
                        _isVolumeIncreased = true;
                    }
                }

                if (_alarmTimer > maxVolume)
                {
                    _alarmTimer = minVolume;
                }
            }
        }
    }
}