using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AlarmZone : MonoBehaviour
{
    [SerializeField] private float _speedChange;

    private AudioSource _audioAlarm;
    private Coroutine _volumeChangerWork;

    private bool _coroutineIsLooped;

    private void Start()
    {
        _audioAlarm = GetComponent<AudioSource>();
        _audioAlarm.volume = 0;
        _speedChange = 1f;
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.TryGetComponent<Enemy>(out Enemy enemy))
        {
            _audioAlarm.Play();
            _volumeChangerWork = StartCoroutine(ChangeAlarmVolume());
        }
    }

    private void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.TryGetComponent<Enemy>(out Enemy enemy))
        {
            _audioAlarm.Stop();
            StopCoroutine(_volumeChangerWork);
            _audioAlarm.volume = 0;
        }
    }

    private IEnumerator ChangeAlarmVolume()
    {
        _coroutineIsLooped = true;

        while (_coroutineIsLooped)
        {
            if (_audioAlarm.isPlaying)
            {
                _audioAlarm.volume = Mathf.PingPong(Time.time, _speedChange);

                yield return null;
            }
        }
    }
}