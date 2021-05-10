using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Collider2D))]
public class AlarmZone : MonoBehaviour
{
    [SerializeField] private float _speedChange;

    private AudioSource _audioAlarm;
    private AlarmLamp _alarmLamp;

    private bool _coroutineIslooped;

    private void Start()
    {
        _audioAlarm = GetComponent<AudioSource>();
        _alarmLamp = GetComponentInChildren<AlarmLamp>();
        _audioAlarm.volume = 0;
        _speedChange = 1f;
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.TryGetComponent<Enemy>(out Enemy enemy))
        {
            _audioAlarm.Play();
            _coroutineIslooped = true;
            StartCoroutine(ChangeAlarmVolume());
            _alarmLamp.StartChangeColor();
        }
    }

    private void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.TryGetComponent<Enemy>(out Enemy enemy))
        {
            _audioAlarm.Stop();
            _coroutineIslooped = false;
            StopCoroutine(ChangeAlarmVolume());
            _audioAlarm.volume = 0;
            _alarmLamp.StopChangeColor();
        }
    }

    private IEnumerator ChangeAlarmVolume()
    {
        while (_coroutineIslooped)
        {
            if (_audioAlarm.isPlaying)
            {
                _audioAlarm.volume = Mathf.PingPong(Time.time, _speedChange);

                yield return null;
            }
        }
    }
}